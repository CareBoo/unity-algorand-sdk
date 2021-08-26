using System;
using AlgoSdk.MsgPack;

namespace AlgoSdk
{
    public struct DryrunSource
        : IMessagePackObject
        , IEquatable<DryrunSource>
    {
        public bool Equals(DryrunSource other)
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
