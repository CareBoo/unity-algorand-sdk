using System;
using Algorand.Unity.Crypto;
using Unity.Collections;
using UnityEngine;

namespace Algorand.Unity
{
    /// <summary>
    /// Block information.
    /// </summary>
    [AlgoApiObject]
    [Serializable]
    public partial struct BlockHeader
        : IEquatable<BlockHeader>
        , IBlockRewards
        , IBlockUpgradeState
        , IBlockUpgradeVote
        , ITxnCommitments
    {
        [SerializeField]
        private TxnCommitments txnCommitments;

        /// <summary>
        /// [gh] hash to which this block belongs.
        /// </summary>
        [AlgoApiField("gh")]
        [Tooltip("hash to which this block belongs.")]
        public GenesisHash GenesisHash;

        /// <summary>
        /// [gen] ID to which this block belongs.
        /// </summary>
        [AlgoApiField("gen")]
        [Tooltip("ID to which this block belongs.")]
        public FixedString64Bytes GenesisId;

        /// <summary>
        /// [prev] Previous block hash.
        /// </summary>
        [AlgoApiField("prev")]
        [Tooltip("Previous block hash.")]
        public BlockHash PreviousBlockHash;

        /// <summary>
        /// See <see cref="BlockRewards"/>
        /// </summary>
        [AlgoApiField("rewards")]
        public BlockRewards Rewards;

        /// <summary>
        /// [rnd] Current round on which this block was appended to the chain.
        /// </summary>
        [AlgoApiField("rnd")]
        [Tooltip("Current round on which this block was appended to the chain.")]
        public ulong Round;

        /// <summary>
        /// [seed] Sortition seed.
        /// </summary>
        [AlgoApiField("seed")]
        [Tooltip("Sortition seed.")]
        public Sha512_256_Hash Seed;

        /// <summary>
        /// [ts] Block creation timestamp in seconds since epoch
        /// </summary>
        [AlgoApiField("ts")]
        [Tooltip("Block creation timestamp in seconds since epoch")]
        public ulong Timestamp;

        /// <summary>
        /// [txns] list of transactions corresponding to a given round.
        /// </summary>
        [AlgoApiField("txns")]
        [Tooltip("list of transactions corresponding to a given round.")]
        public BlockTransaction[] Transactions;

        /// <summary>
        /// [tc] TxnCounter counts the number of transactions committed in the ledger, from the time at which support for this feature was introduced.
        /// </summary>
        /// <remarks>
        /// Specifically, TxnCounter is the number of the next transaction that will be committed after this block. It is 0 when no transactions have ever been committed (since TxnCounter started being supported).
        /// </remarks>
        [AlgoApiField("tc")]
        [Tooltip("TxnCounter counts the number of transactions committed in the ledger, from the time at which support for this feature was introduced.")]
        public ulong TransactionCounter;

        /// <summary>
        /// See <see cref="BlockUpgradeState"/>
        /// </summary>
        [AlgoApiField("upgrade-state")]
        public BlockUpgradeState UpgradeState;

        /// <summary>
        /// See <see cref="BlockUpgradeVote"/>
        /// </summary>
        [AlgoApiField("upgrade-vote")]
        public BlockUpgradeVote UpgradeVote;

        [AlgoApiField("cc")]
        public AlgoApiObject Cc;

        /// <summary>
        /// StateProofTracking tracks the status of the state proofs, potentially
        /// for multiple types of ASPs (Algorand's State Proofs).
        /// </summary>
        [AlgoApiField("spt")]
        public StateProofTrackingDataMap StateProofTracking;

        /// <inheritdoc />
        [AlgoApiField("fees")]
        public Address FeeSink
        {
            get => Rewards.FeeSink;
            set => Rewards.FeeSink = value;
        }

        /// <inheritdoc />
        [AlgoApiField("rwcalr")]
        public ulong RewardsCalculationRound
        {
            get => Rewards.RewardsCalculationRound;
            set => Rewards.RewardsCalculationRound = value;
        }

        /// <inheritdoc />
        [AlgoApiField("earn")]
        public ulong RewardsLevel
        {
            get => Rewards.RewardsLevel;
            set => Rewards.RewardsLevel = value;
        }

        /// <inheritdoc />
        [AlgoApiField("rwd")]
        public Address RewardsPool
        {
            get => Rewards.RewardsPool;
            set => Rewards.RewardsPool = value;
        }

        /// <inheritdoc />
        [AlgoApiField("rate")]
        public ulong RewardsRate
        {
            get => Rewards.RewardsRate;
            set => Rewards.RewardsRate = value;
        }

        /// <inheritdoc />
        [AlgoApiField("frac")]
        public ulong RewardsResidue
        {
            get => Rewards.RewardsResidue;
            set => Rewards.RewardsResidue = value;
        }

        /// <inheritdoc />
        [AlgoApiField("proto")]
        public FixedString128Bytes CurrentProtocol
        {
            get => UpgradeState.CurrentProtocol;
            set => UpgradeState.CurrentProtocol = value;
        }

        /// <inheritdoc />
        [AlgoApiField("nextproto")]
        public FixedString128Bytes NextProtocol
        {
            get => UpgradeState.NextProtocol;
            set => UpgradeState.NextProtocol = value;
        }

        /// <inheritdoc />
        [AlgoApiField("nextyes")]
        public ulong NextProtocolApprovals
        {
            get => UpgradeState.NextProtocolApprovals;
            set => UpgradeState.NextProtocolApprovals = value;
        }

        /// <inheritdoc />
        [AlgoApiField("nextswitch")]
        public ulong NextProtocolSwitchOn
        {
            get => UpgradeState.NextProtocolSwitchOn;
            set => UpgradeState.NextProtocolSwitchOn = value;
        }

        /// <inheritdoc />
        [AlgoApiField("nextbefore")]
        public ulong NextProtocolVoteBefore
        {
            get => UpgradeState.NextProtocolVoteBefore;
            set => UpgradeState.NextProtocolVoteBefore = value;
        }

        /// <inheritdoc />
        [AlgoApiField("upgradeyes")]
        public Optional<bool> UpgradeApprove
        {
            get => UpgradeVote.UpgradeApprove;
            set => UpgradeVote.UpgradeApprove = value;
        }

        /// <inheritdoc />
        [AlgoApiField("upgradedelay")]
        public ulong UpgradeDelay
        {
            get => UpgradeVote.UpgradeDelay;
            set => UpgradeVote.UpgradeDelay = value;
        }

        /// <inheritdoc />
        [AlgoApiField("upgradeprop")]
        public Address UpgradePropose
        {
            get => UpgradeVote.UpgradePropose;
            set => UpgradeVote.UpgradePropose = value;
        }

        /// <inheritdoc />
        [AlgoApiField("txn")]
        public Digest NativeSha512_256Commitment
        {
            get => txnCommitments.NativeSha512_256Commitment;
            set => txnCommitments.NativeSha512_256Commitment = value;
        }

        /// <inheritdoc />
        [AlgoApiField("txn256")]
        public Digest Sha256Commitment
        {
            get => txnCommitments.Sha256Commitment;
            set => txnCommitments.Sha256Commitment = value;
        }

        public bool Equals(BlockHeader other)
        {
            return GenesisId.Equals(other.GenesisId)
                && GenesisHash.Equals(other.GenesisHash)
                && PreviousBlockHash.Equals(other.PreviousBlockHash)
                && Round.Equals(other.Round)
                && CurrentProtocol.Equals(other.CurrentProtocol)
                && Seed.Equals(other.Seed)
                && txnCommitments.Equals(other.txnCommitments)
                ;
        }
    }
}
