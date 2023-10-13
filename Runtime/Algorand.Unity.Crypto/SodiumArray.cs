using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace Algorand.Unity.Crypto
{
    [StructLayout(LayoutKind.Sequential)]
    public struct SodiumArray<T> : INativeDisposable
        where T : unmanaged
    {
        private readonly SecureMemoryHandle handle;
        private readonly int length;

        public ref T this[int index]
        {
            get
            {
                CheckIndexRange(index);
                unsafe
                {
                    return ref UnsafeUtility.ArrayElementAsRef<T>(handle.GetUnsafePtr(), index);
                }
            }
        }

        public int Length => length;

        public bool IsCreated => handle.IsCreated;

        public SodiumArray(int length)
        {
            this.length = length;
            var ptr = sodium.sodium_allocarray((UIntPtr)length, (UIntPtr)UnsafeUtility.SizeOf<T>());
            handle = new SecureMemoryHandle(ptr);
        }

        public unsafe void* GetUnsafePtr() => handle.GetUnsafePtr();

        public JobHandle Dispose(JobHandle inputDeps)
        {
            return handle.Dispose(inputDeps);
        }

        public Span<byte> AsByteSpan()
        {
            unsafe
            {
                return new Span<byte>(handle.GetUnsafePtr(), length * UnsafeUtility.SizeOf<T>());
            }
        }

        public Span<T> AsSpan()
        {
            unsafe
            {
                return new Span<T>(handle.GetUnsafePtr(), length);
            }
        }

        public void Dispose()
        {
            handle.Dispose();
        }

        [Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
        private void CheckIndexRange(int index)
        {
            if (index < 0 || index >= length)
                throw new IndexOutOfRangeException(nameof(index));
        }
    }
}
