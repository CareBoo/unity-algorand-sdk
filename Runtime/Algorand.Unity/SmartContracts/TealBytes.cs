using System;
using Algorand.Unity.Formatters;
using Algorand.Unity.LowLevel;

namespace Algorand.Unity
{
    [Serializable]
    [AlgoApiFormatter(typeof(ByteArrayFormatter<TealBytes>))]
    public partial struct TealBytes
        : IEquatable<TealBytes>
        , IByteArray
    {
        public const int SizeBytes = 128;
        public FixedBytes128 Bytes;
        public byte this[int index] { get => this.GetByteAt(index); set => this.SetByteAt(index, value); }

        public unsafe void* GetUnsafePtr()
        {
            fixed (byte* b = &Bytes.offset0000.offset0000.byte0000)
                return b;
        }

        public int Length => SizeBytes;

        public bool Equals(TealBytes other)
        {
            return ByteArray.Equals(this, other);
        }
    }
}
