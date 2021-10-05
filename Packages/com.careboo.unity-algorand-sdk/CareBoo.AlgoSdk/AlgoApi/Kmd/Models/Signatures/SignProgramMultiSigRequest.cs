using System;
using AlgoSdk.Crypto;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct SignProgramMultiSigRequest
        : IEquatable<SignProgramMultiSigRequest>
    {
        [AlgoApiField("address", null)]
        public Address Address;

        [AlgoApiField("data", null)]
        public byte[] Data;

        [AlgoApiField("partial_multisig", null)]
        public MultiSig MultiSig;

        [AlgoApiField("public_key", null)]
        public Ed25519.PublicKey PublicKey;

        [AlgoApiField("wallet_handle_token", null)]
        public FixedString128Bytes WalletHandleToken;

        [AlgoApiField("wallet_password", null)]
        public FixedString128Bytes WalletPassword;

        public bool Equals(SignProgramMultiSigRequest other)
        {
            return Address.Equals(other.Address)
                && ArrayComparer.Equals(Data, other.Data)
                && MultiSig.Equals(other.MultiSig)
                && PublicKey.Equals(other.PublicKey)
                && WalletHandleToken.Equals(other.WalletHandleToken)
                && WalletPassword.Equals(other.WalletPassword)
                ;
        }
    }
}
