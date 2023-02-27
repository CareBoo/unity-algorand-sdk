using System;
using System.Collections.Generic;
using System.Numerics;
using Unity.Collections;

namespace Algorand.Unity
{
    public static class Endianness
    {
        public static NativeArray<byte> ToBytesBigEndian(this long value, Allocator allocator)
            => ToBytesBigEndian(unchecked((ulong)value), allocator);

        public static NativeArray<byte> ToBytesBigEndian(this ulong value, Allocator allocator)
        {
            var result = new NativeArray<byte>(8, allocator);
            value.CopyToNativeBytesBigEndian(ref result);
            return result;
        }

        public static NativeArray<byte> ToBytesBigEndian(this int value, Allocator allocator)
            => ToBytesBigEndian(unchecked((uint)value), allocator);

        public static NativeArray<byte> ToBytesBigEndian(this uint value, Allocator allocator)
        {
            var result = new NativeArray<byte>(8, allocator);
            value.CopyToNativeBytesBigEndian(ref result);
            return result;
        }

        public static NativeArray<byte> ToBytesBigEndian(this short value, Allocator allocator)
            => ToBytesBigEndian(unchecked((ushort)value), allocator);

        public static NativeArray<byte> ToBytesBigEndian(this ushort value, Allocator allocator)
        {
            var result = new NativeArray<byte>(8, allocator);
            value.CopyToNativeBytesBigEndian(ref result);
            return result;
        }

        public static byte[] ToBytesBigEndian(this long value)
            => ToBytesBigEndian(unchecked((ulong)value));

        public static byte[] ToBytesBigEndian(this ulong value)
        {
            var bytes = new byte[8];
            value.CopyToBytesBigEndian(ref bytes);
            return bytes;
        }

        public static byte[] ToBytesBigEndian(this int value)
            => ToBytesBigEndian(unchecked((uint)value));

        public static byte[] ToBytesBigEndian(this uint value)
        {
            var bytes = new byte[8];
            value.CopyToBytesBigEndian(ref bytes);
            return bytes;
        }

        public static byte[] ToBytesBigEndian(this short value)
            => ToBytesBigEndian(unchecked((ushort)value));

        public static byte[] ToBytesBigEndian(this ushort value)
        {
            var bytes = new byte[8];
            value.CopyToBytesBigEndian(ref bytes);
            return bytes;
        }

        public static void CopyToBytesBigEndian<TByteArray>(this long value, ref TByteArray bytes, int offset = 0)
            where TByteArray : IList<byte> => CopyToBytesBigEndian(unchecked((ulong)value), ref bytes, offset);

        public static void CopyToBytesBigEndian<TByteArray>(this ulong value, ref TByteArray bytes, int offset = 0)
            where TByteArray : IList<byte>
        {
            unchecked
            {
                bytes[offset] = (byte)(value >> 56);
                bytes[offset + 1] = (byte)(value >> 48);
                bytes[offset + 2] = (byte)(value >> 40);
                bytes[offset + 3] = (byte)(value >> 32);
                bytes[offset + 4] = (byte)(value >> 24);
                bytes[offset + 5] = (byte)(value >> 16);
                bytes[offset + 6] = (byte)(value >> 8);
                bytes[offset + 7] = (byte)(value);
            }
        }

        public static void CopyToBytesBigEndian<TByteArray>(this int value, ref TByteArray bytes, int offset = 0)
            where TByteArray : IList<byte> => CopyToBytesBigEndian(unchecked((uint)value), ref bytes, offset);

        public static void CopyToBytesBigEndian<TByteArray>(this uint value, ref TByteArray bytes, int offset = 0)
            where TByteArray : IList<byte>
        {
            unchecked
            {
                bytes[offset] = (byte)(value >> 24);
                bytes[offset + 1] = (byte)(value >> 16);
                bytes[offset + 2] = (byte)(value >> 8);
                bytes[offset + 3] = (byte)(value);
            }
        }

        public static void CopyToBytesBigEndian<TByteArray>(this short value, ref TByteArray bytes, int offset = 0)
            where TByteArray : IList<byte> => CopyToBytesBigEndian(unchecked((ushort)value), ref bytes, offset);

        public static void CopyToBytesBigEndian<TByteArray>(this ushort value, ref TByteArray bytes, int offset = 0)
            where TByteArray : IList<byte>
        {
            unchecked
            {
                bytes[offset] = (byte)(value >> 8);
                bytes[offset + 1] = (byte)(value);
            }
        }

        public static void CopyToNativeBytesBigEndian<TByteArray>(this long value, ref TByteArray bytes, int offset = 0)
            where TByteArray : IIndexable<byte> => CopyToNativeBytesBigEndian(unchecked((ulong)value), ref bytes, offset);

