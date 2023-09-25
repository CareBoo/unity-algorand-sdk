using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;
using static Algorand.Unity.Crypto.sodium;

namespace Algorand.Unity.Crypto
{
    public struct SecureMemoryHandle
        : INativeDisposable
    {
        [NativeDisableUnsafePtrRestriction]
        private IntPtr ptr;

        public IntPtr Ptr => ptr;

        public unsafe byte* GetUnsafePtr()
        {
            return (byte*)ptr.ToPointer();
        }

        static SecureMemoryHandle()
        {
            sodium_init();
        }

        internal SecureMemoryHandle(IntPtr ptr)
        {
            this.ptr = ptr;
        }

        public bool IsCreated => Ptr != IntPtr.Zero;

        public JobHandle Dispose(JobHandle inputDeps)
        {
            return new DisposeJob { keyHandle = this }.Schedule(inputDeps);
        }

        public void Dispose()
        {
            if (!IsCreated)
                return;

            sodium_free(ptr);
            ptr = IntPtr.Zero;
        }

        public unsafe Span<byte> ToSpan(int length)
        {
            return new Span<byte>((void*)ptr, length);
        }

        public static SecureMemoryHandle Create(UIntPtr sizeBytes)
        {
            return sodium_malloc(sizeBytes);
        }

        public static implicit operator IntPtr(SecureMemoryHandle handle)
        {
            return handle.Ptr;
        }

        public static implicit operator SecureMemoryHandle(IntPtr ptr)
        {
            return new SecureMemoryHandle(ptr);
        }

        internal struct DisposeJob : IJob
        {
            public SecureMemoryHandle keyHandle;

            public void Execute()
            {
                keyHandle.Dispose();
            }
        }
    }
}
