using System;
using AlgoSdk.MsgPack;

namespace AlgoSdk
{
    public struct Asset
        : IMessagePackObject
        , IEquatable<Asset>
    {
        public ulong Index;
        public AssetParams Params;

        public bool Equals(Asset other)
        {
            return this.Equals(ref other);
        }
    }
}

namespace AlgoSdk.MsgPack
{
    internal static partial class FieldMaps
    {
        internal static readonly Field<Asset>.Map assetFields =
            new Field<Asset>.Map()
                .Assign("index", (ref Asset x) => ref x.Index)
                .Assign("params", (ref Asset x) => ref x.Params)
                ;
    }
}
