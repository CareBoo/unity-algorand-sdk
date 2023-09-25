using System.Runtime.InteropServices;

namespace Algorand.Unity.Crypto
{
    internal static unsafe partial class sodium
    {
        internal const ulong crypto_box_PUBLICKEYBYTES = 32U;
        internal const ulong crypto_box_SECRETKEYBYTES = 32U;
        internal const ulong crypto_box_NONCEBYTES = 24U;

        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int crypto_box_keypair(
            void* pk,
            void* sk);

        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int crypto_box_seed_keypair(
            void* pk,
            void* sk,
            void* seed);

        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int crypto_box_easy(
            void* c,
            void* m,
            ulong mlen,
            void* n,
            void* pk,
            void* sk);
    }
}
