using System;

namespace AlgoSdk
{
    public struct AccountParticipation : IEquatable<AccountParticipation>
    {
        public bool Equals(AccountParticipation other)
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
