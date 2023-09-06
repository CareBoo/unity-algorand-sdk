using System;

namespace Algorand.Unity.WalletConnect.Core
{
    /// <summary>
    /// Hardcoded to be empty, for Algorand.
    /// </summary>
    [AlgoApiObject]
    [Serializable]
    public partial struct OptionalNamespaces : IEquatable<OptionalNamespaces>
    {
        public bool Equals(OptionalNamespaces other)
        {
            return true;
        }
    }
}
