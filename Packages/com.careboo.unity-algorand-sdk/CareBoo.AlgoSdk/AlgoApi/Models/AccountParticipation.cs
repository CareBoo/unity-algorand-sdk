using System;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct AccountParticipation
        : IEquatable<AccountParticipation>
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
            return SelectionParticipationKey.Equals(other.SelectionParticipationKey);
        }
    }
}
