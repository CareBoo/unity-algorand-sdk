using System;

namespace Algorand.Unity.WalletConnect
{
    public static class RpcPayload
    {
        public const int IdRandomPartSize = 1000;

        public static long GenerateId()
        {
            var datePart = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() * IdRandomPartSize;
            var randomPart = UnityEngine.Random.Range(0, IdRandomPartSize);
            return datePart + randomPart;
        }
    }
}
