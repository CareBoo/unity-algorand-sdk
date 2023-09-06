using System;
using UnityEngine;

namespace Algorand.Unity.WalletConnect.Core
{
    [AlgoApiObject]
    [Serializable]
    public partial struct Participant : IEquatable<Participant>
    {
        [SerializeField]
        private string publicKey;

        [SerializeField]
        private Metadata metadata;

        [AlgoApiField("publicKey")]
        public string PublicKey
        {
            get => publicKey;
            set => publicKey = value;
        }

        [AlgoApiField("metadata")]
        public Metadata Metadata
        {
            get => metadata;
            set => metadata = value;
        }

        public bool Equals(Participant other)
        {
            return StringComparer.Equals(PublicKey, other.PublicKey)
                && Metadata.Equals(other.Metadata);
        }
    }
}
