using System;
using UnityEngine;

namespace Algorand.Unity
{
    public interface IStateProofTrackingData
    {
        /// <summary>
        /// StateProofVotersCommitment is the root of a vector commitment containing the
        /// online accounts that will help sign a state proof.  The
        /// VC root, and the state proof, happen on blocks that
        /// are a multiple of ConsensusParams.StateProofRounds.  For blocks
        /// that are not a multiple of ConsensusParams.StateProofRounds,
        /// this value is zero.
        /// </summary>
        byte[] StateProofVotersCommitment { get; set; }

        /// <summary>
        /// StateProofOnlineTotalWeight is the total number of microalgos held by the online accounts
        /// during the StateProof round (or zero, if the merkle root is zero - no commitment for StateProof voters).
        /// This is intended for computing the threshold of votes to expect from StateProofVotersCommitment.
        /// </summary>
        MicroAlgos StateProofOnlineTotalWeight { get; set; }

        /// <summary>
        /// StateProofNextRound is the next round for which we will accept
        /// a StateProof transaction.
        /// </summary>
        ulong StateProofNextRound { get; set; }
    }

    /// <summary>
    /// StateProofTrackingData tracks the status of state proofs.
    /// </summary>
    [AlgoApiObject]
    [Serializable]
    public partial struct StateProofTrackingData
        : IStateProofTrackingData
            , IEquatable<StateProofTrackingData>
    {
        [SerializeField]
        [Tooltip("StateProofVotersCommitment is the root of a vector commitment containing the " +
                 "online accounts that will help sign a state proof. The " +
                 "VC root, and the state proof, happen on blocks that " +
                 "are a multiple of ConsensusParams.StateProofRounds.  For blocks " +
                 "that are not a multiple of ConsensusParams.StateProofRounds, " +
                 "this value is zero.")]
        private byte[] stateProofVotersCommitment;

        [SerializeField]
        [Tooltip("StateProofOnlineTotalWeight is the total number of microalgos held by the online accounts " +
                 "during the StateProof round (or zero, if the merkle root is zero - no commitment for StateProof voters). " +
                 "This is intended for computing the threshold of votes to expect from StateProofVotersCommitment.")]
        private MicroAlgos stateProofOnlineTotalWeight;

        [SerializeField]
        [Tooltip("StateProofNextRound is the next round for which we will accept a StateProof transaction.")]
        private ulong stateProofNextRound;

        /// <inehritdoc />
        [AlgoApiField("v")]
        public byte[] StateProofVotersCommitment
        {
            get => stateProofVotersCommitment;
            set => stateProofVotersCommitment = value;
        }

        /// <inehritdoc />
        [AlgoApiField("t")]
        public MicroAlgos StateProofOnlineTotalWeight
        {
            get => stateProofOnlineTotalWeight;
            set => stateProofOnlineTotalWeight = value;
        }

        /// <inehritdoc />
        [AlgoApiField("n")]
        public ulong StateProofNextRound
        {
            get => stateProofNextRound;
            set => stateProofNextRound = value;
        }

        public bool Equals(StateProofTrackingData other)
        {
            return ArrayComparer.Equals(StateProofVotersCommitment, other.StateProofVotersCommitment)
                   && StateProofOnlineTotalWeight.Equals(other.StateProofOnlineTotalWeight)
                   && StateProofNextRound.Equals(other.StateProofNextRound)
                ;
        }
    }
}
