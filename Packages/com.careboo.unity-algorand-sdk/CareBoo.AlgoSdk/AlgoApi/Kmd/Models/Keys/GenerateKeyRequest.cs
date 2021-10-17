using System;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct GenerateKeyRequest
        : IEquatable<GenerateKeyRequest>
    {

        [AlgoApiField("display_mnemonic", null)]
        public Optional<bool> DisplayMnemonic;

        [AlgoApiField("wallet_handle_token", null)]
        public FixedString128Bytes WalletHandleToken;

        public bool Equals(GenerateKeyRequest other)
        {
            return DisplayMnemonic.Equals(other.DisplayMnemonic)
                && WalletHandleToken.Equals(other.WalletHandleToken)
                ;
        }
    }
}
