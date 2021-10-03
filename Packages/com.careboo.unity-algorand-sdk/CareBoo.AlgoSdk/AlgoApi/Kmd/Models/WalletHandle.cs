using System;

namespace AlgoSdk
{
    public struct WalletHandle
        : IEquatable<WalletHandle>
    {
        [AlgoApiField("expires_seconds", null)]
        public ulong ExpiresSeconds;

        [AlgoApiField("wallet", null)]
        public Wallet Wallet;

        public bool Equals(WalletHandle other)
        {
            return Wallet.Equals(other.Wallet);
        }
    }
}
