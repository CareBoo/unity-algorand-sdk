using System;
using Unity.Collections;

namespace Algorand.Unity.Kmd
{
    [AlgoApiObject]
    public partial struct ListKeysRequest
        : IEquatable<ListKeysRequest>
    {
        [AlgoApiField("wallet_handle_token")]
        public FixedString128Bytes WalletHandleToken;

        public bool Equals(ListKeysRequest other)
        {
            return WalletHandleToken.Equals(other.WalletHandleToken);
        }
    }
}
