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

        public AlgoApiRequest SetJsonBody(string json) =>
            SetRawBody(Encoding.UTF8.GetBytes(json), ContentType.Json);

        public AlgoApiRequest SetJsonBody(NativeText json) =>
            SetRawBody(json.AsArray().ToArray(), ContentType.Json);

        public AlgoApiRequest SetJsonBody<TBody>(TBody value)
        {
            using var json = AlgoApiSerializer.SerializeJson(value, Allocator.Temp);
            return SetJsonBody(json);
        }

        public AlgoApiRequest SetPlainTextBody(string plainText) =>
            SetRawBody(Encoding.UTF8.GetBytes(plainText), ContentType.PlainText);

        public AlgoApiRequest SetMessagePackBody(NativeArray<byte>.ReadOnly bytes) =>
            SetRawBody(bytes.ToArray(), ContentType.MessagePack);

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

        public static AlgoApiRequest Delete(string url)
        {
            var webRequest = UnityWebRequest.Delete(url);
            return new AlgoApiRequest(ref webRequest);
        }

        static UnityWebRequest UnityWebRequestPostWithoutBody(string url)
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
