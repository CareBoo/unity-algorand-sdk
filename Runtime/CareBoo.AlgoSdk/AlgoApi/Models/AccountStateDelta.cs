using System;

namespace AlgoSdk
{
    /// <summary>
    /// Application state delta for an account <see cref="Address"/>.
    /// </summary>
    [AlgoApiObject]
    [Serializable]
    public partial struct AccountStateDelta
        : IEquatable<AccountStateDelta>
    {
        [AlgoApiField("address", null)]
        public Address Address;

        [AlgoApiField("delta", null)]
        public AppStateDelta Delta;

        public bool Equals(AccountStateDelta other)
        {
            return Address.Equals(other.Address)
                && ArrayComparer.Equals(Delta, other.Delta);
        }
    }
}
