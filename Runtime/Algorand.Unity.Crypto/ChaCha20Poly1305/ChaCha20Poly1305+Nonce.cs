using System;
using System.Runtime.InteropServices;
using Algorand.Unity.LowLevel;

namespace Algorand.Unity.Crypto
{
    public static partial class ChaCha20Poly1305
    {
        [Serializable]
        [StructLayout(LayoutKind.Explicit, Size = SizeBytes)]
        public struct Nonce : IByteArray, IEquatable<Nonce>
        {
            public const int SizeBytes = 12;
            public const int SizeUints = SizeBytes / 4;

            [FieldOffset(0)]
            internal unsafe fixed uint buffer[SizeUints];

            public int Length => SizeBytes;

            public byte this[int index]
            {
                get => this.GetByteAt(index);
                set => this.SetByteAt(index, value);
            }

            public unsafe void* GetUnsafePtr()
            {
                fixed (uint* b = buffer)
                {
                    return b;
                }
            }

            public bool Equals(Nonce other)
            {
                return ByteArray.Equals(this, other);
            }

            public static Nonce Create()
            {
                return Random.Bytes<Nonce>();
            }

            public Nonce Next()
            {
                return this + 1;
            }

            public static Nonce operator +(Nonce nonce, uint increment)
            {
                const int uintBits = sizeof(uint) * 8;
                ulong sum = increment;
                unsafe
                {
                    for (var i = 0; i < SizeUints; i++)
                    {
                        sum += nonce.buffer[i];
                        nonce.buffer[i] = (uint)(sum & 0xFFFFFFFF);
                        sum >>= uintBits;
                    }
                }

                return nonce;
            }
        }
    }
}