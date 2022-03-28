using System;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public partial struct RenewWalletHandleTokenRequest
        : IEquatable<RenewWalletHandleTokenRequest>
    {
        [AlgoApiField("wallet_handle_token", null)]
        public FixedString128Bytes WalletHandleToken;

        public bool Equals(RenewWalletHandleTokenRequest other)
        {
            return WalletHandleToken.Equals(other.WalletHandleToken);
        }
    }
}
