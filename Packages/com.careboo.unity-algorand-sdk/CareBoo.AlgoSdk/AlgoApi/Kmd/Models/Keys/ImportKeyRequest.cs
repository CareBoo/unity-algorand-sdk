using System;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct ImportKeyRequest
        : IEquatable<ImportKeyRequest>
    {

        [AlgoApiField("private_key", null)]
        public PrivateKey PrivateKey;

        [AlgoApiField("wallet_password", null)]
        public FixedString128Bytes WalletPassword;

        public bool Equals(ImportKeyRequest other)
        {
            return PrivateKey.Equals(other.PrivateKey)
                && WalletPassword.Equals(other.WalletPassword)
                ;
        }
    }
}
