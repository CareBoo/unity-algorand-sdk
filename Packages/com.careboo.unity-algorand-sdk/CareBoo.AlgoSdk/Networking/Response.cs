
using Unity.Collections;
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

        public static implicit operator Response<T>(NativeReference<T> message)
        {
            return new Response<T>(ref message);
        }

        public static implicit operator Response<T>(ErrorResponse error)
        {
            return new Response<T>(ref error);
        }
    }

    public struct Response
        : INativeDisposable
    {
        public NativeText Message;
        public ErrorResponse Error;

        public Response(ref NativeText message)
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
            if (Message.IsCreated)
                Message.Dispose();
            Error.Dispose();
        }

        public static implicit operator Response(NativeText message)
        {
            return new Response(ref message);
        }

        public static implicit operator Response(ErrorResponse error)
        {
            return new Response(ref error);
        }
    }
}
