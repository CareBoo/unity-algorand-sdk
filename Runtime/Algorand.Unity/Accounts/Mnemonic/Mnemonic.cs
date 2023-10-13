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
    internal struct FixedBytes50 : IByteArray
    {
        [FieldOffset(0)]
        [SerializeField]
        internal FixedBytes16 offset0000;

        [FieldOffset(16)]
        [SerializeField]
        internal FixedBytes16 offset0016;

        [FieldOffset(32)]
        [SerializeField]
        internal FixedBytes16 offset0032;

        [FieldOffset(48)]
        [SerializeField]
        internal byte byte0048;

        [FieldOffset(49)]
        [SerializeField]
        internal byte byte0049;

        public int Length => 50;

        public byte this[int index]
        {
            get => this.GetByteAt(index);
            set => this.SetByteAt(index, value);
        }

        public unsafe void* GetUnsafePtr()
        {
            fixed (byte* b = &offset0000.byte0000)
            {
                return b;
            }
        }
    }

    /// <summary>
    ///     Byte struct representing a private key encoded with <see cref="Length" /> words.
    /// </summary>
    [Serializable]
    [StructLayout(LayoutKind.Explicit, Size = 50)]
    public partial struct Mnemonic
        : IEquatable<Mnemonic>
    {
        public enum ParseError
        {
            None,
            Not25Words,
            WordsNotInSet,
            InvalidChecksum
        }

        /// <summary>
        ///     Size of Mnemonic in bytes.
        /// </summary>
        public const int SizeBytes = 50;

        /// <summary>
        ///     Length of the largest word.
        /// </summary>
        public const int MaxWordLength = 8;

        /// <summary>
        ///     Number of words contained in the mnemonic.
        /// </summary>
        public const int Length = 25;

        /// <summary>
        ///     Index of the word used for the checksum.
        /// </summary>
        public const int ChecksumIndex = Length - 1;

        /// <summary>
        ///     The number of bits encoded by each word.
        /// </summary>
        public const int BitsPerWord = 11;

        [FieldOffset(0)]
        [SerializeField]
        internal FixedBytes50 buffer;

        internal unsafe byte* Buffer
        {
            get
            {
                fixed (byte* b = &buffer.offset0000.byte0000)
                {
                    return b;
                }
            }
        }

        /// <summary>
        ///     The word at a given index.
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
                    UnsafeUtility.WriteArrayElement(Buffer, index, value);
                }
            }
        }

        public bool IsValid()
        {
            for (var i = 0; i < ChecksumIndex; i++)
            {
                if (this[i] == Word.Unknown) return false;
            }
            return this[ChecksumIndex] == ComputeChecksum();
        }

        public bool Equals(Mnemonic other)
        {
            for (var i = 0; i < Length; i++)
                if ((ushort)this[i] != (ushort)other[i])
                    return false;
            return true;
        }

        /// <summary>
        ///     Get the byte pointer at the start of this struct.
        /// </summary>
        /// <returns>An unsafe byte pointer.</returns>
        public unsafe byte* GetUnsafePtr()
        {
            return Buffer;
        }

        public override string ToString()
        {
            var words = new string[25];
            for (var i = 0; i < Length; i++)
                words[i] = this[i].ToString();
            return string.Join(" ", words);
        }

        /// <summary>
        ///     Get the <see cref="PrivateKey" /> from this mnemonic encodes.
        /// </summary>
        public void ToPrivateKey(ref PrivateKey privateKey)
        {
            var buffer = 0;
            var numBits = 0;
            var j = 0;
            for (var i = 0; i < ChecksumIndex; i++)
            {
                buffer |= (ushort)this[i] << numBits;
                numBits += BitsPerWord;
                while (numBits >= 8 && j < 32)
                {
                    privateKey[j] = (byte)(buffer & 0xff);
                    j++;
                    buffer >>= 8;
                    numBits -= 8;
                }
            }

            if (numBits != 0 && j < 32) privateKey[j] = (byte)(buffer & 0xff);
        }

        /// <summary>
        ///     Get the <see cref="PrivateKey" /> from this mnemonic encodes.
        /// </summary>
        public void ToPrivateKey(Span<byte> privateKeyBytes)
        {
            var buffer = 0;
            var numBits = 0;
            var j = 0;
            for (var i = 0; i < ChecksumIndex; i++)
            {
                buffer |= (ushort)this[i] << numBits;
                numBits += BitsPerWord;
                while (numBits >= 8 && j < 32)
                {
                    privateKeyBytes[j] = (byte)(buffer & 0xff);
                    j++;
                    buffer >>= 8;
                    numBits -= 8;
                }
            }

            if (numBits != 0 && j < 32) privateKeyBytes[j] = (byte)(buffer & 0xff);
        }

        /// <summary>
        ///     Get the <see cref="PrivateKey" /> from this mnemonic encodes.
        /// </summary>
        public PrivateKey ToPrivateKey()
        {
            var result = new PrivateKey();
            ToPrivateKey(ref result);
            return result;
        }

        private Word ComputeChecksum()
        {
            var pk = ToPrivateKey();
            var newMnemonic = pk.ToMnemonic();
            return newMnemonic[ChecksumIndex];
        }

        /// <summary>
        ///     Convert a mnemonic from a string.
        /// </summary>
        /// <param name="mnemonicString">The 25 word mnemonic in a string, with each word separated by spaces.</param>
        /// <returns>A mnemonic parsed from the string.</returns>
        /// <exception cref="ArgumentException">If the mnemonic string cannot be parsed, this error is thrown.</exception>
        public static Mnemonic FromString(string mnemonicString)
        {
            if (string.IsNullOrEmpty(mnemonicString))
                return default;

            return TryParse(mnemonicString, out var mnemonic) switch
            {
                ParseError.InvalidChecksum => throw new ArgumentException("invalid checksum", nameof(mnemonicString)),
                ParseError.Not25Words => throw new ArgumentException("mnemonic must be 25 words",
                    nameof(mnemonicString)),
                ParseError.WordsNotInSet => throw new ArgumentException("mnemonic contains words not in set",
                    nameof(mnemonicString)),
                ParseError.None => mnemonic,
                _ => throw new ArgumentException("unknown parse error", nameof(mnemonicString))
            };
        }

        /// <summary>
        ///     Try to parse a mnemonic from a string.
        /// </summary>
        /// <param name="mnemonicString">The mnemonic string of 25 words separated by space.</param>
        /// <param name="mnemonic">The mnemonic parsed from the mnemonic string.</param>
        /// <returns>A ParseError if the mnemonic string could not be parsed.</returns>
        public static ParseError TryParse(ReadOnlySpan<char> mnemonicString, out Mnemonic mnemonic)
        {
            mnemonic = default;
            if (mnemonicString.Length == 0) return ParseError.None;

            var start = 0;
            int length;
            var wordCount = 0;
            ReadOnlySpan<char> shortWord;
            Word parsedWord;
            for (var i = 1; i < mnemonicString.Length; i++)
            {
                var c = mnemonicString[i];
                if (c != ' ') continue;
                length = i - start;
                shortWord = mnemonicString.Slice(start, math.min(length, 4));
                parsedWord = ParseWord(shortWord);
                if (parsedWord == Word.Unknown) return ParseError.WordsNotInSet;
                mnemonic[wordCount] = parsedWord;

                wordCount++;
                i++;
                start = i;
                if (wordCount == 24) break;
            }

            if (wordCount < 24) return ParseError.Not25Words;

            length = mnemonicString.Length - start;
            shortWord = mnemonicString.Slice(start, math.min(length, 4));
            parsedWord = ParseWord(shortWord);
            if (parsedWord == Word.Unknown) return ParseError.WordsNotInSet;
            var checksum = mnemonic.ComputeChecksum();
            if (checksum != parsedWord) return ParseError.InvalidChecksum;
            mnemonic[ChecksumIndex] = parsedWord;

            return ParseError.None;
        }

        public static implicit operator string(Mnemonic mnemonic)
        {
            return mnemonic.ToString();
        }

        public static implicit operator Mnemonic(string mnemonicString)
        {
            return FromString(mnemonicString);
        }

        public static Mnemonic FromKey(PrivateKey key)
        {
            return key.ToMnemonic();
        }
    }
}
