using System;
using UnityEngine;

namespace AlgoSdk
{
    /// <summary>
    /// Specifies both the unique identifier and the parameters for an asset.
    /// </summary>
    [AlgoApiObject]
    [Serializable]
    public struct Asset
        : IEquatable<Asset>
    {
        /// <summary>
        /// Round during which this asset was created.
        /// </summary>
        [AlgoApiField("created-at-round", null)]
        [Tooltip("Round during which this asset was created.")]
        public ulong CreatedAtRound;

        /// <summary>
        /// Whether or not this asset is currently deleted.
        /// </summary>
        [AlgoApiField("deleted", null)]
        [Tooltip("Whether or not this asset is currently deleted.")]
        public Optional<bool> Deleted;

        /// <summary>
        /// Round during which this asset was destroyed.
        /// </summary>
        [AlgoApiField("destroyed-at-round", null)]
        [Tooltip("Round during which this asset was destroyed.")]
        public ulong DestroyedAtRound;

        /// <summary>
        /// unique asset identifier
        /// </summary>
        [AlgoApiField("index", null)]
        [Tooltip("unique asset identifier")]
        public ulong Index;

        [AlgoApiField("params", null)]
        public AssetParams Params;

        public bool Equals(Asset other)
        {
            return Index.Equals(other.Index)
                && Params.Equals(other.Params)
                ;
        }
    }
}
