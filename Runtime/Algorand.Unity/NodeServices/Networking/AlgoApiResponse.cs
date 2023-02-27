using System.Diagnostics;
using System.Linq;
using System.Text;
using UnityEngine.Networking;
using static UnityEngine.Networking.UnityWebRequest;

namespace Algorand.Unity
{
    /// <summary>
    /// Wrapper class around a completed <see cref="UnityWebRequest"/>
    /// </summary>
    public struct AlgoApiResponse : IAlgoApiResponse
    {
        private readonly byte[] data;
        private readonly Result status;
        private readonly long responseCode;
        private readonly ContentType contentType;
        private readonly ErrorResponse error;

        public AlgoApiResponse(UnityWebRequest completedRequest)
        {
            DebugRequest(completedRequest);
            data = completedRequest.downloadHandler?.data;
            status = completedRequest.result;
            responseCode = completedRequest.responseCode;
            contentType = completedRequest.ParseResponseContentType();
            error = status switch
            {
                Result.ProtocolError => AlgoApiSerializer.Deserialize<ErrorResponse>(data, contentType).WithCode(responseCode),
                Result.ConnectionError => new ErrorResponse { Message = $"Failed to communicate with the server: {completedRequest.error}", Code = responseCode },
                Result.DataProcessingError => new ErrorResponse { Message = $"Error processing data: {completedRequest.error}", Code = responseCode },
                _ => default
            };
            completedRequest.Dispose();
        }

        public AlgoApiResponse(ErrorResponse error)
        {
            data = null;
            status = Result.ProtocolError;
            responseCode = 0;
            contentType = ContentType.None;
            this.error = error;
        }

        public byte[] Data => data;

        public long ResponseCode => responseCode;

        public Result Status => status;

        public ContentType ContentType => contentType;

        public ErrorResponse Error => error;

        public string GetText() => GetText(data, contentType);


        [Conditional("UNITY_ALGO_SDK_DEBUG")]
        private static void DebugRequest(UnityWebRequest completedRequest)
        {
            UnityEngine.Debug.Log(
                "completed request\n" +
                $"\turl: {completedRequest.url}\n" +
                $"\tuploadedData: {GetText(completedRequest.uploadHandler?.data, completedRequest.ParseRequestContentType())}\n" +
                $"\tdownloadedData: {GetText(completedRequest.downloadHandler?.data, completedRequest.ParseResponseContentType())}\n" +
                $"\terror: {completedRequest.error}\n" +
                $"\tmethod: {completedRequest.method}\n" +
                $"\tdownloadHandler.error: {completedRequest.downloadHandler?.error}\n" +
                $"\tGetResponseHeaders(): {{\n\t\t{string.Join(",\n\t\t", completedRequest.GetResponseHeaders()?.Select(kvp => $"{kvp.Key}: {kvp.Value}") ?? new string[0])}\n\t}}"
            );
        }

        private static string GetText(byte[] data, ContentType contentType)
        {
            if (data == null)
                return "";
            return contentType == ContentType.MessagePack
                ? System.Convert.ToBase64String(data)
                : Encoding.UTF8.GetString(data, 0, data.Length)
                ;
        }
    }

    /// <summary>
    /// Wrapper class around a completed <see cref="UnityWebRequest"/>.
    /// This can be deconstructed into a tuple of (ErrorResponse, Payload)
    /// </summary>
    /// <typeparam name="T">The type of the object deserialized from the response</typeparam>
    public readonly struct AlgoApiResponse<T> : IAlgoApiResponse<T>
    {
        private readonly AlgoApiResponse rawResponse;
        private readonly ErrorResponse error;
        private readonly T payload;

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

        public byte[] Data => rawResponse.Data;

        public long ResponseCode => rawResponse.ResponseCode;

        public Result Status => rawResponse.Status;

        public ContentType ContentType => rawResponse.ContentType;

        public string GetText() => rawResponse.GetText();

        public AlgoApiResponse<U> Cast<U>()
        {
            return new AlgoApiResponse<U>(rawResponse);
        }

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