        public static void CopyToNativeBytesBigEndian<TByteArray>(this ulong value, ref TByteArray bytes, int offset = 0)
            where TByteArray : IIndexable<byte>
        {
            unchecked
            {
                bytes.ElementAt(offset) = (byte)(value >> 56);
                bytes.ElementAt(offset + 1) = (byte)(value >> 48);
                bytes.ElementAt(offset + 2) = (byte)(value >> 40);
                bytes.ElementAt(offset + 3) = (byte)(value >> 32);
                bytes.ElementAt(offset + 4) = (byte)(value >> 24);
                bytes.ElementAt(offset + 5) = (byte)(value >> 16);
                bytes.ElementAt(offset + 6) = (byte)(value >> 8);
                bytes.ElementAt(offset + 7) = (byte)(value);
            }
        }

        public static void CopyToNativeBytesBigEndian<TByteArray>(this int value, ref TByteArray bytes, int offset = 0)
            where TByteArray : IIndexable<byte> => CopyToNativeBytesBigEndian(unchecked((uint)value), ref bytes, offset);

        public static void CopyToNativeBytesBigEndian<TByteArray>(this uint value, ref TByteArray bytes, int offset = 0)
            where TByteArray : IIndexable<byte>
        {
            unchecked
            {
                bytes.ElementAt(offset) = (byte)(value >> 24);
                bytes.ElementAt(offset + 1) = (byte)(value >> 16);
                bytes.ElementAt(offset + 2) = (byte)(value >> 8);
                bytes.ElementAt(offset + 3) = (byte)(value);
            }
        }

        public static void CopyToNativeBytesBigEndian<TByteArray>(this short value, ref TByteArray bytes, int offset = 0)
            where TByteArray : IIndexable<byte> => CopyToNativeBytesBigEndian(unchecked((ushort)value), ref bytes, offset);

        public static void CopyToNativeBytesBigEndian<TByteArray>(this ushort value, ref TByteArray bytes, int offset = 0)
            where TByteArray : IIndexable<byte>
        {
            unchecked
            {
                bytes.ElementAt(offset) = (byte)(value >> 8);
                bytes.ElementAt(offset + 1) = (byte)(value);
            }
        }

        public static void CopyToNativeBytesBigEndian(this long value, ref NativeArray<byte> bytes, int offset = 0)
            => CopyToNativeBytesBigEndian(unchecked((ulong)value), ref bytes, offset);

        public static void CopyToNativeBytesBigEndian(this ulong value, ref NativeArray<byte> bytes, int offset = 0)
        {
            unchecked
            {
                bytes[offset] = (byte)(value >> 56);
                bytes[offset + 1] = (byte)(value >> 48);
                bytes[offset + 2] = (byte)(value >> 40);
                bytes[offset + 3] = (byte)(value >> 32);
                bytes[offset + 4] = (byte)(value >> 24);
                bytes[offset + 5] = (byte)(value >> 16);
                bytes[offset + 6] = (byte)(value >> 8);
                bytes[offset + 7] = (byte)(value);
            }
        }

        public static void CopyToNativeBytesBigEndian(this int value, ref NativeArray<byte> bytes, int offset = 0)
            => CopyToNativeBytesBigEndian(unchecked((uint)value), ref bytes, offset);

        public static void CopyToNativeBytesBigEndian(this uint value, ref NativeArray<byte> bytes, int offset = 0)
        {
            unchecked
            {
                bytes[offset] = (byte)(value >> 24);
                bytes[offset + 1] = (byte)(value >> 16);
                bytes[offset + 2] = (byte)(value >> 8);
                bytes[offset + 3] = (byte)(value);
            }
        }

        public static void CopyToNativeBytesBigEndian(this short value, ref NativeArray<byte> bytes, int offset = 0)
            => CopyToNativeBytesBigEndian(unchecked((ushort)value), ref bytes, offset);

        public static void CopyToNativeBytesBigEndian(this ushort value, ref NativeArray<byte> bytes, int offset = 0)
        {
            unchecked
            {
                bytes[offset] = (byte)(value >> 8);
                bytes[offset + 1] = (byte)(value);
            }
        }

        public static void FromBytesBigEndian(byte[] bytes, out ushort value)
        {
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);

            value = BitConverter.ToUInt16(bytes, 0);
        }

        public static void FromBytesBigEndian(byte[] bytes, out uint value)
        {
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);

            value = BitConverter.ToUInt32(bytes, 0);
        }

        public static void FromBytesBigEndian(byte[] bytes, out ulong value)
        {
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);

            value = BitConverter.ToUInt64(bytes, 0);
        }

        public static void FromBytesBigEndian(byte[] bytes, out BigInteger value)
        {
            if (BitConverter.IsLittleEndian)
                Array.Reverse(bytes);
            value = new BigInteger(bytes);
        }
    }
}
