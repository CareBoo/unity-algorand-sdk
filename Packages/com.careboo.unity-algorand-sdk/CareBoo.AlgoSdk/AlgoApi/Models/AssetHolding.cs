using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct AssetHolding
        : IEquatable<AssetHolding>
    {
        [AlgoApiKey("amount", null)]
        public ulong Amount;

        [AlgoApiKey("asset-id", null)]
        public ulong AssetId;

        [AlgoApiKey("creator", null)]
        public Address Creator;

        [AlgoApiKey("deleted", null)]
        public Optional<bool> Deleted;

        [AlgoApiKey("is-frozen", null)]
        public bool IsFrozen;

        [AlgoApiKey("opted-in-at-round", null)]
        public Optional<ulong> OptedInAtRound;

        [AlgoApiKey("opted-out-at-round", null)]
        public Optional<ulong> OptedOutAtRound;

        public bool Equals(AssetHolding other)
        {
            return Amount.Equals(other.Amount)
                && AssetId.Equals(other.AssetId)
                ;
        }
    }
}
