using System;
using System.Runtime.InteropServices;
using Algorand.Unity.LowLevel;

namespace Algorand.Unity.Crypto
{
    public static partial class X25519
    {
        [Serializable]
        [StructLayout(LayoutKind.Explicit, Size = Size)]
        public struct PublicKey : IByteArray, IEquatable<PublicKey>
        {
            public const int Size = (int)sodium.crypto_box_PUBLICKEYBYTES;

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

            public bool Equals(PublicKey other)
            {
                return ByteArray.Equals(this, other);
            }
        }
    }
}
