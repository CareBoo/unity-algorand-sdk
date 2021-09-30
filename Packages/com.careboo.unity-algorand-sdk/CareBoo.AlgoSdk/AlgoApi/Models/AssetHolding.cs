using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct AssetHolding
        : IEquatable<AssetHolding>
    {
        [AlgoApiKey("amount")]
        public ulong Amount;

        [AlgoApiKey("asset-id")]
        public ulong AssetId;

        [AlgoApiKey("creator")]
        public Address Creator;

        [AlgoApiKey("deleted")]
        public Optional<bool> Deleted;

        [AlgoApiKey("is-frozen")]
        public bool IsFrozen;

        [AlgoApiKey("opted-in-at-round")]
        public Optional<ulong> OptedInAtRound;

        [AlgoApiKey("opted-out-at-round")]
        public Optional<ulong> OptedOutAtRound;

        public bool Equals(AssetHolding other)
        {
            return Amount.Equals(other.Amount)
                && AssetId.Equals(other.AssetId)
                ;
        }
    }
}
