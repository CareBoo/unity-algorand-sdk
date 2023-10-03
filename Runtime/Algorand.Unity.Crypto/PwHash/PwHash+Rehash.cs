using System;

namespace Algorand.Unity.Crypto
{
    public enum PasswordNeedsRehashResult
    {
        Success = 0,
        Error = -1,
        NeedsRehash = 1
    }

    public partial struct PwHash
    {
        public PasswordNeedsRehashResult NeedsRehash(
            OpsLimit opsLimit = OpsLimit.Interactive,
            MemLimit memLimit = MemLimit.Interactive
        )
        {
            unsafe
            {
                return (PasswordNeedsRehashResult)sodium.crypto_pwhash_str_needs_rehash(
                    GetUnsafePtr(),
                    (ulong)opsLimit,
                    unchecked((UIntPtr)memLimit)
                );
            }
        }
    }
}
