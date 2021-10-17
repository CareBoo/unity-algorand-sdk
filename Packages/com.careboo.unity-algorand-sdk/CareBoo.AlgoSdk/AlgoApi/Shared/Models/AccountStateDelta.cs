using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct AccountStateDelta
        : IEquatable<AccountStateDelta>
    {
        [AlgoApiField("address", null)]
        public Address Address;

        [AlgoApiField("delta", null)]
        public EvalDeltaKeyValue[] Delta;

        public bool Equals(AccountStateDelta other)
        {
            return Address.Equals(other.Address)
                && ArrayComparer.Equals(Delta, other.Delta);
        }
    }
}
