using System;
using AlgoSdk.MsgPack;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct AssetHolding
        : IMessagePackObject
        , IEquatable<AssetHolding>
    {
        [AlgoApiKey("amount")]
        public ulong Amount;
        [AlgoApiKey("asset-id")]
        public ulong AssetId;
        [AlgoApiKey("creator")]
        public Address Creator;
        [AlgoApiKey("is-frozen")]
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
