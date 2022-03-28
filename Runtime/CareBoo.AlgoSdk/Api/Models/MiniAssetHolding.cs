using System;
using UnityEngine;

namespace AlgoSdk
{
    /// <summary>
    /// A simplified version of AssetHolding
    /// </summary>
    [AlgoApiObject]
    [Serializable]
    public partial struct MiniAssetHolding
        : IEquatable<MiniAssetHolding>
    {
        [AlgoApiField("address")]
        public Address Address;

        [AlgoApiField("amount")]
        public ulong Amount;

        /// <summary>
        /// Whether or not this asset holding is currently deleted from its account.
        /// </summary>
        [AlgoApiField("deleted")]
        [Tooltip("Whether or not this asset holding is currently deleted from its account.")]
        public Optional<bool> Deleted;

        [AlgoApiField("is-frozen")]
        public Optional<bool> IsFrozen;

        /// <summary>
        /// Round during which the account opted into the asset.
        /// </summary>
        [AlgoApiField("opted-in-at-round")]
        [Tooltip("Round during which the account opted into the asset.")]
        public ulong OptedInAtRound;

        /// <summary>
        /// Round during which the account opted out of the asset.
        /// </summary>
        [AlgoApiField("opted-out-at-round")]
        [Tooltip("Round during which the account opted out of the asset.")]
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
