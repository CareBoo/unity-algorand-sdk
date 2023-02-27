using System;
using Unity.Collections;
using UnityEngine;

namespace Algorand.Unity
{
    /// <summary>
    /// Params used for setting all transactions. Contains fee, genesis info, and round constraints.
    /// </summary>
    [Serializable]
    public partial struct TransactionParams
        : IEquatable<TransactionParams>
    {
        /// <summary>
        /// Indicates the consensus protocol version as of <see cref="LastRound"/>.
        /// </summary>
        [Tooltip("Indicates the consensus protocol version as of LastRound.")]
        public string ConsensusVersion;

        /// <summary>
        /// Fee is the suggested transaction fee in units of micro-Algos per byte.
        /// Fee may fall to zero but transactions must still have a fee of at least
        /// <see cref="MinFee"/> for the current network protocol.
        /// </summary>
        [Tooltip("Fee is the suggested transaction fee in units of micro-Algos per byte. Fee may fall to zero but transactions must still have a fee of at least MinFee for the current network protocol.")]
        public MicroAlgos Fee;

        /// <summary>
        /// The hash of the genesis block.
        /// </summary>
        [Tooltip("The hash of the genesis block.")]
        public GenesisHash GenesisHash;

        /// <summary>
        /// An ID listed in the genesis block.
        /// </summary>
        [Tooltip("An ID listed in the genesis block.")]
        public FixedString32Bytes GenesisId;

        /// <summary>
        /// The minimum transaction fee (not per byte) required for the txn to validate for the current network protocol.
        /// </summary>
        [Tooltip("The minimum transaction fee (not per byte) required for the txn to validate for the current network protocol.")]
        public MicroAlgos MinFee;

        /// <summary>
        /// Indicates the last round seen by the node
        /// </summary>
        public ulong LastRound
        {
            get => prevRound;
            set
            {
                prevRound = value;
                FirstValidRound = value;
                LastValidRound = value + 1000;
            }
        }

        /// <summary>
        /// Whether to interpret <see cref="Fee"/> as microalgos per byte, or as a flat amount of microalgos.
        /// </summary>
        public bool FlatFee
        {
            get => !noFlatFee;
            set => noFlatFee = !value;
        }

        /// <summary>
        /// The last valid round for a transaction.
        /// </summary>
        [Tooltip("The last valid round for a transaction.")]
        public ulong FirstValidRound;

        /// <summary>
        /// The first valid round for a transaction.
        /// </summary>
        [Tooltip("The first valid round for a transaction.")]
        public ulong LastValidRound;

        private ulong prevRound;

        private bool noFlatFee;

        public bool Equals(TransactionParams other)
        {
            return ConsensusVersion.Equals(other.ConsensusVersion)
                && Fee.Equals(other.Fee)
                && GenesisHash.Equals(other.GenesisHash)
                && GenesisId.Equals(other.GenesisId)
                && MinFee.Equals(other.MinFee)
                && FirstValidRound.Equals(other.FirstValidRound)
                && LastValidRound.Equals(other.LastValidRound)
                && FlatFee.Equals(other.FlatFee)
                ;
        }
    }
}
