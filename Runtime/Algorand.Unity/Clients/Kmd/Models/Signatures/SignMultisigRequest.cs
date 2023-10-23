using System;
using Algorand.Unity.Crypto;
using Unity.Collections;

namespace Algorand.Unity.Kmd
{
    [AlgoApiObject]
    public partial struct SignMultisigRequest
        : IEquatable<SignMultisigRequest>
    {
        [AlgoApiField("partial_multisig")]
        public MultisigSig Multisig;

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
