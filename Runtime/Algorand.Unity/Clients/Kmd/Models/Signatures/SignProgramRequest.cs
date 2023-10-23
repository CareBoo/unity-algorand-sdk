using System;
using Unity.Collections;

namespace Algorand.Unity.Kmd
{
    [AlgoApiObject]
    public partial struct SignProgramRequest
        : IEquatable<SignProgramRequest>
    {
        [AlgoApiField("address")]
        public Address Address;

        [AlgoApiField("data")]
        public byte[] Data;

        [AlgoApiField("wallet_handle_token")]
        public FixedString128Bytes WalletHandleToken;

        [AlgoApiField("wallet_password")]
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
