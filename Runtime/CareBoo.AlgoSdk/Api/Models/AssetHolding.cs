using System;
using UnityEngine;

namespace AlgoSdk
{
    /// <summary>
    /// Describes an asset held by an account.
    /// </summary>
    [AlgoApiObject]
    [Serializable]
    public partial struct AssetHolding
        : IEquatable<AssetHolding>
    {
        /// <summary>
        /// number of units held.
        /// </summary>
        [AlgoApiField("amount")]
        [Tooltip("number of units held.")]
        public ulong Amount;

        /// <summary>
        /// Asset ID of the holding.
        /// </summary>
        [AlgoApiField("asset-id")]
        [Tooltip("Asset ID of the holding.")]
        public ulong AssetId;

        /// <summary>
        /// Address that created this asset. This is the address where the parameters for this asset can be found, and also the address where unwanted asset units can be sent in the worst case.
        /// </summary>
        [AlgoApiField("creator")]
        [Tooltip("Address that created this asset. This is the address where the parameters for this asset can be found, and also the address where unwanted asset units can be sent in the worst case.")]
        public Address Creator;

        /// <summary>
        /// Whether or not the asset holding is currently deleted from its account.
        /// </summary>
        [AlgoApiField("deleted")]
        [Tooltip("Whether or not the asset holding is currently deleted from its account.")]
        public Optional<bool> Deleted;

        /// <summary>
        /// whether or not the holding is frozen.
        /// </summary>
        [AlgoApiField("is-frozen")]
        [Tooltip("whether or not the holding is frozen.")]
        public bool IsFrozen;

        /// <summary>
        /// Round during which the account opted into this asset holding.
        /// </summary>
        [AlgoApiField("opted-in-at-round")]
        [Tooltip("Round during which the account opted into this asset holding.")]
        public ulong OptedInAtRound;

        /// <summary>
        /// Round during which the account opted out of this asset holding.
        /// </summary>
        [AlgoApiField("opted-out-at-round")]
        [Tooltip("Round during which the account opted out of this asset holding.")]
        public ulong OptedOutAtRound;

        public bool Equals(AssetHolding other)
        {
            return Amount.Equals(other.Amount)
                && AssetId.Equals(other.AssetId)
                ;
        }
    }
}
