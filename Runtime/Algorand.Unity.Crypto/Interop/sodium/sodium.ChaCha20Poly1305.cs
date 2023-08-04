using System.Runtime.InteropServices;

namespace Algorand.Unity.Crypto
{
    internal static unsafe partial class sodium
    {
        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int crypto_aead_chacha20poly1305_ietf_encrypt(
            void* c,
            out ulong clen_p,
            void* m,
            ulong mlen,
            void* ad,
            ulong adlen,
            void* nsec,
            void* npub,
            void* k);

        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int crypto_aead_chacha20poly1305_ietf_decrypt(
            void* m,
            out ulong mlen_p,
            void* nsec,
            void* c,
            ulong clen,
            void* ad,
            ulong adlen,
            void* npub,
            void* k);
    }
}