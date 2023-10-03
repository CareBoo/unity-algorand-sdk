#if !UNITY_2022_3_OR_NEWER
using System;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace Algorand.Unity
{
    public static class NativeArrayExtensions
    {
        public unsafe static Span<T> AsSpan<T>(this NativeArray<T> nativeArray)
            where T : unmanaged
        {
            return new Span<T>(nativeArray.GetUnsafePtr(), nativeArray.Length);
        }

        public unsafe static ReadOnlySpan<T> AsReadOnlySpan<T>(this NativeArray<T> nativeArray)
            where T : unmanaged
        {
            return new ReadOnlySpan<T>(nativeArray.GetUnsafeReadOnlyPtr(), nativeArray.Length);
        }
    }
}
#endif
