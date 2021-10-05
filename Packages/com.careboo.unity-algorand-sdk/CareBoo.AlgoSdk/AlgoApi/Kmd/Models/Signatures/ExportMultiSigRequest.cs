using System;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct ExportMultiSigRequest
        : IEquatable<ExportMultiSigRequest>
    {
        [AlgoApiField("address", null)]
        public Address Address;

        [AlgoApiField("wallet_handle_token", null)]
        public FixedString128Bytes WalletHandleToken;

        public bool Equals(ExportMultiSigRequest other)
        {
            return Address.Equals(other.Address)
                && WalletHandleToken.Equals(other.WalletHandleToken)
                ;
        }
    }
}
