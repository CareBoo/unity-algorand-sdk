using System;
using AlgoSdk.Crypto;
using UnityEngine;

namespace AlgoSdk
{
    /// <summary>
    /// Describes the parameters used by this account in consensus protocol.
    /// </summary>
    [AlgoApiObject]
    [Serializable]
    public partial struct AccountParticipation
        : IEquatable<AccountParticipation>
    {
        /// <summary>
        /// Root participation public key (if any) currently registered for this round.
        /// </summary>
        [AlgoApiField("vote-participation-key", "votekey")]
        [Tooltip("Root participation public key (if any) currently registered for this round.")]
        public Ed25519.PublicKey VoteParticipationKey;

        /// <summary>
        /// Selection public key (if any) currently registered for this round.
        /// </summary>
        [AlgoApiField("selection-participation-key", "selkey")]
        [Tooltip("Selection public key (if any) currently registered for this round.")]
        public VrfPubKey SelectionParticipationKey;

        /// <summary>
        /// First round for which this participation is valid.
        /// </summary>
        [AlgoApiField("vote-first-valid", "votefst")]
        [Tooltip("First round for which this participation is valid.")]
        public ulong VoteFirst;

        /// <summary>
        /// Last round for which this participation is valid.
        /// </summary>
        [AlgoApiField("vote-last-valid", "votelst")]
        [Tooltip("Last round for which this participation is valid.")]
        public ulong VoteLast;

        /// <summary>
        /// Number of subkeys in each batch of participation keys.
        /// </summary>
        [AlgoApiField("vote-key-dilution", "votekd")]
        [Tooltip("Number of subkeys in each batch of participation keys.")]
        public ulong VoteKeyDilution;

        public bool Equals(AccountParticipation other)
        {
            return SelectionParticipationKey.Equals(other.SelectionParticipationKey);
        }
    }
}
