using System;
using AlgoSdk.Crypto;
using Unity.Collections;
using UnityEngine;

namespace AlgoSdk
{
    /// <summary>
    /// AssetParams specifies the parameters for an asset.
    /// [apar] when part of an AssetConfig transaction.
    /// </summary>
    [AlgoApiObject]
    [Serializable]
    public struct AssetParams
        : IEquatable<AssetParams>
    {
        /// <summary>
        /// Address of account used to clawback holdings of this asset. If empty, clawback is not permitted.
        /// </summary>
        [AlgoApiField("clawback", "c")]
        [Tooltip("Address of account used to clawback holdings of this asset. If empty, clawback is not permitted.")]
        public Address Clawback;

        /// <summary>
        /// The address that created this asset. This is the address where the parameters for this asset can be found, and also the address where unwanted asset units can be sent in the worst case.
        /// </summary>
        [AlgoApiField("creator", null, readOnly: true)]
        [Tooltip("The address that created this asset. This is the address where the parameters for this asset can be found, and also the address where unwanted asset units can be sent in the worst case.")]
        public Address Creator;

        /// <summary>
        /// The number of digits to use after the decimal point when displaying this asset. If 0, the asset is not divisible. If 1, the base unit of the asset is in tenths. If 2, the base unit of the asset is in hundredths, and so on. This value must be between 0 and 19 (inclusive).
        /// Minimum value: 0. Maximum value: 19.
        /// </summary>
        [AlgoApiField("decimals", "dc")]
        [Tooltip("The number of digits to use after the decimal point when displaying this asset. If 0, the asset is not divisible. If 1, the base unit of the asset is in tenths. If 2, the base unit of the asset is in hundredths, and so on. This value must be between 0 and 19 (inclusive).")]
        public uint Decimals;

        /// <summary>
        /// Whether holdings of this asset are frozen by default.
        /// </summary>
        [AlgoApiField("default-frozen", "df")]
        [Tooltip("Whether holdings of this asset are frozen by default.")]
        public bool DefaultFrozen;

        /// <summary>
        /// Address of account used to freeze holdings of this asset. If empty, freezing is not permitted.
        /// </summary>
        [AlgoApiField("freeze", "f")]
        [Tooltip("Address of account used to freeze holdings of this asset. If empty, freezing is not permitted.")]
        public Address Freeze;

        /// <summary>
        /// Address of account used to manage the keys of this asset and to destroy it.
        /// </summary>
        [AlgoApiField("manager", "m")]
        [Tooltip("Address of account used to manage the keys of this asset and to destroy it.")]
        public Address Manager;

        /// <summary>
        /// A commitment to some unspecified asset metadata. The format of this metadata is up to the application.
        /// </summary>
        [AlgoApiField("metadata-hash", "am")]
        [Tooltip("A commitment to some unspecified asset metadata. The format of this metadata is up to the application.")]
        public Sha512_256_Hash MetadataHash;

        /// <summary>
        /// Name of this asset, as supplied by the creator. Included only when the asset name is composed of printable utf-8 characters.
        /// </summary>
        [AlgoApiField("name", "an")]
        [Tooltip("Name of this asset, as supplied by the creator. Included only when the asset name is composed of printable utf-8 characters.")]
        public FixedString64Bytes Name;

        /// <summary>
        /// Base64 encoded name of this asset, as supplied by the creator.
        /// </summary>
        [AlgoApiField("name-b64", null, readOnly: true)]
        public FixedString64Bytes NameBase64
        {
            get
            {
                FixedString64Bytes b64 = default;
                Name.Utf8ToBase64(ref b64);
                return b64;
            }
            set => value.Base64ToUtf8(ref Name);
        }

        /// <summary>
        /// Address of account holding reserve (non-minted) units of this asset.
        /// </summary>
        [AlgoApiField("reserve", "r")]
        [Tooltip("Address of account holding reserve (non-minted) units of this asset.")]
        public Address Reserve;

        /// <summary>
        /// The total number of units of this asset.
        /// </summary>
        [AlgoApiField("total", "t")]
        [Tooltip("The total number of units of this asset.")]
        public ulong Total;

        /// <summary>
        /// Name of a unit of this asset, as supplied by the creator. Included only when the name of a unit of this asset is composed of printable utf-8 characters.
        /// </summary>
        [AlgoApiField("unit-name", "un")]
        [Tooltip("Name of a unit of this asset, as supplied by the creator. Included only when the name of a unit of this asset is composed of printable utf-8 characters.")]
        public FixedString32Bytes UnitName;

        /// <summary>
        /// Base64 encoded name of a unit of this asset, as supplied by the creator.
        /// </summary>
        [AlgoApiField("unit-name-b64", null, readOnly: true)]
        public FixedString32Bytes UnitNameBase64
        {
            get
            {
                FixedString32Bytes b64 = default;
                UnitName.Utf8ToBase64(ref b64);
                return b64;
            }
            set => value.Base64ToUtf8(ref UnitName);
        }

        /// <summary>
        /// URL where more information about the asset can be retrieved. Included only when the URL is composed of printable utf-8 characters.
        /// </summary>
        [AlgoApiField("url", "au")]
        [Tooltip("URL where more information about the asset can be retrieved. Included only when the URL is composed of printable utf-8 characters.")]
        public FixedString128Bytes Url;

        /// <summary>
        /// Base64 encoded URL where more information about the asset can be retrieved.
        /// </summary>
        [AlgoApiField("url-b64", null, readOnly: true)]
        public FixedString128Bytes UrlBase64
        {
            get
            {
                FixedString64Bytes b64 = default;
                Url.Utf8ToBase64(ref b64);
                return b64;
            }
            set => value.Base64ToUtf8(ref Url);
        }

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
