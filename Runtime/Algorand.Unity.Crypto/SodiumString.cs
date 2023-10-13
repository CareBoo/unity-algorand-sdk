using System;
using System.Diagnostics;
using Unity.Collections;
using Unity.Jobs;

namespace Algorand.Unity.Crypto
{
    public struct SodiumString : INativeDisposable
    {
        private readonly SecureMemoryHandle handle;

        private readonly int totalCapacity;

        private int length;

        public bool IsCreated => handle.IsCreated;

        public int Length => length;

        public int Capacity => totalCapacity - 1;

        public unsafe byte* GetUnsafePtr()
        {
            return (byte*)handle.GetUnsafePtr();
        }

        public SodiumString(ReadOnlySpan<char> source)
        {
            totalCapacity = source.Length * sizeof(char);
            var totalCapacityNUint = (UIntPtr)totalCapacity;
            handle = SecureMemoryHandle.Create(totalCapacityNUint);
            unsafe
            {
                fixed (char* sourceptr = source)
                {
                    var error = UTF8ArrayUnsafeUtility.Copy(
                        handle.GetUnsafePtr(),
                        out length,
                        totalCapacity - 1,
                        sourceptr,
                        source.Length
                    );
                    if (error != CopyError.None)
                    {
                        if (handle.IsCreated) handle.Dispose();
                        length = 0;
                        ThrowCopyError(error, source);
                    }
                }
            }
        }

        public JobHandle Dispose(JobHandle inputDeps)
        {
            return handle.Dispose(inputDeps);
        }

        public void Dispose()
        {
            handle.Dispose();
        }

        [Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
        void ThrowCopyError(CopyError error, ReadOnlySpan<char> source)
        {
            throw new ArgumentException($"NativeText: {error} while copying \"{source.ToString()}\"");
        }
    }
}
