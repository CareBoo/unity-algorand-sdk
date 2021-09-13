using System;
using AlgoSdk.MsgPack;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public partial struct AssetParams
        : IEquatable<AssetParams>
        , IMessagePackObject
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
            return this.Equals(ref other);
        }
    }
}

namespace AlgoSdk.MsgPack
{
    internal static partial class FieldMaps
    {
        internal static readonly Field<AssetParams>.Map assetParamsFields =
            new Field<AssetParams>.Map()
                .Assign("clawback", (ref AssetParams x) => ref x.Clawback)
                .Assign("creator", (ref AssetParams x) => ref x.Creator)
                .Assign("decimals", (ref AssetParams x) => ref x.Decimals)
                .Assign("default-frozen", (ref AssetParams x) => ref x.DefaultFrozen)
                .Assign("freeze", (ref AssetParams x) => ref x.Freeze)
                .Assign("manager", (ref AssetParams x) => ref x.Manager)
                .Assign("metadata-hash", (ref AssetParams x) => ref x.MetadataHash)
                .Assign("name", (ref AssetParams x) => ref x.Name)
                .Assign("reserve", (ref AssetParams x) => ref x.Reserve)
                .Assign("total", (ref AssetParams x) => ref x.Total)
                .Assign("unit-name", (ref AssetParams x) => ref x.UnitName)
                .Assign("url", (ref AssetParams x) => ref x.Url)
                ;
    }
}
