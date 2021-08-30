using System;
using AlgoSdk.MsgPack;

namespace AlgoSdk
{
    public struct AssetHolding
        : IMessagePackObject
        , IEquatable<AssetHolding>
    {
        public ulong Amount;
        public ulong AssetId;
        public Address Creator;
        public bool IsFrozen;

        public bool Equals(AssetHolding other)
        {
            return this.Equals(ref other);
        }
    }
}

namespace AlgoSdk.MsgPack
{
    internal static partial class FieldMaps
    {
        internal static readonly Field<AssetHolding>.Map assetHoldingFields =
            new Field<AssetHolding>.Map()
                .Assign("amount", (ref AssetHolding x) => ref x.Amount)
                .Assign("asset-id", (ref AssetHolding x) => ref x.AssetId)
                .Assign("creator", (ref AssetHolding x) => ref x.Creator)
                .Assign("is-frozen", (ref AssetHolding x) => ref x.IsFrozen)
                ;
    }
}
