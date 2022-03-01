using System;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public partial struct WalletInfoRequest
        : IEquatable<WalletInfoRequest>
    {
        [AlgoApiField("wallet_handle_token", null)]
        public FixedString128Bytes WalletHandleToken;

        public bool Equals(WalletInfoRequest other)
        {
            return WalletHandleToken.Equals(other.WalletHandleToken);
        }
    }
}
