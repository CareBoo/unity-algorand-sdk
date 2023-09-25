using System;
using System.Runtime.InteropServices;

namespace Algorand.Unity.Crypto
{
    internal static unsafe partial class sodium
    {
        public const uint crypto_pwhash_PASSWD_MIN = 0;
        public const uint crypto_pwhash_PASSWD_MAX = 4294967295;
        public const int crypto_pwhash_SALTBYTES = 16;
        public const int crypto_pwhash_STRBYTES = 128;
        public const int crypto_pwhash_ALG_DEFAULT = 2;
        public const int crypto_pwhash_ALG_ARGON2I13 = 1;
        public const int crypto_pwhash_ALG_ARGON2ID13 = 2;

        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern int crypto_pwhash(
            byte* @out,
            ulong outlen,
            byte* passwd,
            ulong passwdlen,
            byte* salt,
            ulong opslimit,
            UIntPtr memlimit,
            int alg
        );

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
