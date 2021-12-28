using System;
using static AlgoSdk.Crypto.sodium;
using Unity.Collections.LowLevel.Unsafe;
using AlgoSdk.LowLevel;

namespace AlgoSdk.Crypto
{
    public unsafe static class Random
    {
#if (!UNITY_WEBGL || UNITY_EDITOR)
        static Random()
        {
            sodium_init();
        }
#endif

        public static T Bytes<T>() where T : unmanaged
        {
            var size = UnsafeUtility.SizeOf<T>();
            T result = default;
            randombytes_buf(&result, (UIntPtr)size);
            return result;
        }

        public static void Randomize<T>(T bytes)
            where T : struct, IByteArray
        {
            randombytes_buf(bytes.GetUnsafePtr(), (UIntPtr)bytes.Length);
        }
    }
}
