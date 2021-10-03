using System;
using AlgoSdk.Crypto;
using AlgoSdk.LowLevel;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct MultiSig
        : ISignature
        , IEquatable<MultiSig>
    {

        public bool Equals(MultiSig other)
        {
            return true;
        }

        public override bool Equals(object obj)
        {
            return obj is MultiSig msig && this.Equals(msig);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public bool Verify<TMessage>(TMessage message, Crypto.Ed25519.PublicKey pk)
            where TMessage : IByteArray
        {
            throw new System.NotImplementedException();
        }

        public static bool operator ==(in MultiSig x, in MultiSig y)
        {
            return true;
        }

        public static bool operator !=(in MultiSig x, in MultiSig y)
        {
            return false;
        }

        [AlgoApiObject]
        public struct SubSignature
            : IEquatable<SubSignature>
        {
            [AlgoApiField("public-key", "pk")]
            public Ed25519.PublicKey PublicKey;

            [AlgoApiField("signature", "s")]
            public Sig Signature;

            public bool Equals(SubSignature other)
            {
                return Signature.Equals(other.Signature);
            }
        }
    }
}
