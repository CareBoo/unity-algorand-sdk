using System;
using UnityEngine;

namespace Algorand.Unity.WalletConnect.Core
{
    [AlgoApiObject]
    [Serializable]
    public partial struct Namespace : IEquatable<Namespace>
    {
        [SerializeField]
        private string[] chains;

        [SerializeField]
        private string[] methods;

        [SerializeField]
        private string[] events;

        [AlgoApiField("chains")]
        public string[] Chains
        {
            get => chains;
            set => chains = value;
        }

        [AlgoApiField("methods")]
        public string[] Methods
        {
            get => methods;
            set => methods = value;
        }

        [AlgoApiField("events")]
        public string[] Events
        {
            get => events;
            set => events = value;
        }

        public bool Equals(Namespace other)
        {
            return ArrayComparer.Equals(Chains, other.Chains)
                && ArrayComparer.Equals(Methods, other.Methods)
                && ArrayComparer.Equals(Events, other.Events);
        }

        public static readonly Namespace AlgorandNamespace = new Namespace
        {
            chains = new[] { "algorand:SGO1GKSzyE7IEPItTxCByw9x8FmnrCDe" },
            methods = new[] { "algo_signTxn" },
            events = new[] { "chainChanged", "accountsChanged" }
        };
    }
}
