using System;

namespace Algorand.Unity.WalletConnect.Core
{
    /// <summary>
    /// Hardcoded to be empty, for Algorand.
    /// </summary>
    [AlgoApiObject]
    [Serializable]
    public partial struct SessionProperties : IEquatable<SessionProperties>
    {
        public bool Equals(SessionProperties other)
        {
            return true;
        }
    }
}
