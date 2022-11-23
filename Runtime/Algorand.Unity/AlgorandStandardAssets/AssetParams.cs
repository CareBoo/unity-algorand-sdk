using System;
using Algorand.Unity.Crypto;
using Unity.Collections;
using UnityEngine;

namespace Algorand.Unity
{
    /// <summary>
    /// AssetParams specifies the parameters for an asset.
    /// [apar] when part of an AssetConfig transaction.
    /// </summary>
    [AlgoApiObject]
    [Serializable]
    public partial struct AssetParams
        : IEquatable<AssetParams>
    {
        /// <summary>
        /// Address of account used to clawback holdings of this asset. If empty, clawback is not permitted.
        /// </summary>
        [AlgoApiField("c")]
        [Tooltip("Address of account used to clawback holdings of this asset. If empty, clawback is not permitted.")]
        public Address Clawback;

        /// <summary>
        /// The number of digits to use after the decimal point when displaying this asset. If 0, the asset is not divisible. If 1, the base unit of the asset is in tenths. If 2, the base unit of the asset is in hundredths, and so on. This value must be between 0 and 19 (inclusive).
        /// Minimum value: 0. Maximum value: 19.
        /// </summary>
        [AlgoApiField("dc")]
        [Tooltip("The number of digits to use after the decimal point when displaying this asset. If 0, the asset is not divisible. If 1, the base unit of the asset is in tenths. If 2, the base unit of the asset is in hundredths, and so on. This value must be between 0 and 19 (inclusive).")]
        public uint Decimals;

        /// <summary>
        /// Whether holdings of this asset are frozen by default.
        /// </summary>
        [AlgoApiField("df")]
        [Tooltip("Whether holdings of this asset are frozen by default.")]
        public bool DefaultFrozen;

        /// <summary>
        /// Address of account used to freeze holdings of this asset. If empty, freezing is not permitted.
        /// </summary>
        [AlgoApiField("f")]
        [Tooltip("Address of account used to freeze holdings of this asset. If empty, freezing is not permitted.")]
        public Address Freeze;

        /// <summary>
        /// Address of account used to manage the keys of this asset and to destroy it.
        /// </summary>
        [AlgoApiField("m")]
        [Tooltip("Address of account used to manage the keys of this asset and to destroy it.")]
        public Address Manager;

        /// <summary>
        /// A commitment to some unspecified asset metadata. The format of this metadata is up to the application.
        /// </summary>
        [AlgoApiField("am")]
        [Tooltip("A commitment to some unspecified asset metadata. The format of this metadata is up to the application.")]
        public Sha512_256_Hash MetadataHash;

        /// <summary>
        /// Name of this asset, as supplied by the creator. Included only when the asset name is composed of printable utf-8 characters.
        /// </summary>
        [AlgoApiField("an")]
        [Tooltip("Name of this asset, as supplied by the creator. Included only when the asset name is composed of printable utf-8 characters.")]
        public FixedString64Bytes Name;

        /// <summary>
        /// Address of account holding reserve (non-minted) units of this asset.
        /// </summary>
        [AlgoApiField("r")]
        [Tooltip("Address of account holding reserve (non-minted) units of this asset.")]
        public Address Reserve;

        /// <summary>
        /// The total number of units of this asset.
        /// </summary>
        [AlgoApiField("t")]
        [Tooltip("The total number of units of this asset.")]
        public ulong Total;

        /// <summary>
        /// Name of a unit of this asset, as supplied by the creator. Included only when the name of a unit of this asset is composed of printable utf-8 characters.
        /// </summary>
        [AlgoApiField("un")]
        [Tooltip("Name of a unit of this asset, as supplied by the creator. Included only when the name of a unit of this asset is composed of printable utf-8 characters.")]
        public FixedString32Bytes UnitName;

        /// <summary>
        /// URL where more information about the asset can be retrieved. Included only when the URL is composed of printable utf-8 characters.
        /// </summary>
        [AlgoApiField("au")]
        [Tooltip("URL where more information about the asset can be retrieved. Included only when the URL is composed of printable utf-8 characters.")]
        public FixedString128Bytes Url;


        public bool Equals(AssetParams other)
        {
            return Clawback.Equals(other.Clawback)
                && Decimals.Equals(other.Decimals)
                && DefaultFrozen.Equals(other.DefaultFrozen)
                && Freeze.Equals(other.Freeze)
                && Manager.Equals(other.Manager)
                && MetadataHash.Equals(other.MetadataHash)
                && Name.Equals(other.Name)
                && Reserve.Equals(other.Reserve)
                && Total.Equals(other.Total)
                && UnitName.Equals(other.UnitName)
                && Url.Equals(other.Url)
                ;
        }
    }
}
