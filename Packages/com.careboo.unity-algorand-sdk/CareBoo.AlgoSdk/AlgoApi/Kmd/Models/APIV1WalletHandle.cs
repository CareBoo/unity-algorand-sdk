using System;

namespace AlgoSdk
{
    public struct APIV1WalletHandle
        : IEquatable<APIV1WalletHandle>
    {
        [AlgoApiField("expires_seconds", null)]
        public ulong ExpiresSeconds;

        [AlgoApiField("wallet", null)]
        public APIV1Wallet Wallet;

        public bool Equals(APIV1WalletHandle other)
        {
            return Wallet.Equals(other.Wallet);
        }
    }
}
