using System;

namespace Algorand.Unity.Kmd
{
    [AlgoApiObject]
    public partial struct WalletHandle
        : IEquatable<WalletHandle>
    {
        [AlgoApiField("expires_seconds")]
        public ulong ExpiresSeconds;

        [AlgoApiField("wallet")]
        public Wallet Wallet;

        public bool Equals(WalletHandle other)
        {
            return Wallet.Equals(other.Wallet);
        }
    }
}
