using System;
using AlgoSdk.Crypto;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct SignTransactionRequest
        : IEquatable<SignTransactionRequest>
    {
        [AlgoApiField("public_key", null)]
        public Ed25519.PublicKey PublicKey;

        [AlgoApiField("transaction", null)]
        public byte[] TransactionMsgPack;

        [AlgoApiField("wallet_handle_token", null)]
        public FixedString128Bytes WalletHandleToken;

        [AlgoApiField("wallet_password", null)]
        public FixedString128Bytes WalletPassword;

        public bool Equals(SignTransactionRequest other)
        {
            return PublicKey.Equals(other.PublicKey)
                && ArrayComparer.Equals(TransactionMsgPack, other.TransactionMsgPack)
                && WalletHandleToken.Equals(other.WalletHandleToken)
                && WalletPassword.Equals(other.WalletPassword)
                ;
        }
    }
}
