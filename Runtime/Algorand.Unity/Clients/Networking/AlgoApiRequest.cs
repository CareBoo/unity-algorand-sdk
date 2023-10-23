using System;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using Algorand.Unity.Collections;
using Cysharp.Threading.Tasks;
using Unity.Collections;
using UnityEngine.Networking;
using static Cysharp.Threading.Tasks.UnityAsyncExtensions;

namespace Algorand.Unity
{
    /// <summary>
    /// A wrapper around <see cref="UnityWebRequest"/> for handling requests
    /// to Algorand REST services.
    /// </summary>
    public readonly ref struct AlgoApiRequest
    {
        private readonly UnityWebRequest unityWebRequest;

        private AlgoApiRequest(ref UnityWebRequest unityWebRequest)
        {
            this.unityWebRequest = unityWebRequest;
        }

        /// <summary>
        /// Set the token used for authenticating to the service.
        /// </summary>
        /// <param name="tokenHeader">The name of the request header used for the token.</param>
        /// <param name="token">The token used for authenticating to the service.</param>
        /// <returns><see cref="this"/> with token set</returns>
        public AlgoApiRequest SetToken(string tokenHeader, string token)
        {
            if (!string.IsNullOrEmpty(token))
                unityWebRequest.SetRequestHeader(tokenHeader, token);
            return this;
        }

        /// <summary>
        /// Set additional headers on this request
        /// </summary>
        /// <param name="headers">The headers in format "key:value"</param>
        /// <returns>this request with headers set</returns>
        public AlgoApiRequest SetHeaders(params Header[] headers)
        {
            if (headers == null)
                return this;

            for (var i = 0; i < headers.Length; i++)
            {
                var (key, value) = headers[i];
                unityWebRequest.SetRequestHeader(key, value);
            }
            return this;
        }

        /// <summary>
        /// Send the request.
        /// </summary>
        /// <param name="cancellationToken">An optional cancellation token</param>
        /// <returns>A <see cref="Sent"/> request</returns>
        public Sent Send(CancellationToken cancellationToken = default)
        {
            return new Sent(unityWebRequest.SendWebRequest(), cancellationToken);
        }

        /// <summary>
        /// Send the request and check progress callbacks using <see cref="IProgress{System.Single}"/>
        /// </summary>
        /// <typeparam name="TProgress">A progress callback implementing <see cref="IProgress{System.Single}"/></typeparam>
        /// <param name="progress">The progress callback</param>
        /// <param name="cancellationToken">An optional cancellation token</param>
        /// <returns>A <see cref="SentWithProgress{TProgress}"/> request</returns>
        public SentWithProgress<TProgress> Send<TProgress>(TProgress progress, CancellationToken cancellationToken = default)
            where TProgress : IProgress<float>
        {
            return new SentWithProgress<TProgress>(unityWebRequest.SendWebRequest(), progress, cancellationToken);
        }

        /// <summary>
        /// Set body of the request, and set its content type header to "application/json"
        /// </summary>
        /// <param name="json">The utf8 json to set the body of the request</param>
        /// <returns>this request with body and header set</returns>
        public AlgoApiRequest SetJsonBody(string json) =>
            SetRawBody(Encoding.UTF8.GetBytes(json), ContentType.Json);

        /// <summary>
        /// Set body of the request, and set its content type header to "application/json"
        /// </summary>
        /// <param name="json">The utf8 json to set the body of the request</param>
        /// <returns>this request with body and header set</returns>
        public AlgoApiRequest SetJsonBody(NativeText json) =>
            SetRawBody(json.AsArray().ToArray(), ContentType.Json);

        /// <summary>
        /// Set body of the request, and set its content type header to "application/json"
        /// </summary>
        /// <typeparam name="TBody">Type of the object to serialize</typeparam>
        /// <param name="value">An object to serialize into json, then to set in the body of the request</param>
        /// <returns>this request with body and header set</returns>
        public AlgoApiRequest SetJsonBody<TBody>(TBody value)
        {
            using var json = AlgoApiSerializer.SerializeJson(value, Allocator.Temp);
            return SetJsonBody(json);
        }

        /// <summary>
        /// Set body of the request, and set its content type header to "application/text"
        /// </summary>
        /// <param name="plainText">The utf8 plaintext to use for the body</param>
        /// <returns>this request with body and header set</returns>
        public AlgoApiRequest SetPlainTextBody(string plainText) =>
            SetPlainTextBody(Encoding.UTF8.GetBytes(plainText));

        /// <summary>
        /// Set body of the request, and set its content type header to "application/text"
        /// </summary>
        /// <param name="plainText">The utf8 plaintext to use for the body</param>
        /// <returns>this request with body and header set</returns>
        public AlgoApiRequest SetPlainTextBody(byte[] plainText) =>
            SetRawBody(plainText, ContentType.PlainText);


        /// <summary>
        /// Set body of the request and set its content type header to "application/msgpack"
        /// </summary>
        /// <param name="value">The value to serialize to message pack and set for the body</param>
        /// <returns>this request with body and header set</returns>
        public AlgoApiRequest SetMessagePackBody<TBody>(TBody value)
        {
            using var msgpack = AlgoApiSerializer.SerializeMessagePack(value, Allocator.Temp);
            return SetMessagePackBody(msgpack.AsArray().AsReadOnly());
        }

        /// <summary>
        /// Set body of the request and set its content type header to "application/msgpack"
        /// </summary>
        /// <param name="bytes">The msgpack bytes to set for the body</param>
        /// <returns>this request with body and header set</returns>
        public AlgoApiRequest SetMessagePackBody(NativeArray<byte>.ReadOnly bytes) =>
            SetMessagePackBody(bytes.ToArray());

        /// <summary>
        /// Set body of the request and set its content type header to "application/msgpack"
        /// </summary>
        /// <param name="bytes">The msgpack bytes to set for the body</param>
        /// <returns>this request with body and header set</returns>
        public AlgoApiRequest SetMessagePackBody(byte[] bytes) =>
            SetRawBody(bytes, ContentType.MessagePack);

        /// <summary>
        /// Set the body and content type header of the request
        /// </summary>
        /// <param name="data">The raw bytes to set for the body</param>
        /// <param name="contentType">The content type to set the header of the request</param>
        /// <returns>this request with body and header set</returns>
        public AlgoApiRequest SetRawBody(byte[] data, ContentType contentType)
        {
            if (data == null || data.Length == 0)
            {
                unityWebRequest.SetRequestHeader("Content-Type", contentType.ToHeaderValue());
                return this;
            }
            unityWebRequest.uploadHandler = new UploadHandlerRaw(data);
            unityWebRequest.uploadHandler.contentType = contentType.ToHeaderValue();
            unityWebRequest.disposeUploadHandlerOnDispose = true;
            return this;
        }

        /// <summary>
        /// Create a GET request
        /// </summary>
        /// <param name="url">The url for this request</param>
        /// <returns>A new GET request</returns>
        public static AlgoApiRequest Get(string url)
        {
            var webRequest = UnityWebRequest.Get(url);
            return new AlgoApiRequest(ref webRequest);
        }

        /// <summary>
        /// Create a POST request
        /// </summary>
        /// <param name="url">The url for this request</param>
        /// <returns>A new POST request</returns>
        public static AlgoApiRequest Post(string url)
        {
            var webRequest = UnityWebRequestPostWithoutBody(url);
            return new AlgoApiRequest(ref webRequest);
        }

        /// <summary>
        /// Create a DELETE request
        /// </summary>
        /// <param name="url">The url for this request</param>
        /// <returns>A new DELETE request</returns>
        public static AlgoApiRequest Delete(string url)
        {
            var webRequest = UnityWebRequest.Delete(url);
            return new AlgoApiRequest(ref webRequest);
        }

        private static UnityWebRequest UnityWebRequestPostWithoutBody(string url)
        {
            return new UnityWebRequest()
            {
                method = UnityWebRequest.kHttpVerbPOST,
                url = url,
                downloadHandler = new DownloadHandlerBuffer(),
                disposeDownloadHandlerOnDispose = true
            };
        }

        /// <summary>
        /// A wrapper around the <see cref="UnityWebRequestAsyncOperation"/> handling the sent request.
        /// </summary>
        public readonly struct Sent
        {
            private readonly UnityWebRequestAsyncOperation asyncOperation;
            private readonly CancellationToken cancellationToken;

            public UnityWebRequestAsyncOperation AsyncOperation => asyncOperation;

            public Sent(
                UnityWebRequestAsyncOperation asyncOperation,
                CancellationToken cancellationToken = default
            )
            {
                this.asyncOperation = asyncOperation;
                this.cancellationToken = cancellationToken;
            }

            public Sent WithCancellation(CancellationToken cancellationToken)
            {
                return new Sent(asyncOperation, cancellationToken);
            }

            public SentWithProgress<TProgress> WithProgress<TProgress>(TProgress progress)
                where TProgress : IProgress<float>
            {
                return new SentWithProgress<TProgress>(asyncOperation, progress, cancellationToken);
            }

            public Sent<TResponse> CastResponse<TResponse>()
            {
                return new Sent<TResponse>(asyncOperation, cancellationToken);
            }

            public async UniTask<AlgoApiResponse> ToUniTask()
            {
                return await this;
            }

            public Awaiter GetAwaiter()
            {
                var uniTaskAwaiter = asyncOperation
                    .ToUniTask(cancellationToken: cancellationToken)
                    .GetAwaiter();
                return new Awaiter(uniTaskAwaiter);
            }
        }

        /// <summary>
        /// A wrapper around the <see cref="UnityWebRequestAsyncOperation"/> handling the sent request.
        /// </summary>
        public readonly struct Sent<TResponse>
        {
            private readonly UnityWebRequestAsyncOperation asyncOperation;
            private readonly CancellationToken cancellationToken;

            public UnityWebRequestAsyncOperation AsyncOperation => asyncOperation;

            public Sent(
                UnityWebRequestAsyncOperation asyncOperation,
                CancellationToken cancellationToken = default
            )
            {
                this.asyncOperation = asyncOperation;
                this.cancellationToken = cancellationToken;
            }

            public Sent<TResponse> WithCancellation(CancellationToken cancellationToken)
            {
                return new Sent<TResponse>(asyncOperation, cancellationToken);
            }

            public SentWithProgress<TResponse, TProgress> WithProgress<TProgress>(TProgress progress)
                where TProgress : IProgress<float>
            {
                return new SentWithProgress<TResponse, TProgress>(asyncOperation, progress, cancellationToken);
            }

            public Awaiter<TResponse> GetAwaiter()
            {
                var uniTaskAwaiter = asyncOperation
                    .ToUniTask(cancellationToken: cancellationToken)
                    .GetAwaiter();
                return new Awaiter<TResponse>(uniTaskAwaiter);
            }

            public async UniTask<AlgoApiResponse<TResponse>> ToUniTask()
            {
                return await this;
            }

            public static implicit operator Sent<TResponse>(Sent sent)
            {
                return sent.CastResponse<TResponse>();
            }
        }

        /// <summary>
        /// A wrapper around the <see cref="UnityWebRequestAsyncOperation"/> handling the sent request.
        /// </summary>
        public readonly struct SentWithProgress<TProgress>
            where TProgress : IProgress<float>
        {
            private readonly UnityWebRequestAsyncOperation asyncOperation;
            private readonly TProgress progress;
            private readonly CancellationToken cancellationToken;

            public UnityWebRequestAsyncOperation AsyncOperation => asyncOperation;

            public SentWithProgress(
                UnityWebRequestAsyncOperation asyncOperation,
                TProgress progress,
                CancellationToken cancellationToken = default
            )
            {
                this.asyncOperation = asyncOperation;
                this.progress = progress;
                this.cancellationToken = cancellationToken;
            }

            public SentWithProgress<TProgress> WithCancellation(CancellationToken cancellationToken)
            {
                return new SentWithProgress<TProgress>(asyncOperation, progress, cancellationToken);
            }

            public SentWithProgress<TResponse, TProgress> CastResponse<TResponse>()
            {
                return new SentWithProgress<TResponse, TProgress>(asyncOperation, progress, cancellationToken);
            }

            public Awaiter GetAwaiter()
            {
                var uniTaskAwaiter = asyncOperation
                    .ToUniTask(progress: progress, cancellationToken: cancellationToken)
                    .GetAwaiter();
                return new Awaiter(uniTaskAwaiter);
            }

            public async UniTask<AlgoApiResponse> ToUniTask()
            {
                return await this;
            }
        }

        /// <summary>
        /// A wrapper around the <see cref="UnityWebRequestAsyncOperation"/> handling the sent request.
        /// </summary>
        public readonly struct SentWithProgress<TResponse, TProgress>
            where TProgress : IProgress<float>
        {
            private readonly UnityWebRequestAsyncOperation asyncOperation;
            private readonly TProgress progress;
            private readonly CancellationToken cancellationToken;

            public UnityWebRequestAsyncOperation AsyncOperation => asyncOperation;

            public SentWithProgress(
                UnityWebRequestAsyncOperation asyncOperation,
                TProgress progress,
                CancellationToken cancellationToken = default
            )
            {
                this.asyncOperation = asyncOperation;
                this.progress = progress;
                this.cancellationToken = cancellationToken;
            }

            public SentWithProgress<TResponse, TProgress> WithCancellation(CancellationToken cancellationToken)
            {
                return new SentWithProgress<TResponse, TProgress>(asyncOperation, progress, cancellationToken);
            }

            public Awaiter<TResponse> GetAwaiter()
            {
                var uniTaskAwaiter = asyncOperation
                    .ToUniTask(progress: progress, cancellationToken: cancellationToken)
                    .GetAwaiter();
                return new Awaiter<TResponse>(uniTaskAwaiter);
            }

            public async UniTask<AlgoApiResponse<TResponse>> ToUniTask()
            {
                return await this;
            }

            public static implicit operator SentWithProgress<TResponse, TProgress>(SentWithProgress<TProgress> sent)
            {
                return sent.CastResponse<TResponse>();
            }
        }

        /// <summary>
        /// A Task Awaiter for <see cref="UnityWebRequest"/>
        /// </summary>
        public readonly struct Awaiter
            : ICriticalNotifyCompletion
        {
            private readonly UniTask<UnityWebRequest>.Awaiter uniTaskAwaiter;

            public Awaiter(UniTask<UnityWebRequest>.Awaiter uniTaskAwaiter)
            {
                this.uniTaskAwaiter = uniTaskAwaiter;
            }

            public bool IsCompleted => uniTaskAwaiter.IsCompleted;

            public AlgoApiResponse GetResult()
            {
                UnityWebRequest completedRequest;
                try
                {
                    completedRequest = uniTaskAwaiter.GetResult();
                }
                catch (UnityWebRequestException webErr)
                {
                    completedRequest = webErr.UnityWebRequest;
                }
                return new AlgoApiResponse(completedRequest);
            }

            public void OnCompleted(Action continuation)
            {
                UnsafeOnCompleted(continuation);
            }

            public void UnsafeOnCompleted(Action continuation)
            {
                uniTaskAwaiter.UnsafeOnCompleted(continuation);
            }
        }


        /// <summary>
        /// A Task Awaiter for <see cref="UnityWebRequest"/>
        /// </summary>
        public readonly struct Awaiter<TResponse>
            : ICriticalNotifyCompletion
        {
            private readonly UniTask<UnityWebRequest>.Awaiter uniTaskAwaiter;

            public Awaiter(UniTask<UnityWebRequest>.Awaiter uniTaskAwaiter)
            {
                this.uniTaskAwaiter = uniTaskAwaiter;
            }

            public bool IsCompleted => uniTaskAwaiter.IsCompleted;

            public AlgoApiResponse<TResponse> GetResult()
            {
                UnityWebRequest completedRequest;
                try
                {
                    completedRequest = uniTaskAwaiter.GetResult();
                }
                catch (UnityWebRequestException webErr)
                {
                    completedRequest = webErr.UnityWebRequest;
                }
                return new AlgoApiResponse(completedRequest);
            }

            public void OnCompleted(Action continuation)
            {
                UnsafeOnCompleted(continuation);
            }

            public void UnsafeOnCompleted(Action continuation)
            {
                uniTaskAwaiter.UnsafeOnCompleted(continuation);
            }
        }
    }
}
