using Unity.Collections;
using Unity.Jobs;

namespace Algorand.Unity.Crypto
{
    public static partial class Ed25519
    {
        public struct KeyPair : INativeDisposable
        {
            public SecretKeyHandle SecretKey;
            public PublicKey PublicKey;

            public KeyPair(SecretKeyHandle secretKey, PublicKey publicKey)
            {
                SecretKey = secretKey;
                PublicKey = publicKey;
            }

            public JobHandle Dispose(JobHandle inputDeps)
            {
                return SecretKey.Dispose(inputDeps);
            }

            public void Dispose()
            {
                SecretKey.Dispose();
            }

            public void Deconstruct(out SecretKeyHandle secretKey, out PublicKey publicKey)
            {
                secretKey = SecretKey;
                publicKey = PublicKey;
            }
        }
    }
}