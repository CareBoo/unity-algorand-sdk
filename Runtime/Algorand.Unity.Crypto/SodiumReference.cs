using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace Algorand.Unity.Crypto
{
    public readonly struct SodiumReference<T> : INativeDisposable
        where T : unmanaged
    {
        private readonly SecureMemoryHandle handle;

        public unsafe T* GetUnsafePtr() => (T*)handle.GetUnsafePtr();

        public unsafe T Value
        {
            get => *GetUnsafePtr();
            set => *GetUnsafePtr() = value;
        }

        public unsafe ref T RefValue => ref *GetUnsafePtr();

        public bool IsCreated => handle.IsCreated;

        public SodiumReference(SecureMemoryHandle handle)
        {
            this.handle = handle;
        }

        public unsafe Span<byte> AsSpan()
        {
            return new Span<byte>(handle.GetUnsafePtr(), UnsafeUtility.SizeOf<T>());
        }

        public JobHandle Dispose(JobHandle inputDeps)
        {
            return handle.Dispose(inputDeps);
        }

        public void Dispose()
        {
            handle.Dispose();
        }

        public static SodiumReference<T> Alloc()
        {
            var handle = SecureMemoryHandle.Create((UIntPtr)UnsafeUtility.SizeOf<T>());
            return new SodiumReference<T>(handle);
        }
    }
}
