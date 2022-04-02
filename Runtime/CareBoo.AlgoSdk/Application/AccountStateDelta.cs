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
        [AlgoApiField("address")]
        public Address Address;

        [AlgoApiField("delta")]
        public AppStateDelta Delta;

        public bool Equals(AccountStateDelta other)
        {
            return Address.Equals(other.Address)
                && ArrayComparer.Equals(Delta, other.Delta);
        }
    }
}
