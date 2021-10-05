using System;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct GenerateMasterKeyRequest
        : IEquatable<GenerateMasterKeyRequest>
    {

        [AlgoApiField("display_mnemonic", null)]
        public Optional<bool> DisplayMnemonic;

        [AlgoApiField("wallet_password", null)]
        public FixedString128Bytes WalletPassword;

        public bool Equals(GenerateMasterKeyRequest other)
        {
            return DisplayMnemonic.Equals(other.DisplayMnemonic)
                && WalletPassword.Equals(other.WalletPassword)
                ;
        }
    }
}
