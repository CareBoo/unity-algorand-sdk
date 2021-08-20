
using AlgoSdk.MsgPack;
using MessagePack;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace AlgoSdk
{
    public readonly struct Response<T>
        : INativeDisposable
        where T : unmanaged
    {
        public readonly NativeReference<T> Message;
        public readonly ErrorResponse Error;

        public Response(ref NativeReference<T> message)
        {
            Message = message;
            Error = default;
        }

        public Response(ref ErrorResponse error)
        {
            Message = default;
            Error = error;
        }

        public bool IsError => Error.Message.Length > 0;

        public JobHandle Dispose(JobHandle inputDeps)
        {
            return JobHandle.CombineDependencies(
                Message.Dispose(inputDeps),
                Error.Dispose(inputDeps)
            );
        }

        public void Dispose()
        {
            Message.Dispose();
            Error.Dispose();
        }
    }

    public struct Response
        : INativeDisposable
    {
        public NativeArray<byte> Message;
        public ErrorResponse Error;

        public Response(ref NativeArray<byte> message)
        {
            Message = message;
            Error = default;
        }

        public Response(ref ErrorResponse error)
        {
            Message = default;
            Error = error;
        }

        public bool IsError => Error.Message.Length > 0;

        public JobHandle Dispose(JobHandle inputDeps)
        {
            return JobHandle.CombineDependencies(
                Message.Dispose(inputDeps),
                Error.Dispose(inputDeps)
            );
        }

        public void Dispose()
        {
            Message.Dispose();
            Error.Dispose();
        }
    }

    public struct ErrorResponse
        : INativeDisposable
    {
        public UnsafeText Data;
        public UnsafeText Message;

        public JobHandle Dispose(JobHandle inputDeps)
        {
            return JobHandle.CombineDependencies(
                Data.Dispose(inputDeps),
                Message.Dispose(inputDeps)
            );
        }

        public void Dispose()
        {
            Data.Dispose();
            Message.Dispose();
        }
    }
}
