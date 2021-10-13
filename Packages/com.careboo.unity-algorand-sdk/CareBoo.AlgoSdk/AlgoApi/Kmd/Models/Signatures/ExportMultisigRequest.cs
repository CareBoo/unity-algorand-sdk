using System;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct ExportMultisigRequest
        : IEquatable<ExportMultisigRequest>
    {
        [AlgoApiField("address", null)]
        public Address Address;

        [AlgoApiField("wallet_handle_token", null)]
        public FixedString128Bytes WalletHandleToken;

        public bool Equals(ExportMultisigRequest other)
        {
            return Address.Equals(other.Address)
                && WalletHandleToken.Equals(other.WalletHandleToken)
                ;
        }
    }
}
