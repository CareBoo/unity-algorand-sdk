using System;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct ListKeysRequest
        : IEquatable<ListKeysRequest>
    {
        [AlgoApiField("wallet_handle_token", null)]
        public FixedString128Bytes WalletHandleToken;

        public bool Equals(ListKeysRequest other)
        {
            return WalletHandleToken.Equals(other.WalletHandleToken);
        }
    }
}
