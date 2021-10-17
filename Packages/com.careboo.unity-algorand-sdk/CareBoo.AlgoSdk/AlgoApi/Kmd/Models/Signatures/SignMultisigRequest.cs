using System;
using AlgoSdk.Crypto;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct SignMultisigRequest
        : IEquatable<SignMultisigRequest>
    {
        [AlgoApiField("partial_multisig", null)]
        public Multisig Multisig;

        [AlgoApiField("public_key", null)]
        public Ed25519.PublicKey PublicKey;

        [AlgoApiField("signer", null)]
        public Address Signer;

        [AlgoApiField("transaction", null)]
        public byte[] Transaction;

        [AlgoApiField("wallet_handle_token", null)]
        public FixedString128Bytes WalletHandleToken;

        [AlgoApiField("wallet_password", null)]
        public FixedString128Bytes WalletPassword;

        public bool Equals(SignMultisigRequest other)
        {
            return Multisig.Equals(other.Multisig)
                && PublicKey.Equals(other.PublicKey)
                && ArrayComparer.Equals(Signer, other.Signer)
                && ArrayComparer.Equals(Transaction, other.Transaction)
                && WalletHandleToken.Equals(other.WalletHandleToken)
                && WalletPassword.Equals(other.WalletPassword)
                ;
        }
    }
}
