using System;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;

namespace Algorand.Unity.LowLevel
{
    public interface IArray<T>
    {
        /// <summary>
        ///     The length in <see cref="T" /> of this array.
        /// </summary>
        int Length { get; }

        /// <summary>
        ///     Get/set the element at the index.
        /// </summary>
        T this[int index] { get; set; }
    }

    public interface IContiguousArray<T> : IArray<T>
    {
        /// <summary>
        ///     Gets the ptr at the beginning of this array.
        /// </summary>
        unsafe void* GetUnsafePtr();
    }

    public interface IByteArray : IContiguousArray<byte>
    {
    }

    public struct ByteArrayComparer<T> : IEqualityComparer<T> where T : unmanaged, IByteArray
    {
        bool IEqualityComparer<T>.Equals(T x, T y)
        {
            return Equals(x, y);
        }

        int IEqualityComparer<T>.GetHashCode(T obj)
        {
            return GetHashCode(obj);
        }

        public static bool Equals(T x, T y)
        {
            unsafe
            {
                return UnsafeUtility.MemCmp(x.GetUnsafePtr(), y.GetUnsafePtr(), x.Length) == 0;
            }
        }

        public static unsafe int GetHashCode(T obj)
        {
            return UnsafeUtility.ReadArrayElement<int>(obj.GetUnsafePtr(), 0);
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

        public static ref byte ElementAt<TByteArray>(this ref TByteArray byteArray, int index)
            where TByteArray : struct, IByteArray
        {
            CheckElementAccess(index, byteArray.Length);
            unsafe
            {
                return ref UnsafeUtility.ArrayElementAsRef<byte>(byteArray.GetUnsafePtr(), index);
            }
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
                UnsafeUtility.WriteArrayElement(bytes.GetUnsafePtr(), index, value);
            }
        }

        public static void CopyFrom<T, U>(this ref T target, U source, int start, int length = int.MaxValue)
            where T : struct, IByteArray
            where U : struct, IByteArray
        {
            if (start >= target.Length)
                return;
            length = math.min(source.Length, length);
            unsafe
            {
                UnsafeUtility.MemCpy(
                    (byte*)target.GetUnsafePtr() + start,
                    source.GetUnsafePtr(),
                    length);
            }
        }

        public static void CopyFrom<T>(this ref T target, byte[] source, int start, int length = int.MaxValue)
            where T : struct, IByteArray
        {
            if (start >= target.Length)
                return;
            length = math.min(source.Length, length);
            unsafe
            {
                fixed (byte* s = &source[0])
                {
                    UnsafeUtility.MemCpy(
                        (byte*)target.GetUnsafePtr() + start,
                        s,
                        length);
                }
            }
        }

        public static void CopyFrom<T>(this ref T target, NativeArray<byte> source, int start,
            int length = int.MaxValue)
            where T : struct, IByteArray
        {
            if (start >= target.Length)
                return;
            length = math.min(source.Length, length);
            unsafe
            {
                UnsafeUtility.MemCpy(
                    (byte*)target.GetUnsafePtr() + start,
                    source.GetUnsafePtr(),
                    length);
            }
        }

        public static void CopyTo<T, U>(this T source, ref U target, int start, int length = int.MaxValue)
            where T : struct, IByteArray
            where U : struct, IByteArray
        {
            if (start >= source.Length)
                return;
            length = math.min(length, target.Length);
            unsafe
            {
                UnsafeUtility.MemCpy(target.GetUnsafePtr(), (byte*)source.GetUnsafePtr() + start, length);
            }
        }


        public static void CopyTo<T, U>(this T from, ref U to)
            where T : struct, IByteArray
            where U : struct, IByteArray
        {
            var length = math.min(from.Length, to.Length);
            unsafe
            {
                UnsafeUtility.MemCpy(to.GetUnsafePtr(), from.GetUnsafePtr(), length);
            }
        }

        public static void CopyTo<T>(this T from, ref Span<byte> to)
            where T : struct, IByteArray
        {
            var length = math.min(from.Length, to.Length);

            unsafe
            {
                fixed (byte* toPtr = &to[0])
                {
                    UnsafeUtility.MemCpy(toPtr, from.GetUnsafePtr(), length);
                }
            }
        }

        public static string ToBase64<TByteArray>(this TByteArray bytes)
            where TByteArray : struct, IArray<byte>
        {
            return Convert.ToBase64String(bytes.ToArray());
        }

        public static TByteArray FromBase64<TByteArray>(string b64)
            where TByteArray : struct, IByteArray
        {
            var arr = Convert.FromBase64String(b64);
            var result = default(TByteArray);
            result.CopyFrom(arr, 0, arr.Length);
            return result;
        }

        public static NativeArray<byte> ToNativeArray<TByteArray>(this TByteArray bytes, Allocator allocator)
            where TByteArray : struct, IArray<byte>
        {
            var arr = new NativeArray<byte>(bytes.Length, Allocator.Temp);
            try
            {
                for (var i = 0; i < arr.Length; i++)
                    arr[i] = bytes[i];
                return arr;
            }
            finally
            {
                arr.Dispose();
            }
        }

        public static byte[] ToArray<TByteArray>(this TByteArray bytes)
            where TByteArray : struct, IArray<byte>
        {
            var result = new byte[bytes.Length];
            for (var i = 0; i < bytes.Length; i++)
                result[i] = bytes[i];
            return result;
        }

        public static bool Equals<TByteArray>(TByteArray x, TByteArray y)
            where TByteArray : unmanaged, IByteArray
        {
            return ByteArrayComparer<TByteArray>.Equals(x, y);
        }

        public static bool EqualsOther<T, U>(T x, U y)
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

        public static bool Equals<TByteArray>(TByteArray x, object obj)
            where TByteArray : unmanaged, IByteArray
        {
            if (obj is TByteArray y)
                return Equals(x, y);
            return false;
        }

        public static int GetHashCode<TByteArray>(TByteArray bytes)
            where TByteArray : unmanaged, IByteArray
        {
            return ByteArrayComparer<TByteArray>.GetHashCode(bytes);
        }

        public static ByteArraySlice<TBytes> Slice<TBytes>(this TBytes bytes)
            where TBytes : struct, IByteArray
        {
            return new ByteArraySlice<TBytes>(bytes, 0, bytes.Length);
        }

        public static ByteArraySlice<TBytes> Slice<TBytes>(this TBytes bytes, int start)
            where TBytes : struct, IByteArray
        {
            return new ByteArraySlice<TBytes>(bytes, start, bytes.Length - start);
        }

        public static ByteArraySlice<TBytes> Slice<TBytes>(this TBytes bytes, int start, int length)
            where TBytes : struct, IByteArray
        {
            return new ByteArraySlice<TBytes>(bytes, start, length);
        }

        public static Span<byte> AsSpan<TBytes>(this TBytes bytes)
            where TBytes : struct, IByteArray
        {
            unsafe
            {
                return new Span<byte>(bytes.GetUnsafePtr(), bytes.Length);
            }
        }

        public static ReadOnlySpan<byte> AsReadOnlySpan<TBytes>(this TBytes bytes)
            where TBytes : struct, IByteArray
        {
            unsafe
            {
                return new Span<byte>(bytes.GetUnsafePtr(), bytes.Length);
            }
        }
    }
}
