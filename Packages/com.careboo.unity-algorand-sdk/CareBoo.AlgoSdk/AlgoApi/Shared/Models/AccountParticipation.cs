using System;
using AlgoSdk.Crypto;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct AccountParticipation
        : IEquatable<AccountParticipation>
    {
        [AlgoApiField("vote-participation-key", "votekey")]
        public Ed25519.PublicKey VoteParticipationKey;

        [AlgoApiField("selection-participation-key", "selkey")]
        public VrfPubKey SelectionParticipationKey;

        [AlgoApiField("vote-first-valid", "votefst")]
        public ulong VoteFirst;

        [AlgoApiField("vote-last-valid", "votelst")]
        public ulong VoteLast;

        [AlgoApiField("vote-key-dilution", "votekd")]
        public ulong VoteKeyDilution;

        public AccountParticipation(
            Ed25519.PublicKey votePk,
            VrfPubKey selectionPk,
            ulong voteFirst,
            ulong voteLast,
            ulong voteKeyDilution
        )
        {
            VoteParticipationKey = votePk;
            SelectionParticipationKey = selectionPk;
            VoteFirst = voteFirst;
            VoteLast = voteLast;
            VoteKeyDilution = voteKeyDilution;
        }

        public bool Equals(AccountParticipation other)
        {
            return SelectionParticipationKey.Equals(other.SelectionParticipationKey);
        }
    }
}
