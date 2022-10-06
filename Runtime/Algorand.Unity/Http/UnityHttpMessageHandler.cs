using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;

namespace Algorand.Unity
{
    public class UnityHttpMessageHandler : HttpMessageHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken
            )
        {
            var requestBytes = request.Content != null
                ? request.Content.ReadAsByteArrayAsync().Result
                : null
                ;
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

            Debug.Log("Sent webrequest");
            await unityWebRequest.SendWebRequest().WithCancellation(cancellationToken);
            var responseMessage = new HttpResponseMessage((HttpStatusCode)unityWebRequest.responseCode)
            {
                Content = new ByteArrayContent(unityWebRequest.downloadHandler.data)
            };
            foreach (var header in unityWebRequest.GetResponseHeaders())
            {
                try
                {
                    var headerLower = header.Key.ToLower();
                    if (headerLower is not "content-type" && headerLower is not "content-length")
                        responseMessage.Headers.Add(header.Key, header.Value);
                    else
                        responseMessage.Content.Headers.Add(header.Key, header.Value);
                }
                catch
                {
                    Debug.LogError($"Issue with header: {header}");
                    throw;
                }
            }
            return responseMessage;
        }
    }
}
