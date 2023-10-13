using System;
using System.Runtime.InteropServices;
using Algorand.Unity.LowLevel;
using Unity.Collections;
using UnityEngine;

namespace Algorand.Unity.Crypto
{
    public static partial class Ed25519
    {
        [Serializable]
        [StructLayout(LayoutKind.Explicit, Size = SizeBytes)]
        public struct PublicKey
            : IByteArray
                , IEquatable<PublicKey>
        {
            public const int SizeBytes = 32;

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

            public int Length => 32;

            public byte this[int index]
            {
                get => this.GetByteAt(index);
                set => this.SetByteAt(index, value);
            }

            public bool Equals(PublicKey other)
            {
                for (var i = 0; i < Length; i++)
                    if (this[i] != other[i])
                        return false;
                return true;
            }

            public override bool Equals(object obj)
            {
                return ByteArray.Equals(this, obj);
            }

            public override int GetHashCode()
            {
                return ByteArray.GetHashCode(this);
            }

            public static bool operator ==(in PublicKey x, in PublicKey y)
            {
                return ByteArray.Equals(x, y);
            }

            public static bool operator !=(in PublicKey x, in PublicKey y)
            {
                return !ByteArray.Equals(x, y);
            }
        }
    }
}