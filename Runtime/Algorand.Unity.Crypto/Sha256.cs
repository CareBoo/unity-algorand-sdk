using System;
using System.Runtime.InteropServices;
using Algorand.Unity.LowLevel;

namespace Algorand.Unity.Crypto
{
    [Serializable]
    [StructLayout(LayoutKind.Explicit, Size = Size)]
    public struct Sha256 : IByteArray, IEquatable<Sha256>
    {
        public const int Size = 256 / 8;

        [FieldOffset(0)]
        internal unsafe fixed ulong buffer[Size / 8];

        public int Length => Size;

        public byte this[int index]
        {
            get => this.GetByteAt(index);
            set => this.SetByteAt(index, value);
        }

        public unsafe void* GetUnsafePtr()
        {
            fixed (void* b = buffer)
            {
                return b;
            }
        }

        public bool Equals(Sha256 other)
        {
            return ByteArray.Equals(this, other);
        }

        public unsafe static Sha256 Hash(ReadOnlySpan<byte> message)
        {
            fixed (byte* messagePtr = message)
            {
                var hash = default(Sha256);
                sodium.crypto_hash_sha256(&hash, messagePtr, (ulong)message.Length);
                return hash;
            }
        }
    }
}
