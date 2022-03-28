using System;
using AlgoSdk.Crypto;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public partial struct SignMultisigRequest
        : IEquatable<SignMultisigRequest>
    {
        [AlgoApiField("partial_multisig")]
        public Multisig Multisig;

        [AlgoApiField("public_key")]
        public Ed25519.PublicKey PublicKey;

        [AlgoApiField("signer")]
        public Address Signer;

        [AlgoApiField("transaction")]
        public byte[] Transaction;

        [AlgoApiField("wallet_handle_token")]
        public FixedString128Bytes WalletHandleToken;

        [AlgoApiField("wallet_password")]
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
