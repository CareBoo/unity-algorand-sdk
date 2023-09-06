using System;
using UnityEngine;

namespace Algorand.Unity.WalletConnect.Core
{
    [AlgoApiObject]
    [Serializable]
    public partial struct SessionProposeRequestPayload : IEquatable<SessionProposeRequestPayload>
    {
        [SerializeField]
        private ProtocolOptions[] relays;

        [SerializeField]
        private RequiredNamespaces requiredNamespaces;

        [SerializeField]
        private OptionalNamespaces optionalNamespaces;

        [SerializeField]
        private SessionProperties sessionProperties;

        [SerializeField]
        private Participant proposer;

        [AlgoApiField("relays")]
        public ProtocolOptions[] Relays
        {
            get => relays;
            set => relays = value;
        }

        [AlgoApiField("requiredNamespaces")]
        public RequiredNamespaces RequiredNamespaces
        {
            get => requiredNamespaces;
            set => requiredNamespaces = value;
        }

        [AlgoApiField("optionalNamespaces")]
        public OptionalNamespaces OptionalNamespaces
        {
            get => optionalNamespaces;
            set => optionalNamespaces = value;
        }

        [AlgoApiField("session")]
        public SessionProperties SessionProperties
        {
            get => sessionProperties;
            set => sessionProperties = value;
        }

        [AlgoApiField("proposer")]
        public Participant Proposer
        {
            get => proposer;
            set => proposer = value;
        }

        public SessionProposeRequestPayload(
            Participant proposer
        )
        {
            relays = new[] { new ProtocolOptions { Protocol = "irn" } };
            requiredNamespaces = new RequiredNamespaces { AlgorandNamespace = Namespace.AlgorandNamespace };
            optionalNamespaces = default;
            sessionProperties = default;
            this.proposer = proposer;
        }

        public bool Equals(SessionProposeRequestPayload other)
        {
            return ArrayComparer.Equals(Relays, other.Relays)
                && RequiredNamespaces.Equals(other.RequiredNamespaces)
                && OptionalNamespaces.Equals(other.OptionalNamespaces)
                && SessionProperties.Equals(other.SessionProperties)
                && Proposer.Equals(other.Proposer);
        }
    }

    [AlgoApiObject]
    [Serializable]
    public partial struct SessionProposeResponsePayload : IEquatable<SessionProposeResponsePayload>
    {
        [SerializeField]
        private ProtocolOptions relay;

        [SerializeField]
        private string responderPublicKey;

        [AlgoApiField("relay")]
        public ProtocolOptions Relay
        {
            get => relay;
            set => relay = value;
        }

        [AlgoApiField("responderPublicKey")]
        public string ResponderPublicKey
        {
            get => responderPublicKey;
            set => responderPublicKey = value;
        }

        public bool Equals(SessionProposeResponsePayload other)
        {
            return Relay.Equals(other.Relay)
                && StringComparer.Equals(ResponderPublicKey, other.ResponderPublicKey);
        }
    }
}
