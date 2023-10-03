using System;
using System.Runtime.InteropServices;
using Algorand.Unity.LowLevel;
using Unity.Collections;

namespace Algorand.Unity.Crypto
{
    [StructLayout(LayoutKind.Explicit, Size = SizeBytes)]
    public struct Sha512_Hash
        : IByteArray
            , IEquatable<Sha512_Hash>
    {
        public const int SizeBytes = 512 / 8;

        [FieldOffset(0)]
        internal FixedBytes16 offset0000;

        [FieldOffset(16)]
        internal FixedBytes16 offset0016;

        [FieldOffset(32)]
        internal FixedBytes16 offset0032;

        [FieldOffset(48)]
        internal FixedBytes16 offset0048;

        public unsafe void* GetUnsafePtr()
        {
            fixed (byte* b = &offset0000.byte0000)
            {
                return b;
            }
        }

        public int Length => SizeBytes;

        public byte this[int index]
        {
            get => this.GetByteAt(index);
            set => this.SetByteAt(index, value);
        }

        public bool Equals(Sha512_Hash other)
        {
            return ByteArray.Equals(this, other);
        }
    }
}