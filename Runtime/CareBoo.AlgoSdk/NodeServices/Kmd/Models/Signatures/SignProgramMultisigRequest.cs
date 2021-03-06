using System;
using AlgoSdk.Crypto;
using Unity.Collections;

namespace AlgoSdk.Kmd
{
    [AlgoApiObject]
    public partial struct SignProgramMultisigRequest
        : IEquatable<SignProgramMultisigRequest>
    {
        [AlgoApiField("address")]
        public Address Address;

        [AlgoApiField("data")]
        public byte[] Data;

        [AlgoApiField("partial_multisig")]
        public MultisigSig Multisig;

        [AlgoApiField("public_key")]
        public Ed25519.PublicKey PublicKey;

        [AlgoApiField("wallet_handle_token")]
        public FixedString128Bytes WalletHandleToken;

        [AlgoApiField("wallet_password")]
        public FixedString128Bytes WalletPassword;

        public bool Equals(SignProgramMultisigRequest other)
        {
            return Address.Equals(other.Address)
                && ArrayComparer.Equals(Data, other.Data)
                && Multisig.Equals(other.Multisig)
                && PublicKey.Equals(other.PublicKey)
                && WalletHandleToken.Equals(other.WalletHandleToken)
                && WalletPassword.Equals(other.WalletPassword)
                ;
        }
    }
}
