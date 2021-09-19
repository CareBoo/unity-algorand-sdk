using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct AccountStateDelta
        : IEquatable<AccountStateDelta>
    {
        [AlgoApiKey("address")]
        public Address Address;

        [AlgoApiKey("delta")]
        public EvalDeltaKeyValue[] Delta;

        public bool Equals(AccountStateDelta other)
        {
            return Address.Equals(other.Address)
                && ArrayComparer.Equals(Delta, other.Delta);
        }
    }
}
