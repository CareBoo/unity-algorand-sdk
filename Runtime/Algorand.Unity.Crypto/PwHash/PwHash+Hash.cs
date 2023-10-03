using System;

namespace Algorand.Unity.Crypto
{
    public partial struct PwHash
    {
        public enum HashAlgorithm
        {
            None,
            Default = sodium.crypto_pwhash_ALG_DEFAULT,
            Argon2I13 = sodium.crypto_pwhash_ALG_ARGON2I13,
            Argon2ID13 = sodium.crypto_pwhash_ALG_ARGON2ID13
        }

        public enum HashError
        {
            None = 0,
            OutOfMemory = -1
        }

        /// <summary>
        /// Hash a password with the given salt. If there is an error, the out handle will not be allocated.
        /// </summary>
        /// <param name="passwd"></param>
        /// <param name="salt"></param>
        /// <param name="outlen"></param>
        /// <param name="out"></param>
        /// <param name="alg"></param>
        /// <param name="opsLimit"></param>
        /// <param name="memLimit"></param>
        /// <returns></returns>
        public static HashError Hash(
            SodiumString passwd,
            Salt salt,
            uint outlen,
            out SecureMemoryHandle @out,
            HashAlgorithm alg = HashAlgorithm.Default,
            OpsLimit opsLimit = OpsLimit.Interactive,
            MemLimit memLimit = MemLimit.Interactive
        )
        {
            HashError err;
            @out = SecureMemoryHandle.Create((UIntPtr)outlen);
            unsafe
            {
                err = (HashError)sodium.crypto_pwhash(
                    (byte*)@out.Ptr,
                    outlen,
                    passwd.GetUnsafePtr(),
                    (ulong)passwd.Length,
                    salt.bytes,
                    (ulong)opsLimit,
                    (UIntPtr)memLimit,
                    (int)alg
                );
            }
            if (err != HashError.None)
            {
                @out.Dispose();
                @out = default;
            }
            return err;
        }
    }
}
