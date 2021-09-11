using System;
using AlgoSdk.MsgPack;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct AccountParticipation
        : IEquatable<AccountParticipation>
        , IMessagePackObject
    {
        [AlgoApiKey("selection-participation-key")]
        public FixedString128Bytes SelectionParticipationKey;

        [AlgoApiKey("vote-first-valid")]
        public ulong VoteFirstValid;

        [AlgoApiKey("vote-key-dilution")]
        public ulong VoteKeyDilution;

        [AlgoApiKey("vote-last-valid")]
        public ulong VoteLastValid;

        [AlgoApiKey("vote-participation-key")]
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
