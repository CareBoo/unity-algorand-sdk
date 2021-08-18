using System;

namespace AlgoSdk
{
    public struct ApplicationStateSchema : IEquatable<ApplicationStateSchema>
    {
        public bool Equals(ApplicationStateSchema other)
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
