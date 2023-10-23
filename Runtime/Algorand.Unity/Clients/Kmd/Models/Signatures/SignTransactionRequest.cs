using System;
using Algorand.Unity.Crypto;
using Unity.Collections;

namespace Algorand.Unity.Kmd
{
    [AlgoApiObject]
    public partial struct SignTransactionRequest
        : IEquatable<SignTransactionRequest>
    {
        [AlgoApiField("public_key")]
        public Ed25519.PublicKey PublicKey;

        [AlgoApiField("transaction")]
        public byte[] Transaction;

        [AlgoApiField("wallet_handle_token")]
        public FixedString128Bytes WalletHandleToken;

        [AlgoApiField("wallet_password")]
        public FixedString128Bytes WalletPassword;

        public bool Equals(SignTransactionRequest other)
        {
            return PublicKey.Equals(other.PublicKey)
                && ArrayComparer.Equals(Transaction, other.Transaction)
                && WalletHandleToken.Equals(other.WalletHandleToken)
                && WalletPassword.Equals(other.WalletPassword)
                ;
        }
    }
}
