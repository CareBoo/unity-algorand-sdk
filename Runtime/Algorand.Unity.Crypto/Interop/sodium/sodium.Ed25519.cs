using System;
using System.Runtime.InteropServices;

namespace Algorand.Unity.Crypto
{
    internal static unsafe partial class sodium
    {
        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int crypto_sign_ed25519_seed_keypair(
            void* pk,
            IntPtr sk,
            void* seed);

#if (UNITY_WEBGL && !UNITY_EDITOR)
        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void crypto_sign_ed25519_detached(
            void* signature,
            void* message,
            int messageLength,
            IntPtr sk);

        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int crypto_sign_ed25519_verify_detached(
            void* signature,
            void* message,
            int messageLength,
            void* pk);
#else
        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int crypto_sign_ed25519_detached(
            void* signature,
            out ulong signatureLength_p,
            void* message,
            ulong messageLength,
            IntPtr sk);

        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int crypto_sign_ed25519_verify_detached(
            void* signature,
            void* message,
            ulong messageLength,
            void* pk);
#endif
    }
}
