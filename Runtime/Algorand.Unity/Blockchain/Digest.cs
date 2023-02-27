using System;
using System.Runtime.InteropServices;
using Algorand.Unity.Crypto;
using Algorand.Unity.Formatters;
using Algorand.Unity.LowLevel;

namespace Algorand.Unity
{
    [Serializable]
    [AlgoApiFormatter(typeof(ByteArrayFormatter<Digest>))]
    [StructLayout(LayoutKind.Explicit, Size = SizeBytes)]
    public partial struct Digest
        : IByteArray
        , IEquatable<Digest>
    {
        public const int SizeBytes = 32;

        [FieldOffset(0)] internal byte buffer;

        public unsafe void* GetUnsafePtr()
        {
            fixed (byte* b = &buffer)
                return b;
        }

        public int Length => SizeBytes;

        public byte this[int index]
        {
            get => this.GetByteAt(index);
            set => this.SetByteAt(index, value);
        }

        public bool Equals(Digest other)
        {
            return ByteArray.Equals(this, other);
        }

        public static implicit operator Sha512_256_Hash(Digest digest)
        {
            var result = new Sha512_256_Hash();
            digest.CopyTo(ref result);
            return result;
        }

        public static implicit operator Digest(Sha512_256_Hash hash)
        {
            var result = new Digest();
            hash.CopyTo(ref result);
            return result;
        }
    }
}
