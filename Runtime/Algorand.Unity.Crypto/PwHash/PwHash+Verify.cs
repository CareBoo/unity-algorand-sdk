namespace Algorand.Unity.Crypto
{
    public enum PasswordVerificationError
    {
        Success = 0,
        VerificationFailed = -1,
    }

    public partial struct PwHash
    {
        public PasswordVerificationError Verify(SodiumString password)
        {
            unsafe
            {
                return (PasswordVerificationError)sodium.crypto_pwhash_str_verify(
                    GetUnsafePtr(),
                    password.GetUnsafePtr(),
                    (ulong)password.Length
                );
            }
        }
    }
}
