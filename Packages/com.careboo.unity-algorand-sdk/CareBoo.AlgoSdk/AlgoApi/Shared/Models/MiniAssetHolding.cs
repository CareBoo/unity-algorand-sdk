using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct MiniAssetHolding
        : IEquatable<MiniAssetHolding>
    {
        [AlgoApiField("address", null)]
        public Address Address;

        [AlgoApiField("amount", null)]
        public ulong Amount;

        [AlgoApiField("deleted", null)]
        public Optional<bool> Deleted;

        [AlgoApiField("is-frozen", null)]
        public Optional<bool> IsFrozen;

        [AlgoApiField("opted-in-at-round", null)]
        public ulong OptedInAtRound;

        [AlgoApiField("opted-out-at-round", null)]
        public ulong OptedOutAtRound;

        public bool Equals(MiniAssetHolding other)
        {
            return Address.Equals(other.Address)
                && Amount.Equals(other.Amount)
                && Deleted.Equals(other.Deleted)
                && IsFrozen.Equals(other.IsFrozen)
                && OptedInAtRound.Equals(other.OptedInAtRound)
                && OptedOutAtRound.Equals(other.OptedOutAtRound)
                ;
        }
    }
}
