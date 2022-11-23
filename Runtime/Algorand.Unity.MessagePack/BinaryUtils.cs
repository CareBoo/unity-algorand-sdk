using System.Runtime.CompilerServices;

namespace Algorand.Unity.MessagePack
{
    public static class BinaryUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static sbyte ReverseEndianness(sbyte value)
        {
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static short ReverseEndianness(short value)
        {
            return (short)(((value & 0xFF) << 8) | ((value & 0xFF00) >> 8));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ReverseEndianness(int value)
        {
            return (int)ReverseEndianness((uint)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static long ReverseEndianness(long value)
        {
            return (long)ReverseEndianness((ulong)value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte ReverseEndianness(byte value)
        {
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ushort ReverseEndianness(ushort value)
        {
            return (ushort)((value >> 8) + (value << 8));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint ReverseEndianness(uint value)
        {
            uint num = value & 0xFF00FFu;
            uint num2 = value & 0xFF00FF00u;
            return ((num >> 8) | (num << 24)) + ((num2 << 8) | (num2 >> 24));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ulong ReverseEndianness(ulong value)
        {
            return ((ulong)ReverseEndianness((uint)value) << 32) + ReverseEndianness((uint)(value >> 32));
        }
    }
}
