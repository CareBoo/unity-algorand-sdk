using System;
using System.Runtime.InteropServices;

namespace Algorand.Unity.Crypto
{
    internal static unsafe partial class sodium
    {
        public const int crypto_pwhash_STRBYTES = 128;

        public enum OpsLimit : ulong
        {
            Interactive = 2,
            Max = 4294967295,
            Min = 2,
            Moderate = 3,
            Sensitive = 4
        }

        public enum MemLimit : ulong
        {
            Interactive = 67108864,
            Max = 4398046510080,
            Min = 8192,
            Moderate = 134217728,
            Sensitive = 1073741824
        }

        public enum PasswordStorageError
        {
            Success = 0,
            OutOfMemory = -1
        }

        public enum PasswordVerificationError
        {
            Success = 0,
            VerificationFailed = -1,
        }

        public enum PasswordNeedsRehashResult
        {
            Success = 0,
            Error = -1,
            NeedsRehash = 1
        }

        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern PasswordStorageError crypto_pwhash_str(
            byte* @out,
            byte* passwd,
            ulong passwdlen,
            ulong opslimit,
            UIntPtr memlimit
        );

        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern PasswordVerificationError crypto_pwhash_str_verify(
            byte* str,
            byte* passwd,
            ulong passwdlen
        );

        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        public static extern PasswordNeedsRehashResult crypto_pwhash_str_needs_rehash(
            byte* str,
            ulong opslimit,
            UIntPtr memlimit
        );
    }
}
