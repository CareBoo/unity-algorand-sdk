using System.Runtime.InteropServices;
using Algorand.Unity.LowLevel;

namespace Algorand.Unity.Crypto
{
    internal static unsafe partial class sodium
    {
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
            internal const int SizeBytes = 8 * 8 + 2 * 8 + 128;

            [FieldOffset(80)]
            internal FixedBytes128 buffer;

            [FieldOffset(64)]
            internal Sha512.StateCount count;

            [FieldOffset(0)]
            internal Sha512.StateVector vector;
        }
    }
}
