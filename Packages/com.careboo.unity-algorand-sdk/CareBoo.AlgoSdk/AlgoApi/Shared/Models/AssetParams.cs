using System;
using AlgoSdk.Crypto;
using Unity.Collections;

namespace AlgoSdk
{
    /// <summary>
    /// AssetParams specifies the parameters for an asset.
    /// [apar] when part of an AssetConfig transaction.
    /// </summary>
    [AlgoApiObject]
    public struct AssetParams
        : IEquatable<AssetParams>
    {
        /// <summary>
        /// [c] Address of account used to clawback holdings of this asset. If empty, clawback is not permitted.
        /// </summary>
        [AlgoApiField("clawback", "c")]
        public Address Clawback;

        /// <summary>
        /// The address that created this asset. This is the address where the parameters for this asset can be found, and also the address where unwanted asset units can be sent in the worst case.
        /// </summary>
        [AlgoApiField("creator", null, readOnly: true)]
        public Address Creator;

        /// <summary>
        /// [dc] The number of digits to use after the decimal point when displaying this asset. If 0, the asset is not divisible. If 1, the base unit of the asset is in tenths. If 2, the base unit of the asset is in hundredths, and so on. This value must be between 0 and 19 (inclusive).
        /// Minimum value: 0. Maximum value: 19.
        /// </summary>
        [AlgoApiField("decimals", "dc")]
        public uint Decimals;

        /// <summary>
        /// [df] Whether holdings of this asset are frozen by default.
        /// </summary>
        [AlgoApiField("default-frozen", "df")]
        public bool DefaultFrozen;

        /// <summary>
        /// [f] Address of account used to freeze holdings of this asset. If empty, freezing is not permitted.
        /// </summary>
        [AlgoApiField("freeze", "f")]
        public Address Freeze;

        /// <summary>
        /// [m] Address of account used to manage the keys of this asset and to destroy it.
        /// </summary>
        [AlgoApiField("manager", "m")]
        public Address Manager;

        /// <summary>
        /// [am] A commitment to some unspecified asset metadata. The format of this metadata is up to the application.
        /// </summary>
        [AlgoApiField("metadata-hash", "am")]
        public Sha512_256_Hash MetadataHash;

        /// <summary>
        /// [an] Name of this asset, as supplied by the creator. Included only when the asset name is composed of printable utf-8 characters.
        /// </summary>
        [AlgoApiField("name", "an")]
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
        /// [r] Address of account holding reserve (non-minted) units of this asset.
        /// </summary>
        [AlgoApiField("reserve", "r")]
        public Address Reserve;

        /// <summary>
        /// [t] The total number of units of this asset.
        /// </summary>
        [AlgoApiField("total", "t")]
        public ulong Total;

        /// <summary>
        /// [un] Name of a unit of this asset, as supplied by the creator. Included only when the name of a unit of this asset is composed of printable utf-8 characters.
        /// </summary>
        [AlgoApiField("unit-name", "un")]
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
        /// [au] URL where more information about the asset can be retrieved. Included only when the URL is composed of printable utf-8 characters.
        /// </summary>
        [AlgoApiField("url", "au")]
        public FixedString64Bytes Url;

        /// <summary>
        /// Base64 encoded URL where more information about the asset can be retrieved.
        /// </summary>
        [AlgoApiField("url-b64", null, readOnly: true)]
        public FixedString64Bytes UrlBase64
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
