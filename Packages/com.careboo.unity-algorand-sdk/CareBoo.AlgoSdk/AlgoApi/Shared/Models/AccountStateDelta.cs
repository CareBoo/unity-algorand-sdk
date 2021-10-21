using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct ApplicationStateDelta
        : IEquatable<ApplicationStateDelta>
    {
        [AlgoApiField("address", null)]
        public Address Address;

        [AlgoApiField("delta", null)]
        public EvalDeltaKeyValue[] Delta;

        public bool Equals(ApplicationStateDelta other)
        {
            return Address.Equals(other.Address)
                && ArrayComparer.Equals(Delta, other.Delta);
        }
    }
}
