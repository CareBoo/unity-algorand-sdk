using System;
using Unity.Collections;
using UnityEngine;

namespace AlgoSdk.Kmd
{
    [AlgoApiObject]
    public partial struct ExportKeyRequest
        : IEquatable<ExportKeyRequest>
    {
        [AlgoApiField("address")]
        public Address Address;

        [AlgoApiField("wallet_handle_token")]
        public FixedString128Bytes WalletHandleToken;

        [AlgoApiField("wallet_password")]
        public FixedString128Bytes WalletPassword;

        public bool Equals(ExportKeyRequest other)
        {
            return Address.Equals(other.Address)
                && WalletHandleToken.Equals(other.WalletHandleToken)
                && WalletPassword.Equals(other.WalletPassword)
                ;
        }
    }
}
