using System;
using Unity.Collections;

namespace Algorand.Unity.Kmd
{
    [AlgoApiObject]
    public partial struct ImportKeyRequest
        : IEquatable<ImportKeyRequest>
    {
        [AlgoApiField("private_key")]
        public PrivateKey PrivateKey;

        [AlgoApiField("wallet_handle_token")]
        public FixedString128Bytes WalletHandleToken;

        public bool Equals(ImportKeyRequest other)
        {
            return PrivateKey.Equals(other.PrivateKey)
                && WalletHandleToken.Equals(other.WalletHandleToken)
                ;
        }
    }
}
