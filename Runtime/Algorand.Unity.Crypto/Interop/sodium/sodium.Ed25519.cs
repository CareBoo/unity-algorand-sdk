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
        internal static unsafe extern void crypto_sign_ed25519_detached(
            void* signature,
            void* message,
            UIntPtr messageLength,
            IntPtr sk);
#else
        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        internal static unsafe extern int crypto_sign_ed25519_detached(
            void* signature,
            out UIntPtr signatureLength_p,
            void* message,
            UIntPtr messageLength,
            IntPtr sk);
#endif

        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int crypto_sign_ed25519_verify_detached(
            void* signature,
            void* message,
            UIntPtr messageLength,
            void* pk);
    }
}
