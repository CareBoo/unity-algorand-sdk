using System;
using AlgoSdk.Crypto;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public partial struct AssetParams
        : IEquatable<AssetParams>
    {
        [AlgoApiField("clawback", "c")]
        public Address Clawback;

        [AlgoApiField("creator", null, readOnly: true)]
        public Address Creator;

        [AlgoApiField("decimals", "dc")]
        public uint Decimals;

        [AlgoApiField("default-frozen", "df")]
        public bool DefaultFrozen;

        [AlgoApiField("freeze", "f")]
        public Address Freeze;

        [AlgoApiField("manager", "m")]
        public Address Manager;

        [AlgoApiField("metadata-hash", "am")]
        public Sha512_256_Hash MetadataHash;

        [AlgoApiField("name", "an")]
        public FixedString64Bytes Name;

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

        [AlgoApiField("reserve", "r")]
        public Address Reserve;

        [AlgoApiField("total", "t")]
        public ulong Total;

        [AlgoApiField("unit-name", "un")]
        public FixedString32Bytes UnitName;

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

        [AlgoApiField("url", "au")]
        public FixedString64Bytes Url;

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
