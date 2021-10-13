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
        [AlgoApiField("subsig", "subsig")]
        public SubSignature[] SubSignatures;

        [AlgoApiField("thr", "thr")]
        public byte Threshold;

        [AlgoApiField("v", "v")]
        public byte Version;

        public bool Equals(MultiSig other)
        {
            return ArrayComparer.Equals(SubSignatures, other.SubSignatures)
                && Threshold.Equals(other.Threshold)
                && Version.Equals(other.Version)
                ;
        }

        public bool Verify<TMessage>(TMessage message)
            where TMessage : IByteArray
        {
            byte verified = 0;
            if (SubSignatures != null)
            {
                for (var i = 0; i < SubSignatures.Length; i++)
                {
                    var pk = SubSignatures[i].PublicKey;
                    var sig = SubSignatures[i].Signature;
                    if (sig.Verify(message, pk))
                        verified++;
                }
            }
            return verified >= Threshold;
        }

        [AlgoApiObject]
        public struct SubSignature
            : IEquatable<SubSignature>
        {
            [AlgoApiField("pk", "pk")]
            public Ed25519.PublicKey PublicKey;

            [AlgoApiField("s", "s")]
            public Sig Signature;

            public bool Equals(SubSignature other)
            {
                return Signature.Equals(other.Signature);
            }

            public static implicit operator SubSignature(Address address)
            {
                return new SubSignature { PublicKey = address };
            }
        }
    }
}
