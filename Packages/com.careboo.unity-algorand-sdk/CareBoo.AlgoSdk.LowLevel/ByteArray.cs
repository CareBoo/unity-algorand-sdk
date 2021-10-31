using System;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;

namespace AlgoSdk.LowLevel
{
    public interface IArray<T>
    {
        int Length { get; }
        T this[int index] { get; set; }
    }

    public interface IContiguousArray<T> : IArray<T>
    {
        unsafe void* GetUnsafePtr();
    }

    public interface IByteArray : IContiguousArray<byte>
    {
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
            return UnsafeUtility.ReadArrayElement<int>(obj.GetUnsafePtr(), 0);
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
                return UnsafeUtility.ReadArrayElement<byte>(bytes.GetUnsafePtr(), index);
            }
        }

        public static void SetByteAt<TByteArray>(ref this TByteArray bytes, int index, byte value)
            where TByteArray : struct, IByteArray
        {
            CheckElementAccess(index, bytes.Length);
            unsafe
            {
                UnsafeUtility.WriteArrayElement<byte>(bytes.GetUnsafePtr(), index, value);
            }
        }

        public static byte ReadByteAt<TByteArray>(in TByteArray bytes, int index)
            where TByteArray : struct, IByteArray
        {
            CheckElementAccess(index, bytes.Length);
            unsafe
            {
                return UnsafeUtility.ReadArrayElement<byte>(bytes.GetUnsafePtr(), index);
            }
        }

        public static void CopyFrom<T, U>(this ref T target, U source, int start, int length = int.MaxValue)
            where T : struct, IByteArray
            where U : struct, IByteArray
        {
            if (start >= target.Length)
                return;
            length = math.min(source.Length, length);
            var end = math.min(target.Length, start + length);
            for (var i = start; i < end; i++)
                target[i] = source[i - start];
        }

        public static void CopyFrom<T>(this ref T target, byte[] source, int start, int length = int.MaxValue)
            where T : struct, IByteArray
        {
            if (start >= target.Length)
                return;
            length = math.min(source.Length, length);
            var end = math.min(target.Length, start + length);
            for (var i = start; i < end; i++)
                target[i] = source[i - start];
        }

        public static void CopyFrom<T>(this ref T target, NativeArray<byte> source, int start, int length = int.MaxValue)
            where T : struct, IByteArray
        {
            if (start >= target.Length)
                return;
            length = math.min(source.Length, length);
            var end = math.min(target.Length, start + length);
            for (var i = start; i < end; i++)
                target[i] = source[i - start];
        }

        public static void CopyTo<T, U>(this T source, ref U target, int start, int length = int.MaxValue)
            where T : struct, IByteArray
            where U : struct, IByteArray
        {
            if (start >= source.Length)
                return;
            length = math.min(length, target.Length);
            var end = math.min(source.Length, start + length);
            for (var i = start; i < end; i++)
                target[i - start] = source[i];
        }


        public static void CopyTo<T, U>(this T from, ref U to)
            where T : struct, IByteArray
            where U : struct, IByteArray
        {
            var length = math.min(from.Length, to.Length);
            for (var i = 0; i < length; i++)
                to.SetByteAt(i, from.GetByteAt(i));
        }

        public static string ToBase64<TByteArray>(this TByteArray bytes)
            where TByteArray : struct, IArray<byte>
        {
            return System.Convert.ToBase64String(bytes.ToArray());
        }

        public static TByteArray FromBase64<TByteArray>(string b64)
            where TByteArray : struct, IByteArray
        {
            var arr = System.Convert.FromBase64String(b64);
            var result = default(TByteArray);
            result.CopyFrom(arr, 0, arr.Length);
            return result;
        }

        public static byte[] ToArray<TByteArray>(this TByteArray bytes)
            where TByteArray : struct, IArray<byte>
        {
            var result = new byte[bytes.Length];
            for (var i = 0; i < bytes.Length; i++)
                result[i] = bytes[i];
            return result;
        }

        public static bool Equals<TByteArray>(in TByteArray x, in TByteArray y)
            where TByteArray : unmanaged, IByteArray
        {
            return ByteArrayComparer<TByteArray>.Equals(in x, in y);
        }

        public static bool EqualsOther<T, U>(in T x, in U y)
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
