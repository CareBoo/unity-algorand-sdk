using System;
using Unity.Collections;

namespace AlgoSdk.Kmd
{
    [AlgoApiObject]
    public partial struct ReleaseWalletHandleTokenRequest
        : IEquatable<ReleaseWalletHandleTokenRequest>
    {
        [AlgoApiField("wallet_handle_token")]
        public FixedString128Bytes WalletHandleToken;

        public bool Equals(ReleaseWalletHandleTokenRequest other)
        {
            return WalletHandleToken.Equals(other.WalletHandleToken);
        }
    }
}
