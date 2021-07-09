using System;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

namespace AlgoSdk
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
    [BurstCompatible]
    public struct Key
    : IEquatable<Key>
    , IByteArray
    {
        [SerializeField] [FieldOffset(0)] internal FixedBytes32 buffer;

        unsafe internal byte* Buffer
        {
            get
            {
                fixed (byte* b = &buffer.offset0000.byte0000)
                    return b;
            }
        }

        public int Length => 32;

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
            var result = new Mnemonic();
            var numBits = 0;
            var buffer = 0;
            var currentIndex = 0;
            for (var i = 0; i < Length; i++)
            {
                buffer |= this[i] << numBits;
                numBits += 8;
                if (numBits >= 11)
                {
                    result[currentIndex] = (Mnemonic.Word)(buffer & ((ushort)Mnemonic.Word.LENGTH - 1));
                    currentIndex++;
                    buffer >>= 11;
                    numBits -= 11;
                }
            }
            if (numBits != 0)
            {

            }
            return result;
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
