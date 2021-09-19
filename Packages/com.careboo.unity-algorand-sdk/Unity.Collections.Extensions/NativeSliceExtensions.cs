using System;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity.Collections
{
    public static class NativeSliceExtensions
    {
        public unsafe static NativeSlice<T> AsNativeSlice<T>(
            this NativeArray<T>.ReadOnly arr,
            int start,
            int length)
            where T : struct
        {
            if (start < 0)
            {
                throw new ArgumentOutOfRangeException("start", $"Slice start {start} < 0.");
            }
            if (length < 0)
            {
                throw new ArgumentOutOfRangeException("length", $"Slice length {length} < 0.");
            }
            if (start + length > arr.Length)
            {
                throw new ArgumentException($"Slice start + length ({start + length}) range must be <= array.Length ({arr.Length})");
            }
            var slice = new NativeSlice<T>();
            slice.m_MinIndex = 0;
            slice.m_MaxIndex = length - 1;
            slice.m_Safety = arr.m_Safety;
            slice.m_Stride = UnsafeUtility.SizeOf<T>();
            slice.m_Buffer = (byte*)arr.m_Buffer + slice.m_Stride * start;
            slice.m_Length = length;
            return slice;
        }
    }
}
