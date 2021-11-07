using System;
using AlgoSdk.Crypto;
using AlgoSdk.LowLevel;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct Multisig
        : ISignature
        , IEquatable<Multisig>
    {
        [AlgoApiField("subsignature", "subsig")]
        public Subsig[] Subsigs;

        [AlgoApiField("threshold", "thr")]
        public byte Threshold;

        [AlgoApiField("version", "v")]
        public byte Version;

        public bool Equals(Multisig other)
        {
            return ArrayComparer.Equals(Subsigs, other.Subsigs)
                && Threshold.Equals(other.Threshold)
                && Version.Equals(other.Version)
                ;
        }

        public bool Verify<TMessage>(TMessage message)
            where TMessage : IByteArray
        {
            byte verified = 0;
            if (Subsigs != null)
            {
                for (var i = 0; i < Subsigs.Length; i++)
                {
                    var pk = Subsigs[i].PublicKey;
                    var sig = Subsigs[i].Sig;
                    if (sig.Verify(message, pk))
                        verified++;
                }
            }
            return verified >= Threshold;
        }

        [AlgoApiObject]
        public struct Subsig
            : IEquatable<Subsig>
        {
            [AlgoApiField("public-key", "pk")]
            public Ed25519.PublicKey PublicKey;

            [AlgoApiField("signature", "s")]
            public Sig Sig;

            public bool Equals(Subsig other)
            {
                return Sig.Equals(other.Sig);
            }

            public static implicit operator Subsig(Address address)
            {
                return new Subsig { PublicKey = address };
            }
        }
    }
}
