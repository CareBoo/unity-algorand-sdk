using System.Text;
using Unity.Collections;
using UnityEngine.Networking;
using static UnityEngine.Networking.UnityWebRequest;

namespace AlgoSdk
{
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
            contentType = contentTypeHeader.ToContentType();
            completedRequest.Dispose();
        }

        public byte[] Data => data;

        public long ResponseCode => responseCode;

        public Result Status => status;

        public ContentType ContentType => contentType;

        public string GetText()
        {
            return contentType == ContentType.MessagePack
                ? System.Convert.ToBase64String(data)
                : Encoding.UTF8.GetString(data, 0, data.Length)
                ;
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
                Result.ProtocolError => AlgoApiSerializer.Deserialize<ErrorResponse>(bytes.AsReadOnly(), response.ContentType),
                Result.ConnectionError => new ErrorResponse("Could not connect"),
                Result.DataProcessingError => new ErrorResponse("Error processing data from server"),
                _ => default
            };
            payload = response.Status switch
            {
                Result.Success => AlgoApiSerializer.Deserialize<T>(bytes.AsReadOnly(), response.ContentType),
                _ => default
            };
        }

        public T Payload => payload;

        public ErrorResponse Error => error;

        public AlgoApiResponse Raw => rawResponse;

        public byte[] Data => rawResponse.Data;

        public long ResponseCode => rawResponse.ResponseCode;

        public Result Status => rawResponse.Status;

        public ContentType ContentType => rawResponse.ContentType;

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
