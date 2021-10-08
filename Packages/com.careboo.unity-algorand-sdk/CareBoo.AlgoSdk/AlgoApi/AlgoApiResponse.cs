using System.Text;
using Unity.Collections;
using UnityEngine.Networking;

namespace AlgoSdk
{
    public enum AlgoApiFormat : byte
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
        AlgoApiFormat contentType;

        public AlgoApiResponse(ref UnityWebRequest completedRequest)
        {
            data = completedRequest.downloadHandler.data;
            status = completedRequest.result;
            responseCode = completedRequest.responseCode;
            var contentTypeHeader = completedRequest.GetResponseHeader("Content-Type");
            contentTypeHeader = PruneParametersFromContentType(contentTypeHeader);
            contentType = contentTypeHeader switch
            {
                "application/json" => AlgoApiFormat.Json,
                "application/msgpack" => AlgoApiFormat.MessagePack,
                _ => AlgoApiFormat.None
            };
            completedRequest.Dispose();
        }

        public byte[] Data => data;

        public long ResponseCode => responseCode;

        public UnityWebRequest.Result Status => status;

        public AlgoApiFormat ContentType => contentType;

        public string GetText()
        {
            return contentType == AlgoApiFormat.MessagePack
                ? System.Convert.ToBase64String(data)
                : Encoding.UTF8.GetString(data, 0, data.Length);
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
#if UNITY_INCLUDE_TESTS
            UnityEngine.Debug.Log(response.GetText());
#endif
            this.rawResponse = response;
            byte[] rawBytes = response.Data;
            using var bytes = new NativeArray<byte>(rawBytes, Allocator.Temp);
            error = response.Status switch
            {
                UnityWebRequest.Result.ProtocolError => AlgoApiSerializer.Deserialize<ErrorResponse>(bytes.AsReadOnly(), response.ContentType),
                UnityWebRequest.Result.ConnectionError => new ErrorResponse("Could not connect"),
                UnityWebRequest.Result.DataProcessingError => new ErrorResponse("Error processing data from server"),
                _ => default
            };
            payload = response.Status switch
            {
                UnityWebRequest.Result.Success => AlgoApiSerializer.Deserialize<T>(bytes.AsReadOnly(), response.ContentType),
                _ => default
            };
        }

        public T Payload => payload;

        public ErrorResponse Error => error;

        public AlgoApiResponse Raw => rawResponse;

        public byte[] Data => rawResponse.Data;

        public long ResponseCode => rawResponse.ResponseCode;

        public UnityWebRequest.Result Status => rawResponse.Status;

        public AlgoApiFormat ContentType => rawResponse.ContentType;

        public string GetText() => rawResponse.GetText();

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
