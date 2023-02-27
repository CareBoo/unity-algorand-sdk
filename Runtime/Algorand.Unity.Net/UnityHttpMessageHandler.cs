using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;

namespace Algorand.Unity.Net
{
    public class UnityHttpMessageHandler : HttpMessageHandler
    {
        public UnityWebRequestAsyncOperation LastSentRequest { get; protected set; }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken
            )
        {
            var requestBytes = request.Content?.ReadAsByteArrayAsync().Result;
            using var uploadHandler = new UploadHandlerRaw(requestBytes);
            using var downloadHandler = new DownloadHandlerBuffer();
            using var unityWebRequest = new UnityWebRequest
            {
                url = request.RequestUri.ToString(),
                method = request.Method.Method.ToUpperInvariant(),
                uploadHandler = uploadHandler,
                downloadHandler = downloadHandler,
            };
            foreach (var header in request.Headers)
            {
                var key = header.Key;
                var value = header.Value.First();
                unityWebRequest.SetRequestHeader(key, value);
            }

            var sentRequest = unityWebRequest.SendWebRequest();
            LastSentRequest = sentRequest;
            await sentRequest.WithCancellation(cancellationToken);
            var responseCode = (HttpStatusCode)unityWebRequest.responseCode;
            var responseContent = new ByteArrayContent(unityWebRequest.downloadHandler.data);
            var responseMessage = new HttpResponseMessage(responseCode) { Content = responseContent };
            foreach (var header in unityWebRequest.GetResponseHeaders())
            {
                try
                {
                    if (IsContentHeader(header))
                        responseMessage.Content.Headers.Add(header.Key, header.Value);
                    else
                        responseMessage.Headers.Add(header.Key, header.Value);
                }
                catch (System.Exception ex)
                {
                    var aggregate = new System.Exception($"Issue with header: {header}", ex);
                    Debug.LogException(aggregate);
                    throw;
                }
            }
            return responseMessage;
        }

        private static bool IsContentHeader(KeyValuePair<string, string> header)
        {
            var headerKeyLower = header.Key.ToLower();
            return headerKeyLower == "content-type" || headerKeyLower == "content-length";
        }
    }
}
