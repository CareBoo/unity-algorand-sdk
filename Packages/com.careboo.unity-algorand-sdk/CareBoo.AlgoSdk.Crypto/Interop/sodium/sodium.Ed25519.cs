using System;
using System.Runtime.InteropServices;

namespace AlgoSdk.Crypto
{
    internal static unsafe partial class sodium
    {
        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int crypto_sign_ed25519_seed_keypair(
            Ed25519.PublicKey* pk,
            SecureMemoryHandle sk,
            Ed25519.Seed* seed);

        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        internal static unsafe extern int crypto_sign_ed25519_detached(
            byte* signature,
            out ulong signatureLength_p,
            byte* message,
            ulong messageLength,
            SecureMemoryHandle sk);

        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int crypto_sign_ed25519_verify_detached(
            byte* signature,
            byte* message,
            ulong messageLength,
            Ed25519.PublicKey* pk);
    }
}
