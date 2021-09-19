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
        [AlgoApiKey("is-frozen")]
        public bool IsFrozen;

        public bool Equals(AssetHolding other)
        {
            return Amount.Equals(other.Amount)
                && AssetId.Equals(other.AssetId)
                ;
        }
    }
}
