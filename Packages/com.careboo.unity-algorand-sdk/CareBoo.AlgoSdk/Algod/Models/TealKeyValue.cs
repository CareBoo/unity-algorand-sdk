using System;

namespace AlgoSdk
{
    public struct TealKeyValue
        : IEquatable<TealKeyValue>
    {
        public bool Equals(TealKeyValue other)
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
