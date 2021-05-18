using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

namespace Algorand
{
    [Serializable]
    [StructLayout(LayoutKind.Explicit, Size = 32)]
    internal struct FixedBytes32
    {
        [FieldOffset(0)] internal FixedBytes16 offset0000;
        [FieldOffset(16)] internal FixedBytes16 offset0016;
    }

    [Serializable]
    [StructLayout(LayoutKind.Explicit, Size = 32)]
    public struct Key
    : IEquatable<Key>
    {
        public const int Length = 32;
        [SerializeField] [FieldOffset(0)] internal FixedBytes32 buffer;

        unsafe internal byte* Buffer
        {
            get
            {
                fixed (byte* b = &buffer.offset0000.byte0000)
                    return b;
            }
        }

        public byte this[int index]
        {
            get
            {
                ByteArray.CheckElementAccess(index, Length);
                unsafe
                {
                    return UnsafeUtility.ReadArrayElement<byte>(Buffer, index);
                }
            }
            set
            {
                ByteArray.CheckElementAccess(index, Length);
                unsafe
                {
                    UnsafeUtility.WriteArrayElement<byte>(Buffer, index, value);
                }
            }
        }

        public Mnemonic ToMnemonic()
        {
            throw new NotImplementedException();
        }

        public bool Equals(Key other)
        {
            for (var i = 0; i < Length; i++)
                if (!this[i].Equals(other[i]))
                    return false;
            return true;
        }
    }
}
