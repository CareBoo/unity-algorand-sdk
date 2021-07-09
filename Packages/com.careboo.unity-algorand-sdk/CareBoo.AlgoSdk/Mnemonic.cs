using System;
using System.Runtime.InteropServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

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

        public static Mnemonic FromKey(Key key)
        {
            throw new NotImplementedException();
        }

        [BurstCompatible(GenericTypeArguments = new[] { typeof(Key) })]
        internal static void To11Bit<TByteArray>(in TByteArray data, ref NativeList<ushort> output)
            where TByteArray : struct, IByteArray
        {
            var buffer = 0;
            var numBits = 0;
            for (var i = 0; i < data.Length; i++)
            {
                buffer |= data[i] << numBits;
                numBits += 8;
                if (numBits >= 11)
                {
                    output.Add((ushort)(buffer & ((ushort)Mnemonic.Word.LENGTH - 1)));
                    buffer >>= 11;
                    numBits -= 11;
                }
            }
            if (numBits != 0)
            {
                output.Add((ushort)(buffer & ((ushort)Mnemonic.Word.LENGTH - 1)));
            }
        }
    }
}
