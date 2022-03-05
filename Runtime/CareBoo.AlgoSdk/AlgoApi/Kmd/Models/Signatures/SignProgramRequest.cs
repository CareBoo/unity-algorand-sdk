using System;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public partial struct SignProgramRequest
        : IEquatable<SignProgramRequest>
    {
        [AlgoApiField("address", null)]
        public Address Address;

        [AlgoApiField("data", null)]
        public byte[] Data;

        [AlgoApiField("wallet_handle_token", null)]
        public FixedString128Bytes WalletHandleToken;

        [AlgoApiField("wallet_password", null)]
        public FixedString128Bytes WalletPassword;

        public bool Equals(SignProgramRequest other)
        {
            return Address.Equals(other.Address)
                && ArrayComparer.Equals(Data, other.Data)
                && WalletHandleToken.Equals(other.WalletHandleToken)
                && WalletPassword.Equals(other.WalletPassword)
                ;
        }
    }
}
