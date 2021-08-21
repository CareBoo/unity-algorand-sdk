using System;
using System.Linq;
using AlgoSdk.MsgPack;
using MessagePack;
using Unity.Collections;
using UnityEngine.Networking;

namespace AlgoSdk
{
    public static class UnityWebRequestExtensions
    {
        public static Response GetResponse(this UnityWebRequest webRequest)
        {
            return webRequest.result switch
            {
                UnityWebRequest.Result.Success => webRequest.GetSuccessResponse(),
                UnityWebRequest.Result.ProtocolError => webRequest.GetProtocolErrorResponse(),
                UnityWebRequest.Result.ConnectionError => webRequest.GetConnectionErrorResponse(),
                UnityWebRequest.Result.DataProcessingError => webRequest.GetDataProcessingErrorResponse(),
                _ => throw new NotSupportedException($"Unknown result occurred: {webRequest.result}")
            };
        }

        public static Response<T> GetMessagePackResponse<T>(this UnityWebRequest webRequest)
            where T : unmanaged
        {
            return webRequest.result switch
            {
                UnityWebRequest.Result.Success => webRequest.GetMessagePackResponse<T>(),
                UnityWebRequest.Result.ProtocolError => webRequest.GetProtocolErrorResponse(),
                UnityWebRequest.Result.ConnectionError => webRequest.GetConnectionErrorResponse(),
                UnityWebRequest.Result.DataProcessingError => webRequest.GetDataProcessingErrorResponse(),
                _ => throw new NotSupportedException($"Unknown result occurred: {webRequest.result}")
            };
        }

        private static NativeText GetSuccessResponse(this UnityWebRequest webRequest)
        {
            return new NativeText(webRequest.downloadHandler.text, Allocator.Temp);
        }

        private static NativeReference<T> GetMessagePackSuccessResponse<T>(this UnityWebRequest webRequest)
            where T : unmanaged
        {
            var obj = MessagePackSerializer.Deserialize<T>(webRequest.downloadHandler.data, AlgoSdkMessagePackConfig.SerializerOptions);
            return new NativeReference<T>(obj, Allocator.Temp);
        }

        private static ErrorResponse GetProtocolErrorResponse(this UnityWebRequest webRequest)
        {
            var contentType = webRequest.GetResponseHeader("Content-Type");
            var bytes = contentType.Contains("application/json")
                ? MessagePackSerializer.ConvertFromJson(webRequest.downloadHandler.text, AlgoSdkMessagePackConfig.SerializerOptions)
                : webRequest.downloadHandler.data
                ;
            return MessagePackSerializer.Deserialize<ErrorResponse>(bytes, AlgoSdkMessagePackConfig.SerializerOptions);
        }

        private static ErrorResponse GetConnectionErrorResponse(this UnityWebRequest webRequest)
        {
            const string errorMessage = "Could not connect to server";
            return new ErrorResponse(errorMessage, Allocator.Temp);
        }

        private static ErrorResponse GetDataProcessingErrorResponse(this UnityWebRequest webRequest)
        {
            const string errorMessage = "Experience error processing data";
            return new ErrorResponse(errorMessage, Allocator.Temp);
        }
    }
}
