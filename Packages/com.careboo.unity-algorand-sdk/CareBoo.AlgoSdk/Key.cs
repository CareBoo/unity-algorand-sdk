using System;
using System.Runtime.InteropServices;
using System.Text;
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

        public const int Length = 32;

        int IByteArray.Length => Length;

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
            var wordIndex = 0;
            for (var i = 0; i < Length; i++)
            {
                buffer |= this[i] << numBits;
                numBits += 8;
                if (numBits >= 11)
                {
                    result[wordIndex] = (Mnemonic.Word)(buffer & ((ushort)Mnemonic.Word.LENGTH - 1));
                    wordIndex++;
                    buffer >>= 11;
                    numBits -= 11;
                }
            }
            if (numBits != 0)
            {
                result[wordIndex] = (Mnemonic.Word)(buffer & ((ushort)Mnemonic.Word.LENGTH - 1));
            }
            return result;
        }

        public Address ToAddress()
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

        public static Key FromString(string keyString)
        {
            var key = new Key();
            var bytes = System.Convert.FromBase64String(keyString);
            for (var i = 0; i < Length; i++)
                key[i] = bytes[i];
            return key;
        }
    }
}
