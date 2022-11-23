using System.Linq;

namespace Algorand.Unity.WalletConnect
{
    public static class DefaultBridge
    {
        public const string Domain = "walletconnect.org";

        public static readonly string[] BridgeUrls = Enumerable.Empty<string>()
            .Append("bridge")
            .Concat("abcdefghijklmnopqrstuvwxyz0123456789".Select(c => $"{c}.bridge"))
            .Select(x => $"https://{x}.{Domain}")
            .ToArray()
            ;

        public static string MainBridge = BridgeUrls[0];

        public static string GetRandomBridgeUrl()
        {
            var randomIndex = UnityEngine.Random.Range(0, BridgeUrls.Length);
            return BridgeUrls[randomIndex];
        }
    }
}
