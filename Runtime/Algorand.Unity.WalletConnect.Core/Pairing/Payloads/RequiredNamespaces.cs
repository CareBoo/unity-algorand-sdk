using System;
using UnityEngine;

namespace Algorand.Unity.WalletConnect.Core
{
    /// <summary>
    /// Hard-coded to Algorand.
    /// </summary>
    [AlgoApiObject]
    [Serializable]
    public partial struct RequiredNamespaces : IEquatable<RequiredNamespaces>
    {
        [SerializeField]
        private Namespace algorand;

        [AlgoApiField("algorand")]
        public Namespace AlgorandNamespace
        {
            get => algorand;
            set => algorand = value;
        }

        public bool Equals(RequiredNamespaces other)
        {
            return AlgorandNamespace.Equals(other.AlgorandNamespace);
        }
    }
}
