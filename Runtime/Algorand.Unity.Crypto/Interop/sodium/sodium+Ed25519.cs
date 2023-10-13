using System;
using System.Runtime.InteropServices;

namespace Algorand.Unity.Crypto
{
    internal static unsafe partial class sodium
    {
        internal const int crypto_sign_ed25519_BYTES = 64;
        internal const int crypto_sign_ed25519_PUBLICKEYBYTES = 32;
        internal const int crypto_sign_ed25519_SECRETKEYBYTES = 64;
        internal const int crypto_sign_ed25519_SEEDBYTES = 32;

        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int crypto_sign_ed25519_sk_to_pk(
            void* pk,
            void* sk);

        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int crypto_sign_ed25519_seed_keypair(
            void* pk,
            void* sk,
            void* seed);

        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int crypto_sign_ed25519_sk_to_seed(
            void* seed,
            void* sk);

        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int crypto_sign_ed25519_detached(
            void* signature,
            out ulong signatureLength_p,
            void* message,
            ulong messageLength,
            void* sk);

        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int crypto_sign_ed25519_verify_detached(
            void* signature,
            void* message,
            ulong messageLength,
            void* pk);
    }
}
