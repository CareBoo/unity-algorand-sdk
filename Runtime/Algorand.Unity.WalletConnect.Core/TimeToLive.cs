using System;

namespace Algorand.Unity.WalletConnect.Core
{
    public static class TimeToLive
    {
        public static long Now => ((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds();

        public static bool IsExpired(long expiry)
        {
            return Now >= expiry;
        }

        public static long GetExpiry(long seconds)
        {
            return Now + seconds;
        }

        public static long GetExpiry(long seconds, DateTime now)
        {
            return ((DateTimeOffset)now).ToUnixTimeSeconds() + seconds;
        }
    }
}