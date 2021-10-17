using System.Diagnostics;
using System.Text;
using UnityEngine.Networking;
using static UnityEngine.Networking.UnityWebRequest;

namespace AlgoSdk
{
    public struct AlgoApiResponse
    {
        readonly byte[] data;
        readonly Result status;
        readonly long responseCode;
        readonly ContentType contentType;
        ErrorResponse error;

        public AlgoApiResponse(UnityWebRequest completedRequest)
        {
            data = completedRequest.downloadHandler?.data;
            status = completedRequest.result;
            responseCode = completedRequest.responseCode;
            contentType = completedRequest.ParseContentType();
            error = status switch
            {
                Result.ProtocolError => AlgoApiSerializer.Deserialize<ErrorResponse>(data, contentType).WithCode(responseCode),
                Result.ConnectionError => new ErrorResponse { Message = "Failed to communicate with the server", Code = responseCode },
                Result.DataProcessingError => new ErrorResponse { Message = "Error processing data", Code = responseCode },
                _ => default
            };
            DebugRequest(completedRequest, contentType);
            completedRequest.Dispose();
        }

        public byte[] Data => data;

        public long ResponseCode => responseCode;

        public Result Status => status;

        public ContentType ContentType => contentType;

        public ErrorResponse Error => error;

        public string GetText() => GetText(data, contentType);


        [Conditional("UNITY_EDITOR")]
        [Conditional("UNITY_ALGO_SDK_DEBUG")]
        static void DebugRequest(UnityWebRequest completedRequest, ContentType contentType)
        {
            UnityEngine.Debug.Log(
                "completed request\n" +
                $"\turl: {completedRequest.url}\n" +
                $"\tuploadedData: {GetText(completedRequest.uploadHandler?.data, contentType)}\n" +
                $"\tdownloadedData: {GetText(completedRequest.downloadHandler?.data, contentType)}\n" +
                $"\terror: {completedRequest.error}\n" +
                $"\tmethod: {completedRequest.method}\n" +
                $"\tdownloadHandler.error: {completedRequest.downloadHandler?.error}"
            );
        }

        static string GetText(byte[] data, ContentType contentType)
        {
            if (data == null)
                return "";
            return contentType == ContentType.MessagePack
                ? System.Convert.ToBase64String(data)
                : Encoding.UTF8.GetString(data, 0, data.Length)
                ;
        }
    }

    public readonly struct AlgoApiResponse<T>
    {
        readonly AlgoApiResponse rawResponse;
        readonly ErrorResponse error;
        readonly T payload;

        public AlgoApiResponse(AlgoApiResponse response)
        {
            this.rawResponse = response;
            error = response.Error;
            payload = error.IsError
                ? default
                : AlgoApiSerializer.Deserialize<T>(response.Data, response.ContentType)
                ;
        }

        public AlgoApiResponse(AlgoApiResponse response, ErrorResponse error)
        {
            this.rawResponse = response;
            this.error = error;
            this.payload = default;
        }

        public AlgoApiResponse(AlgoApiResponse response, T payload)
        {
            this.rawResponse = response;
            this.error = default;
            this.payload = payload;
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

        public void Deconstruct(out ErrorResponse error, out T payload)
        {
            error = this.error;
            payload = this.payload;
        }
    }
}
