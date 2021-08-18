using System;

namespace AlgoSdk
{
    public struct DryrunRequest : IEquatable<DryrunRequest>
    {
        public bool Equals(DryrunRequest other)
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
