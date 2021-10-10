using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

public static class NativeTextExtensions
{
    public unsafe static NativeArray<byte> AsArray(this NativeText text)
    {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
        AtomicSafetyHandle.CheckGetSecondaryDataPointerAndThrow(text.m_Safety);
        var arraySafety = text.m_Safety;
        AtomicSafetyHandle.UseSecondaryVersion(ref arraySafety);
#endif
        var array = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<byte>(text.GetUnsafePtr(), text.Length, Allocator.None);

#if ENABLE_UNITY_COLLECTIONS_CHECKS
        NativeArrayUnsafeUtility.SetAtomicSafetyHandle(ref array, arraySafety);
#endif
        return array;
    }
}
