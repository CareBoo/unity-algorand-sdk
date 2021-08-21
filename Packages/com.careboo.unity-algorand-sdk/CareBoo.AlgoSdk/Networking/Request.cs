using System;
using System.Runtime.CompilerServices;
using Cysharp.Threading.Tasks.Internal;
using UnityEngine;
using UnityEngine.Networking;

namespace AlgoSdk
{
    public readonly struct Request
    {
        readonly UnityWebRequest unityWebRequest;

        public Request(UnityWebRequest webRequest)
        {
            this.unityWebRequest = webRequest;
        }

        public ref struct Sent
        {
            UnityWebRequestAsyncOperation asyncOperation;

            public Sent(UnityWebRequestAsyncOperation asyncOperation)
            {
                this.asyncOperation = asyncOperation;
            }

            public Awaiter GetAwaiter()
            {
                return new Awaiter(asyncOperation);
            }
        }

        public struct Awaiter : ICriticalNotifyCompletion
        {
            UnityWebRequestAsyncOperation asyncOperation;
            Action<AsyncOperation> continuationAction;

            public Awaiter(UnityWebRequestAsyncOperation asyncOperation)
            {
                this.asyncOperation = asyncOperation;
                this.continuationAction = null;
            }

            public bool IsCompleted => asyncOperation.isDone;

            public Response GetResult()
            {
                if (continuationAction != null)
                {
                    asyncOperation.completed -= continuationAction;
                    continuationAction = null;
                }
                var webRequest = asyncOperation.webRequest;
                asyncOperation = null;
                return webRequest.GetResponse();
            }

            public void OnCompleted(Action continuation)
            {
                UnsafeOnCompleted(continuation);
            }

            public void UnsafeOnCompleted(Action continuation)
            {
                Error.ThrowWhenContinuationIsAlreadyRegistered(continuationAction);
                continuationAction = PooledDelegate<AsyncOperation>.Create(continuation);
                asyncOperation.completed += continuationAction;
            }
        }

        public Sent Send()
        {
            return new Sent(unityWebRequest.SendWebRequest());
        }

        public Awaiter GetAwaiter()
        {
            return new Awaiter(unityWebRequest.SendWebRequest());
        }
    }

    public readonly struct Request<T>
        where T : unmanaged
    {
        readonly UnityWebRequest unityWebRequest;

        private Request(UnityWebRequest webRequest)
        {
            unityWebRequest = webRequest;
        }

        public ref struct Sent
        {
            UnityWebRequestAsyncOperation asyncOperation;

            public Sent(UnityWebRequestAsyncOperation asyncOperation)
            {
                this.asyncOperation = asyncOperation;
            }

            public Awaiter GetAwaiter()
            {
                return new Awaiter(asyncOperation);
            }
        }

        public struct Awaiter : ICriticalNotifyCompletion
        {
            UnityWebRequestAsyncOperation asyncOperation;
            Action<AsyncOperation> continuationAction;

            public Awaiter(UnityWebRequestAsyncOperation asyncOperation)
            {
                this.asyncOperation = asyncOperation;
                this.continuationAction = null;
            }

            public bool IsCompleted => asyncOperation.isDone;

            public Response<T> GetResult()
            {
                if (continuationAction != null)
                {
                    asyncOperation.completed -= continuationAction;
                    continuationAction = null;
                }
                var webRequest = asyncOperation.webRequest;
                asyncOperation = null;
                return webRequest.GetMessagePackResponse<T>();
            }

            public void OnCompleted(Action continuation)
            {
                UnsafeOnCompleted(continuation);
            }

            public void UnsafeOnCompleted(Action continuation)
            {
                asyncOperation.completed += continuationAction;
            }
        }

        public Sent Send()
        {
            return new Sent(unityWebRequest.SendWebRequest());
        }
    }
}
