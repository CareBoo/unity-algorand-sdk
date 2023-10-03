namespace Algorand.Unity.Crypto
{
    public static partial class X25519
    {
        public static (PublicKey publicKey, SecretKey secretKey) Keygen()
        {
            var publicKey = default(PublicKey);
            var secretKey = default(SecretKey);

            unsafe
            {
                sodium.crypto_box_keypair(&publicKey, &secretKey);
            }
            return (publicKey, secretKey);
        }
    }
}
