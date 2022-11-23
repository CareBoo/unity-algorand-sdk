using Algorand.Unity.LowLevel;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Jobs;

namespace Algorand.Unity.Experimental.Abi
{
    /// <summary>
    /// Represents the result of encoding an <see cref="IAbiValue"/>
    /// following the specification of an <see cref="AbiType"/>.
    /// </summary>
    public struct EncodedAbiArg
        : IIndexable<byte>
        , IByteArray
        , INativeDisposable
    {
        private NativeList<byte> bytes;

        public EncodedAbiArg(
            int capacity,
            Allocator allocator
            )
        {
            this.bytes = new NativeList<byte>(capacity, allocator);
        }

        public EncodedAbiArg(Allocator allocator)
        {
            this.bytes = new NativeList<byte>(allocator);
        }

        /// <summary>
        /// The raw bytes backing this struct.
        /// </summary>
        public NativeList<byte> Bytes => bytes;

        /// <inheritdoc />
        public byte this[int i]
        {
            get => ElementAt(i);
            set => ElementAt(i) = value;
        }

        /// <inheritdoc />
        public int Length
        {
            get => bytes.Length;
            set => bytes.Length = value;
        }

        /// <inheritdoc />
        public ref byte ElementAt(int index)
        {
            return ref bytes.ElementAt(index);
        }

        /// <inheritdoc />
        public JobHandle Dispose(JobHandle inputDeps)
        {
            return bytes.Dispose(inputDeps);
        }

        /// <inheritdoc />
        public void Dispose()
        {
            bytes.Dispose();
        }

        /// <inheritdoc />
        public unsafe void* GetUnsafePtr()
        {
            return bytes.GetUnsafeReadOnlyPtr();
        }
    }
}
