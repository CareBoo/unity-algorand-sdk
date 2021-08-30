using System;
using AlgoSdk.MsgPack;
using Unity.Collections;

namespace AlgoSdk
{
    public struct AccountParticipation
        : IEquatable<AccountParticipation>
        , IMessagePackObject
    {
        public FixedString128Bytes SelectionParticipationKey;
        public ulong VoteFirstValid;
        public ulong VoteKeyDilution;
        public ulong VoteLastValid;
        public FixedString128Bytes VoteParticipationKey;

        public bool Equals(AccountParticipation other)
        {
            return this.Equals(ref other);
        }
    }
}

namespace AlgoSdk.MsgPack
{
    internal static partial class FieldMaps
    {
        internal static readonly Field<AccountParticipation>.Map accountParticipationFields =
            new Field<AccountParticipation>.Map()
                .Assign("selection-participation-key", (ref AccountParticipation x) => ref x.SelectionParticipationKey)
                .Assign("vote-first-valid", (ref AccountParticipation x) => ref x.VoteFirstValid)
                .Assign("vote-key-dilution", (ref AccountParticipation x) => ref x.VoteKeyDilution)
                .Assign("vote-last-valid", (ref AccountParticipation x) => ref x.VoteLastValid)
                .Assign("vote-participation-key", (ref AccountParticipation x) => ref x.VoteParticipationKey)
                ;
    }
}
