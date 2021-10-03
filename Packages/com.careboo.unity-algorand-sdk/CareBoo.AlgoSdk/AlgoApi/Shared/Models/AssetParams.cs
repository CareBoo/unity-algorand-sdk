using System;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public partial struct AssetParams
        : IEquatable<AssetParams>
    {
        [AlgoApiField("clawback", null)]
        public Address Clawback;

        [AlgoApiField("creator", null)]
        public Address Creator;

        [AlgoApiField("decimals", null)]
        public ulong Decimals;

        [AlgoApiField("default-frozen", null)]
        public Optional<bool> DefaultFrozen;

        [AlgoApiField("freeze", null)]
        public Address Freeze;

        [AlgoApiField("manager", null)]
        public Address Manager;

        [AlgoApiField("metadata-hash", null)]
        public FixedString128Bytes MetadataHash;

        [AlgoApiField("name", null)]
        public FixedString64Bytes Name;

        [AlgoApiField("name-b64", null)]
        public FixedString128Bytes NameBase64;

        [AlgoApiField("reserve", null)]
        public Address Reserve;

        [AlgoApiField("total", null)]
        public ulong Total;

        [AlgoApiField("unit-name", null)]
        public FixedString64Bytes UnitName;

        [AlgoApiField("unit-name-b64", null)]
        public FixedString128Bytes UnitNameBase64;

        [AlgoApiField("url", null)]
        public FixedString512Bytes Url;

        [AlgoApiField("url-b64", null)]
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
