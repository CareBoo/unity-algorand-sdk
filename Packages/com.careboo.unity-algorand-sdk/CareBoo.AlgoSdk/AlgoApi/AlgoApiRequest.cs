using System;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using Cysharp.Threading.Tasks;
using Unity.Collections;
using UnityEngine.Networking;
using static Cysharp.Threading.Tasks.UnityAsyncExtensions;

namespace AlgoSdk
{
    public readonly ref struct AlgoApiRequest
    {
        public const string ContentTypeHeader = "Content-Type";
        readonly UnityWebRequest unityWebRequest;

        private AlgoApiRequest(ref UnityWebRequest unityWebRequest)
        {
            this.unityWebRequest = unityWebRequest;
        }

        public AlgoApiRequest SetToken(string tokenHeader, string token)
        {
            if (!string.IsNullOrEmpty(token))
                unityWebRequest.SetRequestHeader(tokenHeader, token);
            return this;
        }

        public Sent Send(CancellationToken cancellationToken = default)
        {
            return new Sent(unityWebRequest.SendWebRequest(), cancellationToken);
        }

        public Sent<TProgress> Send<TProgress>(TProgress progress, CancellationToken cancellationToken = default)
            where TProgress : IProgress<float>
        {
            return new Sent<TProgress>(unityWebRequest.SendWebRequest(), progress, cancellationToken);
        }

        public static AlgoApiRequest Get(string url)
        {
            var webRequest = UnityWebRequest.Get(url);
            return new AlgoApiRequest(ref webRequest);
        }

        public static AlgoApiRequest Post(string url)
        {
            var webRequest = UnityWebRequestPostWithoutBody(url);
            return new AlgoApiRequest(ref webRequest);
        }

        public static AlgoApiRequest Post(string url, byte[] postData)
        {
            var webRequest = UnityWebRequestPostWithoutBody(url);
            webRequest.uploadHandler = new UploadHandlerRaw(postData);
            webRequest.disposeUploadHandlerOnDispose = true;
            webRequest.SetRequestHeader(ContentTypeHeader, "application/x-binary");
            return new AlgoApiRequest(ref webRequest);
        }

        public static AlgoApiRequest Post(string url, string postData)
        {
            var webRequest = UnityWebRequest.Post(url, postData);
            webRequest.SetRequestHeader(ContentTypeHeader, "text/plain");
            return new AlgoApiRequest(ref webRequest);
        }

        public static AlgoApiRequest Post(string url, NativeText json)
        {
            var webRequest = UnityWebRequest.Post(url, json.ToString());
            webRequest.SetRequestHeader(ContentTypeHeader, "application/json");
            return new AlgoApiRequest(ref webRequest);
        }

        public static AlgoApiRequest Delete(string url)
        {
            var webRequest = UnityWebRequest.Delete(url);
            return new AlgoApiRequest(ref webRequest);
        }

        public static AlgoApiRequest Delete(string url, NativeText json)
        {
            var jsonBytes = Encoding.UTF8.GetBytes(json.ToString());
            var webRequest = UnityWebRequest.Delete(url);
            webRequest.uploadHandler = new UploadHandlerRaw(jsonBytes);
            webRequest.disposeUploadHandlerOnDispose = true;
            webRequest.SetRequestHeader(ContentTypeHeader, "application/json");
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

        public ref struct Sent
        {
            UnityWebRequestAsyncOperation asyncOperation;
            CancellationToken cancellationToken;

            public Sent(
                UnityWebRequestAsyncOperation asyncOperation,
                CancellationToken cancellationToken = default
            )
            {
                this.asyncOperation = asyncOperation;
                this.cancellationToken = cancellationToken;
            }

            public Awaiter GetAwaiter()
            {
                var uniTaskAwaiter = asyncOperation
                    .ToUniTask(cancellationToken: cancellationToken)
                    .GetAwaiter();
                return new Awaiter(uniTaskAwaiter);
            }
        }

        public ref struct Sent<TProgress> where TProgress : IProgress<float>
        {
            UnityWebRequestAsyncOperation asyncOperation;
            TProgress progress;
            CancellationToken cancellationToken;

            public Sent(
                UnityWebRequestAsyncOperation asyncOperation,
                TProgress progress,
                CancellationToken cancellationToken = default
            )
            {
                this.asyncOperation = asyncOperation;
                this.progress = progress;
                this.cancellationToken = cancellationToken;
            }

            public Awaiter GetAwaiter()
            {
                var uniTaskAwaiter = asyncOperation
                    .ToUniTask(progress: progress, cancellationToken: cancellationToken)
                    .GetAwaiter();
                return new Awaiter(uniTaskAwaiter);
            }
        }

        public readonly struct Awaiter : ICriticalNotifyCompletion
        {
            readonly UniTask<UnityWebRequest>.Awaiter uniTaskAwaiter;

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
                return new AlgoApiResponse(ref completedRequest);
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
