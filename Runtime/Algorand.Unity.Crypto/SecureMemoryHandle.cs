using System;
using Unity.Collections;
using Unity.Jobs;
using static Algorand.Unity.Crypto.sodium;

namespace Algorand.Unity.Crypto
{
    public struct SecureMemoryHandle
        : INativeDisposable
    {
#if (!UNITY_WEBGL || UNITY_EDITOR)
        static SecureMemoryHandle()
        {
            sodium_init();
        }
#endif

        public IntPtr Ptr;

        internal SecureMemoryHandle(IntPtr ptr)
        {
            Ptr = ptr;
        }

        public static SecureMemoryHandle Create(UIntPtr sizeBytes)
        {
            return sodium_malloc(sizeBytes);
        }

        public bool IsCreated => Ptr != IntPtr.Zero;

        public JobHandle Dispose(JobHandle inputDeps)
        {
            return new DisposeJob { keyHandle = this }.Schedule(dependsOn: inputDeps);
        }

        public void Dispose()
        {
            if (!IsCreated)
                return;

            sodium_free(Ptr);
            Ptr = IntPtr.Zero;
        }

        internal struct DisposeJob : IJob
        {
            public SecureMemoryHandle keyHandle;

            public void Execute()
            {
                keyHandle.Dispose();
            }
        }

        public static implicit operator IntPtr(SecureMemoryHandle handle)
        {
            return handle.Ptr;
        }

        public static implicit operator SecureMemoryHandle(IntPtr ptr)
        {
            return new SecureMemoryHandle(ptr);
        }
    }
}
