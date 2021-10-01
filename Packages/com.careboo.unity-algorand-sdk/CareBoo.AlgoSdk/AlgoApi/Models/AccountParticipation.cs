using System;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct AccountParticipation
        : IEquatable<AccountParticipation>
    {
        [AlgoApiKey("selection-participation-key", null)]
        public FixedString128Bytes SelectionParticipationKey;

        [AlgoApiKey("vote-first-valid", null)]
        public ulong VoteFirstValid;

        [AlgoApiKey("vote-key-dilution", null)]
        public ulong VoteKeyDilution;

        [AlgoApiKey("vote-last-valid", null)]
        public ulong VoteLastValid;

        [AlgoApiKey("vote-participation-key", null)]
        public FixedString128Bytes VoteParticipationKey;

        public bool Equals(AccountParticipation other)
        {
            return SelectionParticipationKey.Equals(other.SelectionParticipationKey);
        }
    }
}
