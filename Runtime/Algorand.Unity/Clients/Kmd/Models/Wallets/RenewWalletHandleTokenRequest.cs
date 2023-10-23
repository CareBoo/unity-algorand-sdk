using System;
using Unity.Collections;

namespace Algorand.Unity.Kmd
{
    [AlgoApiObject]
    public partial struct RenewWalletHandleTokenRequest
        : IEquatable<RenewWalletHandleTokenRequest>
    {
        [AlgoApiField("wallet_handle_token")]
        public FixedString128Bytes WalletHandleToken;

        public bool Equals(RenewWalletHandleTokenRequest other)
        {
            return WalletHandleToken.Equals(other.WalletHandleToken);
        }
    }
}
