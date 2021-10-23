using System;

namespace AlgoSdk
{
    /// <summary>
    /// Describes an asset held by an account.
    /// </summary>
    [AlgoApiObject]
    public struct AssetHolding
        : IEquatable<AssetHolding>
    {
        /// <summary>
        /// [a] number of units held.
        /// </summary>
        [AlgoApiField("amount", null)]
        public ulong Amount;

        /// <summary>
        /// Asset ID of the holding.
        /// </summary>
        [AlgoApiField("asset-id", null)]
        public ulong AssetId;

        /// <summary>
        /// Address that created this asset. This is the address where the parameters for this asset can be found, and also the address where unwanted asset units can be sent in the worst case.
        /// </summary>
        [AlgoApiField("creator", null)]
        public Address Creator;

        /// <summary>
        /// Whether or not the asset holding is currently deleted from its account.
        /// </summary>
        [AlgoApiField("deleted", null)]
        public Optional<bool> Deleted;

        /// <summary>
        /// [f] whether or not the holding is frozen.
        /// </summary>
        [AlgoApiField("is-frozen", null)]
        public bool IsFrozen;

        /// <summary>
        /// Round during which the account opted into this asset holding.
        /// </summary>
        [AlgoApiField("opted-in-at-round", null)]
        public ulong OptedInAtRound;

        /// <summary>
        /// Round during which the account opted out of this asset holding.
        /// </summary>
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
