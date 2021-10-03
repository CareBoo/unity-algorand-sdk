using System;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct AccountParticipation
        : IEquatable<AccountParticipation>
    {
        [AlgoApiField("selection-participation-key", null)]
        public FixedString128Bytes SelectionParticipationKey;

        [AlgoApiField("vote-first-valid", null)]
        public ulong VoteFirstValid;

        [AlgoApiField("vote-key-dilution", null)]
        public ulong VoteKeyDilution;

        [AlgoApiField("vote-last-valid", null)]
        public ulong VoteLastValid;

        [AlgoApiField("vote-participation-key", null)]
        public FixedString128Bytes VoteParticipationKey;

        public bool Equals(AccountParticipation other)
        {
            return SelectionParticipationKey.Equals(other.SelectionParticipationKey);
        }
    }
}
