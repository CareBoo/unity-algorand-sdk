using System;
using System.Runtime.InteropServices;

namespace Algorand.Unity.Crypto
{
    internal static unsafe partial class sodium
    {
        public const int crypto_pwhash_STRBYTES = 128;

        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern int crypto_pwhash_str(
            byte* @out,
            byte* passwd,
            ulong passwdlen,
            ulong opslimit,
            UIntPtr memlimit
        );

        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern int crypto_pwhash_str_verify(
            byte* str,
            byte* passwd,
            ulong passwdlen
        );

        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern int crypto_pwhash_str_needs_rehash(
            byte* str,
            ulong opslimit,
            UIntPtr memlimit
        );
    }
}
