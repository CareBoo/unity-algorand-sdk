using System;
using Unity.Collections;

namespace AlgoSdk
{
    public struct DeleteMultiSigRequest
        : IEquatable<DeleteMultiSigRequest>
    {
        [AlgoApiField("address", null)]
        public Address Address;

        [AlgoApiField("wallet_handle_token", null)]
        public FixedString128Bytes WalletHandleToken;

        [AlgoApiField("wallet_password", null)]
        public FixedString128Bytes WalletPassword;

        public bool Equals(DeleteMultiSigRequest other)
        {
            return Address.Equals(other.Address)
                && WalletHandleToken.Equals(other.WalletHandleToken)
                && WalletPassword.Equals(other.WalletPassword)
                ;
        }
    }
}
