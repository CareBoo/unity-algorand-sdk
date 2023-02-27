using System;

namespace Algorand.Unity.WalletConnect
{
    /// <summary>
    /// This struct represents metadata required for signing transactions sent by
    /// multisig accounts via WalletConnect. See <see cref="MultisigSig"/> for more information.
    /// </summary>
    [AlgoApiObject]
    public partial struct MultisigMetadata
        : IEquatable<MultisigMetadata>
    {
        /// <summary>
        /// Version of the multisig.
        /// </summary>
        [AlgoApiField("version")]
        public byte Version;

        /// <summary>
        /// Number of signatures required for valid transaction.
        /// </summary>
        [AlgoApiField("threshold")]
        public byte Threshold;

        /// <summary>
        /// The signers of this multisig.
        /// </summary>
        [AlgoApiField("addrs")]
        public Address[] Addresses;

        public bool Equals(MultisigMetadata other)
        {
            return Version.Equals(other.Version)
                && Threshold.Equals(other.Threshold)
                && ArrayComparer.Equals(Addresses, other.Addresses)
                ;
        }

        public static implicit operator MultisigMetadata(MultisigSig msig)
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

        public static implicit operator MultisigSig(MultisigMetadata msigMeta)
        {
            var subsigs = new MultisigSig.Subsig[msigMeta.Addresses.Length];
            for (var i = 0; i < subsigs.Length; i++)
                subsigs[i] = new MultisigSig.Subsig { PublicKey = msigMeta.Addresses[i] };
            return new MultisigSig
            {
                Version = msigMeta.Version,
                Threshold = msigMeta.Threshold,
                Subsigs = subsigs
            };
        }
    }
}
