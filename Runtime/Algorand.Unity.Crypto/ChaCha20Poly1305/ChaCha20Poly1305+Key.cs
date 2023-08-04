using System;
using System.Runtime.InteropServices;
using Algorand.Unity.LowLevel;

namespace Algorand.Unity.Crypto
{
    public static partial class ChaCha20Poly1305
    {
        [Serializable]
        [StructLayout(LayoutKind.Explicit, Size = Size)]
        public struct Key : IByteArray, IEquatable<Key>
        {
            public const int Size = 32;

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
                fixed (ulong* b = buffer)
                {
                    return b;
                }
            }

            public bool Equals(Key other)
            {
                return ByteArray.Equals(this, other);
            }
        }
    }
}