using System;
using AlgoSdk.MsgPack;
using Unity.Collections;

namespace AlgoSdk
{
    public partial struct AssetParams
        : IEquatable<AssetParams>
        , IMessagePackObject
    {
        public Address Clawback;
        public Address Creator;
        public ulong Decimals;
        public Optional<bool> DefaultFrozen;
        public Address Freeze;
        public Address Manager;
        public FixedString128Bytes MetadataHash;
        public FixedString64Bytes Name;
        public Address Reserve;
        public ulong Total;
        public FixedString64Bytes UnitName;
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
