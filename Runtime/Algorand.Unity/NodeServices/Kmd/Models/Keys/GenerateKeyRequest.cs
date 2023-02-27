using System;
using Unity.Collections;

namespace Algorand.Unity.Kmd
{
    [AlgoApiObject]
    public partial struct GenerateKeyRequest
        : IEquatable<GenerateKeyRequest>
    {

        [AlgoApiField("display_mnemonic")]
        public Optional<bool> DisplayMnemonic;

        [AlgoApiField("wallet_handle_token")]
        public FixedString128Bytes WalletHandleToken;

        public bool Equals(GenerateKeyRequest other)
        {
            return DisplayMnemonic.Equals(other.DisplayMnemonic)
                && WalletHandleToken.Equals(other.WalletHandleToken)
                ;
        }
    }
}
