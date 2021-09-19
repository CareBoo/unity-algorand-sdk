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

        [AlgoApiKey("reserve")]
        public Address Reserve;

        [AlgoApiKey("total")]
        public ulong Total;

        [AlgoApiKey("unit-name")]
        public FixedString64Bytes UnitName;

        [AlgoApiKey("url")]
        public FixedString512Bytes Url;

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
                && Reserve.Equals(other.Reserve)
                && Total.Equals(other.Total)
                && UnitName.Equals(other.UnitName)
                && Url.Equals(other.Url)
                ;
        }
    }
}
