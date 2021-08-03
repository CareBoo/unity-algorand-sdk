using AlgoSdk.Crypto;
using Unity.Collections;
using Unity.Jobs;

namespace AlgoSdk
{
    public readonly struct Account : INativeDisposable
    {
        readonly public Ed25519.SecretKeyHandle SecretKey;
        readonly public Address Address;

        public Account(in PrivateKey privateKey)
        {
            (SecretKey, Address) = privateKey.ToKeys();
        }

        public static Account Generate()
        {
            var privateKey = Crypto.Random.Bytes<PrivateKey>();
            return new Account(in privateKey);
        }

        public JobHandle Dispose(JobHandle inputDeps)
        {
            return SecretKey.Dispose(inputDeps);
        }

        public void Dispose()
        {
            SecretKey.Dispose();
        }
    }
}
