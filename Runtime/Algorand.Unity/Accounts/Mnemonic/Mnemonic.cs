using System;
using System.Runtime.InteropServices;
using Algorand.Unity.LowLevel;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;
using UnityEngine;

namespace Algorand.Unity
{
    [Serializable]
    [StructLayout(LayoutKind.Explicit, Size = 50)]
    internal struct FixedBytes50
    {
        [FieldOffset(0), SerializeField] internal FixedBytes16 offset0000;
        [FieldOffset(16), SerializeField] internal FixedBytes16 offset0016;
        [FieldOffset(32), SerializeField] internal FixedBytes16 offset0032;
        [FieldOffset(48), SerializeField] internal byte byte0048;
        [FieldOffset(49), SerializeField] internal byte byte0049;
    }

    /// <summary>
    /// Byte struct representing a private key encoded with <see cref="Length"/> words.
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Explicit, Size = 50)]
    public partial struct Mnemonic
        : IEquatable<Mnemonic>
    {
        [FieldOffset(0), SerializeField] internal FixedBytes50 buffer;

        /// <summary>
        /// Size of Mnemonic in bytes.
        /// </summary>
        public const int SizeBytes = 50;

        /// <summary>
        /// Number of words contained in the mnemonic.
        /// </summary>
        public const int Length = 25;

        /// <summary>
        /// Index of the word used for the checksum.
        /// </summary>
        public const int ChecksumIndex = Length - 1;

        /// <summary>
        /// The number of bits encoded by each word.
        /// </summary>
        public const int BitsPerWord = 11;

        unsafe internal byte* Buffer
        {
            get
            {
                fixed (byte* b = &buffer.offset0000.byte0000)
                    return b;
            }
        }

        /// <summary>
        /// Get the byte pointer at the start of this struct.
        /// </summary>
        /// <returns>An unsafe byte pointer.</returns>
        public unsafe byte* GetUnsafePtr() => Buffer;

        /// <summary>
        /// The word at a given index.
        /// </summary>
        /// <param name="index">The index of the word.</param>
        /// <returns>A word in the set of possible mnemonic words.</returns>
        public Word this[int index]
        {
            get
            {
                ByteArray.CheckElementAccess(index, Length);
                unsafe
                {
                    return UnsafeUtility.ReadArrayElement<Word>(Buffer, index);
                }
            }
            set
            {
                ByteArray.CheckElementAccess(index, Length);
                unsafe
                {
                    UnsafeUtility.WriteArrayElement<Word>(Buffer, index, value);
                }
            }
        }

        public override string ToString()
        {
            var words = new string[25];
            for (var i = 0; i < Length; i++)
                words[i] = this[i].ToString();
            return string.Join(" ", words);
        }

        /// <summary>
        /// Get the <see cref="PrivateKey"/> from this mnemonic encodes.
        /// </summary>
        public PrivateKey ToPrivateKey()
        {
            var result = new PrivateKey();
            var buffer = 0;
            var numBits = 0;
            var j = 0;
            for (var i = 0; i < ChecksumIndex; i++)
            {
                buffer |= ((ushort)this[i] << numBits);
                numBits += BitsPerWord;
                while (numBits >= 8 && j < 32)
                {
                    result[j] = (byte)(buffer & 0xff);
                    j++;
                    buffer >>= 8;
                    numBits -= 8;
                }
            }
            if (numBits != 0 && j < 32)
            {
                result[j] = (byte)(buffer & 0xff);
            }
            return result;
        }

        public bool Equals(Mnemonic other)
        {
            for (var i = 0; i < Length; i++)
                if ((ushort)this[i] != (ushort)other[i])
                    return false;
            return true;
        }

        public static Mnemonic FromString(string mnemonicString)
        {
            if (string.IsNullOrEmpty(mnemonicString))
                return default;
            var words = mnemonicString.Split(' ');
            if (words.Length != 25)
                throw new ArgumentException($"Mnemonic must be 25 words, but was {words.Length} words...");

            var mnemonic = new Mnemonic();
            for (var i = 0; i < words.Length; i++)
            {
                var wordString = words[i];
                var shortWordString = wordString.Substring(0, math.min(4, wordString.Length));
                mnemonic[i] = (Word)Enum.Parse(typeof(ShortWord), shortWordString);
            }
            return mnemonic;
        }

        public static implicit operator string(Mnemonic mnemonic)
        {
            return mnemonic.ToString();
        }

        public static implicit operator Mnemonic(string mnemonicString)
        {
            return Mnemonic.FromString(mnemonicString);
        }

        public static Mnemonic FromKey(PrivateKey key)
        {
            return key.ToMnemonic();
        }
    }
}
