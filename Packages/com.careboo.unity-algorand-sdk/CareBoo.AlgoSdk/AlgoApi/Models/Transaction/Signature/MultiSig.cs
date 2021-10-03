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
        [AlgoApiField("subsignature", "subsig")]
        public SubSignature[] SubSignatures;

        [AlgoApiField("threshold", "thr")]
        public ulong Threshold;

        [AlgoApiField("version", "v")]
        public ulong Version;

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
            ulong verified = 0;
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
