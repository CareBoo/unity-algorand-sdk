using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct AssetHolding
        : IEquatable<AssetHolding>
    {
        [AlgoApiField("amount", null)]
        public ulong Amount;

        [AlgoApiField("asset-id", null)]
        public ulong AssetId;

        [AlgoApiField("creator", null)]
        public Address Creator;

        [AlgoApiField("deleted", null)]
        public Optional<bool> Deleted;

        [AlgoApiField("is-frozen", null)]
        public bool IsFrozen;

        [AlgoApiField("opted-in-at-round", null)]
        public ulong OptedInAtRound;

        [AlgoApiField("opted-out-at-round", null)]
        public ulong OptedOutAtRound;

        public bool Equals(AssetHolding other)
        {
            return Amount.Equals(other.Amount)
                && AssetId.Equals(other.AssetId)
                ;
        }
    }
}
