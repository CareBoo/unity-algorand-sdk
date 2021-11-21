using System;

namespace AlgoSdk.WalletConnect
{
    [AlgoApiObject]
    public struct MultisigMetadata
        : IEquatable<MultisigMetadata>
    {
        [AlgoApiField("version", null)]
        public byte Version;

        [AlgoApiField("threshold", null)]
        public byte Threshold;

        [AlgoApiField("addrs", null)]
        public Address[] Addresses;

        public bool Equals(MultisigMetadata other)
        {
            return Version.Equals(other.Version)
                && Threshold.Equals(other.Threshold)
                && ArrayComparer.Equals(Addresses, other.Addresses)
                ;
        }

        public static implicit operator MultisigMetadata(Multisig msig)
        {
            var addresses = new Address[msig.Subsigs.Length];
            for (var i = 0; i < addresses.Length; i++)
                addresses[i] = msig.Subsigs[i].PublicKey;
            return new MultisigMetadata
            {
                Version = msig.Version,
                Threshold = msig.Threshold,
                Addresses = addresses
            };
        }

        public static implicit operator Multisig(MultisigMetadata msigMeta)
        {
            var subsigs = new Multisig.Subsig[msigMeta.Addresses.Length];
            for (var i = 0; i < subsigs.Length; i++)
                subsigs[i] = new Multisig.Subsig { PublicKey = msigMeta.Addresses[i] };
            return new Multisig
            {
                Version = msigMeta.Version,
                Threshold = msigMeta.Threshold,
                Subsigs = subsigs
            };
        }
    }
}
