using System;
using Algorand.Unity.LowLevel;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using static Algorand.Unity.Crypto.sodium;

namespace Algorand.Unity.Crypto
{
    public static partial class Sha512
    {
        internal static readonly StateVector Fips256Iv = new ulong[8]
        {
            Convert.ToUInt64("22312194FC2BF72C", 16),
            Convert.ToUInt64("9F555FA3C84C64C2", 16),
            Convert.ToUInt64("2393B86B6F53B151", 16),
            Convert.ToUInt64("963877195940EABD", 16),
            Convert.ToUInt64("96283EE2A88EFFE3", 16),
            Convert.ToUInt64("BE5E1E2553863992", 16),
            Convert.ToUInt64("2B0199FC2C85B8AA", 16),
            Convert.ToUInt64("0EB72DDC81C52CA2", 16)
        };

        public static Sha512_256_Hash Hash256Truncated<TByteArray>(TByteArray bytes)
            where TByteArray : struct, IByteArray
        {
            unsafe
            {
                return Hash256Truncated((byte*)bytes.GetUnsafePtr(), bytes.Length);
            }
        }

        public static Sha512_256_Hash Hash256Truncated(NativeArray<byte>.ReadOnly bytes)
        {
            unsafe
            {
                return Hash256Truncated((byte*)bytes.GetUnsafeReadOnlyPtr(), bytes.Length);
            }
        }

        public static Sha512_256_Hash Hash256Truncated(ReadOnlySpan<byte> bytes)
        {
            unsafe
            {
                fixed (byte* ptr = bytes)
                {
                    return Hash256Truncated(ptr, bytes.Length);
                }
            }
        }

        public static unsafe Sha512_256_Hash Hash256Truncated(byte* ptr, int length)
        {
            var result = new Sha512_256_Hash();
            var hashState = default(crypto_hash_sha512_state);
            crypto_hash_sha512_init(&hashState);
            hashState.vector = Fips256Iv;
            crypto_hash_sha512_update(&hashState, ptr, (ulong)length);
            var hash512 = new Sha512_Hash();
            crypto_hash_sha512_final(&hashState, &hash512);
            hash512.CopyTo(ref result);
            return result;
        }
    }
}
