using System;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;
using AlgoSdk.LowLevel;

namespace AlgoSdk
{
    [Serializable]
    [StructLayout(LayoutKind.Explicit, Size = 50)]
    internal struct FixedBytes50
    {
        [FieldOffset(0)] internal FixedBytes16 offset0000;
        [FieldOffset(16)] internal FixedBytes16 offset0016;
        [FieldOffset(32)] internal FixedBytes16 offset0032;
        [FieldOffset(48)] internal byte byte0048;
        [FieldOffset(49)] internal byte byte0049;
    }

    [Serializable]
    [StructLayout(LayoutKind.Explicit, Size = 50)]
    [BurstCompatible]
    public partial struct Mnemonic
        : IEquatable<Mnemonic>
    {
        [FieldOffset(0)] internal FixedBytes50 buffer;
        public const int Length = 25;

        unsafe internal byte* Buffer
        {
            get
            {
                fixed (byte* b = &buffer.offset0000.byte0000)
                    return b;
            }
        }

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

        [NotBurstCompatible]
        public override string ToString()
        {
            var words = new string[25];
            for (var i = 0; i < Length; i++)
                words[i] = this[i].ToString();
            return string.Join(" ", words);
        }

        public Key ToKey()
        {
            throw new NotImplementedException();
        }

        public bool Equals(Mnemonic other)
        {
            for (var i = 0; i < Length; i++)
                if ((ushort)this[i] != (ushort)other[i])
                    return false;
            return true;
        }

        [NotBurstCompatible]
        public static Mnemonic FromString(string mnemonicString)
        {
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

        [NotBurstCompatible]
        public static implicit operator string(Mnemonic mnemonic)
        {
            return mnemonic.ToString();
        }

        [NotBurstCompatible]
        public static implicit operator Mnemonic(string mnemonicString)
        {
            return Mnemonic.FromString(mnemonicString);
        }

        public static Mnemonic FromKey(Key key)
        {
            throw new NotImplementedException();
        }
    }
}
