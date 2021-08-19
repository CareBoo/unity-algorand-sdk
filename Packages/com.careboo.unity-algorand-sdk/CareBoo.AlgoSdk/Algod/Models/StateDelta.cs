using System;
using AlgoSdk.MsgPack;

namespace AlgoSdk
{
    public struct StateDelta
        : IMessagePackObject
        , IEquatable<StateDelta>
    {
        public bool Equals(StateDelta other)
        {
            return this.Equals(ref other);
        }
    }
}

namespace AlgoSdk.MsgPack
{
    internal static partial class FieldMaps
    {
    }
}
