using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace Algorand.Unity.LowLevel
{
    /// <summary>
    /// A wrapper around <see cref="NativeArray{byte}"/> that implements <see cref="IByteArray"/>.
    /// </summary>
    public struct NativeByteArray
        : IByteArray
            , IEquatable<NativeByteArray>
            , INativeDisposable
    {
        public byte this[int index]
        {
            get => this.GetByteAt(index);
            set => this.SetByteAt(index, value);
        }

        public int Length => data.Length;

        public NativeArray<byte>.ReadOnly Data => data.AsReadOnly();

        private readonly NativeArray<byte> data;

        public NativeByteArray(int capacity, Allocator allocator)
        {
            data = new NativeArray<byte>(capacity, allocator);
        }

        public NativeByteArray(byte[] arr, Allocator allocator)
        {
            data = new NativeArray<byte>(arr, allocator);
        }

        public NativeByteArray(NativeArray<byte> data)
        {
            this.data = data;
        }

        public unsafe void* GetUnsafePtr() => data.GetUnsafePtr();

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