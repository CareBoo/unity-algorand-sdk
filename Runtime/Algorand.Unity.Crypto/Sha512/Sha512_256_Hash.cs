using System;
using System.Runtime.InteropServices;
using Algorand.Unity.LowLevel;
using Unity.Collections;
using UnityEngine;

namespace Algorand.Unity.Crypto
{
    [StructLayout(LayoutKind.Explicit, Size = SizeBytes)]
    [Serializable]
    public struct Sha512_256_Hash
        : IByteArray
            , IEquatable<Sha512_256_Hash>
    {
        public const int SizeBytes = 256 / 8;

        [FieldOffset(0)]
        [SerializeField]
        internal FixedBytes16 offset0000;

        [FieldOffset(16)]
        [SerializeField]
        internal FixedBytes16 offset0016;

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

        public bool Equals(Sha512_256_Hash other)
        {
            return ByteArray.Equals(this, other);
        }

        public static bool operator ==(in Sha512_256_Hash x, in Sha512_256_Hash y)
        {
            return ByteArray.Equals(x, y);
        }

        public static bool operator !=(in Sha512_256_Hash x, in Sha512_256_Hash y)
        {
            return !ByteArray.Equals(x, y);
        }

        public override bool Equals(object obj)
        {
            return ByteArray.Equals(this, obj);
        }

        public override int GetHashCode()
        {
            return ByteArray.GetHashCode(this);
        }
    }
}