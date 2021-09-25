using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;

namespace AlgoSdk.LowLevel
{
    public interface IByteArray
    {
        IntPtr Buffer { get; }
        int Length { get; }
        byte this[int index] { get; set; }
    }

    public struct ByteArrayComparer<T> : IEqualityComparer<T> where T : unmanaged, IByteArray
    {
        public static bool Equals(in T x, in T y)
        {
            for (var i = 0; i < x.Length; i++)
                if (ByteArray.ReadByteAt(x, i) != ByteArray.ReadByteAt(y, i))
                    return false;
            return true;
        }

        public static unsafe int GetHashCode(in T obj)
        {
            return UnsafeUtility.ReadArrayElement<int>((void*)obj.Buffer, 0);
        }

        bool IEqualityComparer<T>.Equals(T x, T y)
        {
            return Equals(in x, in y);
        }

        int IEqualityComparer<T>.GetHashCode(T obj)
        {
            return GetHashCode(in obj);
        }
    }

    public static class ByteArray
    {
        [Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
        public static void CheckElementAccess(int index, int length)
        {
            if (index < 0 || index >= length)
                throw new IndexOutOfRangeException($"Index {index} is out of range [0, {length}).");
        }

        public static byte GetByteAt<TByteArray>(this ref TByteArray bytes, int index)
            where TByteArray : struct, IByteArray
        {
            CheckElementAccess(index, bytes.Length);
            unsafe
            {
                return UnsafeUtility.ReadArrayElement<byte>((byte*)bytes.Buffer, index);
            }
        }

        public static void SetByteAt<TByteArray>(ref this TByteArray bytes, int index, byte value)
            where TByteArray : struct, IByteArray
        {
            CheckElementAccess(index, bytes.Length);
            unsafe
            {
                UnsafeUtility.WriteArrayElement<byte>((byte*)bytes.Buffer, index, value);
            }
        }

        public static byte ReadByteAt<TByteArray>(in TByteArray bytes, int index)
            where TByteArray : struct, IByteArray
        {
            CheckElementAccess(index, bytes.Length);
            unsafe
            {
                return UnsafeUtility.ReadArrayElement<byte>((byte*)bytes.Buffer, index);
            }
        }

        public static byte[] ToRawBytes<TByteArray>(ref this TByteArray bytes)
            where TByteArray : struct, IByteArray
        {
            var result = new byte[bytes.Length];
            for (var i = 0; i < bytes.Length; i++)
                result[i] = bytes.GetByteAt(i);
            return result;
        }

        public static void Copy<T, U>(ref T from, ref U to)
            where T : struct, IByteArray
            where U : struct, IByteArray
        {
            var length = math.min(from.Length, to.Length);
            for (var i = 0; i < length; i++)
                to.SetByteAt(i, from.GetByteAt(i));
        }

        public static byte[] ToArray<TByteArray>(ref this TByteArray bytes)
            where TByteArray : struct, IByteArray
        {
            return bytes.ToRawBytes();
        }

        public static bool Equals<TByteArray>(in TByteArray x, in TByteArray y)
            where TByteArray : unmanaged, IByteArray
        {
            return ByteArrayComparer<TByteArray>.Equals(in x, in y);
        }

        public static bool Equals<T, U>(in T x, in U y)
            where T : struct, IByteArray
            where U : struct, IByteArray
        {
            if (x.Length != y.Length)
                return false;

            for (var i = 0; i < x.Length; i++)
                if (x[i] != y[i])
                    return false;

            return true;
        }

        public static bool Equals<TByteArray>(in TByteArray x, object obj)
            where TByteArray : unmanaged, IByteArray
        {
            if (obj is TByteArray y)
                return Equals(in x, in y);
            return false;
        }

        public static int GetHashCode<TByteArray>(in TByteArray bytes)
            where TByteArray : unmanaged, IByteArray
        {
            return ByteArrayComparer<TByteArray>.GetHashCode(in bytes);
        }
    }
}
