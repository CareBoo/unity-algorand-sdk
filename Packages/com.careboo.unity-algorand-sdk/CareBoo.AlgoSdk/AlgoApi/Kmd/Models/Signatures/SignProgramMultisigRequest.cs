using System;
using AlgoSdk.Crypto;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public partial struct SignProgramMultisigRequest
        : IEquatable<SignProgramMultisigRequest>
    {
        [AlgoApiField("address", null)]
        public Address Address;

        [AlgoApiField("data", null)]
        public byte[] Data;

        [AlgoApiField("partial_multisig", null)]
        public Multisig Multisig;

        [AlgoApiField("public_key", null)]
        public Ed25519.PublicKey PublicKey;

        [AlgoApiField("wallet_handle_token", null)]
        public FixedString128Bytes WalletHandleToken;

        [AlgoApiField("wallet_password", null)]
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
