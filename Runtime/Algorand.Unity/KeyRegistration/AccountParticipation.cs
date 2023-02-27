using System;
using UnityEngine;

namespace Algorand.Unity
{
    /// <summary>
    /// Describes the parameters used by this account in consensus protocol.
    /// </summary>
    [Serializable]
    public partial struct AccountParticipation
        : IEquatable<AccountParticipation>
    {
        /// <summary>
        /// Root participation public key (if any) currently registered for this round.
        /// </summary>
        [Tooltip("Root participation public key (if any) currently registered for this round.")]
        public ParticipationPublicKey VoteParticipationKey;

        /// <summary>
        /// Selection public key (if any) currently registered for this round.
        /// </summary>
        [Tooltip("Selection public key (if any) currently registered for this round.")]
        public VrfPubKey SelectionParticipationKey;

        /// <summary>
        /// First round for which this participation is valid.
        /// </summary>
        [Tooltip("First round for which this participation is valid.")]
        public ulong VoteFirst;

        /// <summary>
        /// Last round for which this participation is valid.
        /// </summary>
        [Tooltip("Last round for which this participation is valid.")]
        public ulong VoteLast;

        /// <summary>
        /// Number of subkeys in each batch of participation keys.
        /// </summary>
        [Tooltip("Number of subkeys in each batch of participation keys.")]
        public ulong VoteKeyDilution;

        public bool Equals(AccountParticipation other)
        {
            return SelectionParticipationKey.Equals(other.SelectionParticipationKey);
        }
    }
}
