using Algorand.Unity.LowLevel;
using Algorand.Utils.Crypto;
using Algorand.Crypto;

namespace Algorand.Unity.Net
{
    public static class PrivateKeyExtensions
    {
        public static KeyPair ToDotnet(this PrivateKey from)
        {
            var fsr = new FixedSecureRandom(from.ToArray());
            return new KeyPair(fsr);
        }

        public static PrivateKey ToUnity(this KeyPair from)
        {
            var pkBytes = from.ClearTextPrivateKey;
            var result = new PrivateKey();
            result.CopyFrom(pkBytes, 0, 32);
            return result;
        }
    }
}
