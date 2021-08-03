using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace AlgoSdk.LowLevel
{
    public struct NativeByteArray
        : IByteArray
        , IEquatable<NativeByteArray>
        , INativeDisposable
    {
        public byte this[int index] { get => this.GetByteAt(index); set => this.SetByteAt(index, value); }

        public unsafe IntPtr Buffer => (IntPtr)data.GetUnsafePtr();

        public int Length => data.Length;

        readonly NativeArray<byte> data;

        public NativeByteArray(int capacity, Allocator allocator)
        {
            data = new NativeArray<byte>(capacity, allocator);
        }

        public NativeByteArray(byte[] arr, Allocator allocator)
        {
            data = new NativeArray<byte>(arr, allocator);
        }

        public bool Equals(NativeByteArray other)
        {
            return this.data.Equals(other.data);
        }

        public JobHandle Dispose(JobHandle inputDeps)
        {
            return data.Dispose(inputDeps);
        }

        public void Dispose()
        {
            data.Dispose();
        }
    }
}
