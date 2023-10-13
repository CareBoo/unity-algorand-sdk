using System.Runtime.InteropServices;

namespace Algorand.Unity.Crypto
{
    internal static unsafe partial class sodium
    {
        public const int crypto_secretbox_KEYBYTES = 32;
        public const int crypto_secretbox_NONCEBYTES = 24;
        public const int crypto_secretbox_MACBYTES = 16;

        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern int crypto_secretbox_easy(
            void* c,
            void* m,
            ulong mlen,
            void* n,
            void* k);

        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern int crypto_secretbox_open_easy(
            void* m,
            void* c,
            ulong clen,
            void* n,
            void* k);
    }
}
