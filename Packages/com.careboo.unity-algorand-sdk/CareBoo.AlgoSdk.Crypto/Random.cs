using System;
using static AlgoSdk.Crypto.sodium;
using Unity.Collections.LowLevel.Unsafe;

namespace AlgoSdk.Crypto
{
    public unsafe static class Random
    {
        static Random()
        {
            sodium_init();
        }

        public static T RandomBytes<T>() where T : unmanaged
        {
            var size = UnsafeUtility.SizeOf<T>();
            T result = default;
            randombytes_buf(&result, (UIntPtr)size);
            return result;
        }
    }
}
