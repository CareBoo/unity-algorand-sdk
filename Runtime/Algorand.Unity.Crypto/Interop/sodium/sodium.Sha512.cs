using System.Runtime.InteropServices;
using Algorand.Unity.LowLevel;

namespace Algorand.Unity.Crypto
{
    internal static unsafe partial class sodium
    {
#if (UNITY_WEBGL && !UNITY_EDITOR)
        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void crypto_hash_sha512_256(
            void* output,
            void* @in,
            int inlen);
#else
        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int crypto_hash_sha512_init(
            void* state);

        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int crypto_hash_sha512_update(
            void* state,
            void* @in,
            ulong inlen);

        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int crypto_hash_sha512_final(
            void* state,
            void* @out);

        [StructLayout(LayoutKind.Explicit, Size = SizeBytes)]
        internal struct crypto_hash_sha512_state
        {
            internal const int SizeBytes = (8 * 8) + (2 * 8) + 128;

            [FieldOffset(0)] internal Sha512StateVector vector;
            [FieldOffset(64)] internal Sha512StateCount count;
            [FieldOffset(80)] internal FixedBytes128 buffer;
        }
#endif
    }
}
