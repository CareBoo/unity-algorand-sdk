using System;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public partial struct AssetParams
        : IEquatable<AssetParams>
    {
        [AlgoApiKey("clawback")]
        public Address Clawback;

        [AlgoApiKey("creator")]
        public Address Creator;

        [AlgoApiKey("decimals")]
        public ulong Decimals;

        [AlgoApiKey("default-frozen")]
        public Optional<bool> DefaultFrozen;

        [AlgoApiKey("freeze")]
        public Address Freeze;

        [AlgoApiKey("manager")]
        public Address Manager;

        [AlgoApiKey("metadata-hash")]
        public FixedString128Bytes MetadataHash;

        [AlgoApiKey("name")]
        public FixedString64Bytes Name;

        [AlgoApiKey("name-b64")]
        public FixedString128Bytes NameBase64;

        [AlgoApiKey("reserve")]
        public Address Reserve;

        [AlgoApiKey("total")]
        public ulong Total;

        [AlgoApiKey("unit-name")]
        public FixedString64Bytes UnitName;

        [AlgoApiKey("unit-name-b64")]
        public FixedString128Bytes UnitNameBase64;

        [AlgoApiKey("url")]
        public FixedString512Bytes Url;

        [AlgoApiKey("url-b64")]
        public FixedString512Bytes UrlBase64;

        public bool Equals(AssetParams other)
        {
            return Clawback.Equals(other.Clawback)
                && Creator.Equals(other.Creator)
                && Decimals.Equals(other.Decimals)
                && DefaultFrozen.Equals(other.DefaultFrozen)
                && Freeze.Equals(other.Freeze)
                && Manager.Equals(other.Manager)
                && MetadataHash.Equals(other.MetadataHash)
                && Name.Equals(other.Name)
                && NameBase64.Equals(other.NameBase64)
                && Reserve.Equals(other.Reserve)
                && Total.Equals(other.Total)
                && UnitName.Equals(other.UnitName)
                && UnitNameBase64.Equals(other.UnitNameBase64)
                && Url.Equals(other.Url)
                && UrlBase64.Equals(other.UrlBase64)
                ;
        }
    }
}
