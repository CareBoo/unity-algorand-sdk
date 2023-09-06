using System;

namespace Algorand.Unity.WalletConnect.Core
{
    public static class RpcId
    {
        public const int DefaultEntropyPower = 10 * 10 * 10 * 10 * 10 * 10;

        public static ulong GenerateId(byte entropyDigits = 6)
        {
            ulong entropyPower = IPow(10, entropyDigits);
            var entropy = Crypto.Random.Bytes<ulong>();
            var timestampEpoch = (ulong)((DateTimeOffset)DateTime.UtcNow).ToUnixTimeMilliseconds();
            return entropy % entropyPower
                + timestampEpoch * entropyPower;
        }

        private static ulong IPow(ulong x, byte exp)
        {
            var result = 1UL;
            while (exp > 0)
            {
                if ((exp & 1) != 0)
                {
                    result *= x;
                }
                exp >>= 1;
                result *= result;
            }
            return result;
        }
    }
}
