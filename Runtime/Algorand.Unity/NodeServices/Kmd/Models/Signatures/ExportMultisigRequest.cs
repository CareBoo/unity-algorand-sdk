using System;
using Unity.Collections;

namespace Algorand.Unity.Kmd
{
    [AlgoApiObject]
    public partial struct ExportMultisigRequest
        : IEquatable<ExportMultisigRequest>
    {
        [AlgoApiField("address")]
        public Address Address;

        [AlgoApiField("wallet_handle_token")]
        public FixedString128Bytes WalletHandleToken;

        public bool Equals(ExportMultisigRequest other)
        {
            return Address.Equals(other.Address)
                && WalletHandleToken.Equals(other.WalletHandleToken)
                ;
        }
    }
}
