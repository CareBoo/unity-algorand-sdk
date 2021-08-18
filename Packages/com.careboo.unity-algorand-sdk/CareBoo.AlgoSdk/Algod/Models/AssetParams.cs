using System;
using AlgoSdk.MsgPack;

namespace AlgoSdk
{
    public partial struct AssetParams : IEquatable<AssetParams>, IMessagePackObject
    {
        public bool Equals(AssetParams other)
        {
            return true;
        }
    }
}

namespace AlgoSdk.MsgPack
{
    internal static partial class FieldMaps
    {
    }
}
