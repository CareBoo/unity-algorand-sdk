using System.Text;
using UnityEngine.Networking;

namespace AlgoSdk
{
    public enum ContentType : byte
    {
        None,
        Json,
        MessagePack
    }

    public struct AlgoApiResponse
    {
        byte[] data;
        UnityWebRequest.Result status;
        long responseCode;
        ContentType contentType;

        public AlgoApiResponse(ref UnityWebRequest completedRequest)
        {
            data = completedRequest.downloadHandler.data;
            status = completedRequest.result;
            responseCode = completedRequest.responseCode;
            var contentTypeHeader = completedRequest.GetResponseHeader("Content-Type");
            contentTypeHeader = PruneParametersFromContentType(contentTypeHeader);
            contentType = contentTypeHeader switch
            {
                "application/json" => ContentType.Json,
                "application/msgpack" => ContentType.MessagePack,
                _ => ContentType.None
            };
            completedRequest.Dispose();
        }

        public byte[] Data => data;

        public long ResponseCode => responseCode;

        public UnityWebRequest.Result Status => status;

        public ContentType ContentType => contentType;

        public string GetText()
        {
            return Encoding.UTF8.GetString(data, 0, data.Length);
        }

        private static string PruneParametersFromContentType(string fullType)
        {
            if (fullType == null) return fullType;
            for (var i = 0; i < fullType.Length; i++)
                if (fullType[i] == ';')
                    return fullType.Substring(0, i);
            return fullType;
        }
    }

    public struct AlgoApiResponse<T>
    {
        AlgoApiResponse rawResponse;
        ErrorResponse error;
        T payload;

        public AlgoApiResponse(AlgoApiResponse response)
        {
            this.rawResponse = response;
            byte[] rawBytes = response.Data;
            error = response.Status switch
            {
                UnityWebRequest.Result.ProtocolError => AlgoApiSerializer.Deserialize<ErrorResponse>(rawBytes, response.ContentType),
                UnityWebRequest.Result.ConnectionError => new ErrorResponse("Could not connect"),
                UnityWebRequest.Result.DataProcessingError => new ErrorResponse("Error processing data from server"),
                _ => default
            };
            payload = response.Status switch
            {
                UnityWebRequest.Result.Success => AlgoApiSerializer.Deserialize<T>(rawBytes, response.ContentType),
                _ => default
            };
        }

        public T Payload => payload;

        public ErrorResponse Error => error;

        public AlgoApiResponse Raw => rawResponse;

        public static implicit operator AlgoApiResponse<T>(AlgoApiResponse response)
        {
            return new AlgoApiResponse<T>(response);
        }

        public static implicit operator AlgoApiResponse(AlgoApiResponse<T> response)
        {
            return response.rawResponse;
        }
    }
}
