using System;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct ListMultiSigRequest
        : IEquatable<ListMultiSigRequest>
    {
        [AlgoApiField("wallet_handle_token", null)]
        public FixedString128Bytes WalletHandleToken;

        public bool Equals(ListMultiSigRequest other)
        {
            return WalletHandleToken.Equals(other.WalletHandleToken);
        }
    }
}
