namespace AlgoSdk
{
    /// <summary>
    /// Contains utility functions for generating new accounts (private keys).
    /// </summary>
    public static class Account
    {
        /// <summary>
        /// Generate a fresh account
        /// </summary>
        /// <returns>A private key, address tuple.</returns>
        public static (PrivateKey, Address) GenerateAccount()
        {
            var privateKey = AlgoSdk.Crypto.Random.Bytes<PrivateKey>();
            return (privateKey, privateKey.ToPublicKey());
        }
    }
}
