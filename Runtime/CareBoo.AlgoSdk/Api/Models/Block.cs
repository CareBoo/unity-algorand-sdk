using System;
using AlgoSdk.Crypto;
using Unity.Collections;
using UnityEngine;

namespace AlgoSdk
{
    /// <summary>
    /// Block information.
    /// </summary>
    [AlgoApiObject]
    [Serializable]
    public partial struct Block
        : IEquatable<Block>
        , IBlockRewards
        , IBlockUpgradeState
        , IBlockUpgradeVote
    {
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
        public Sha512_256_Hash PreviousBlockHash;

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
        /// [txn] TransactionsRoot authenticates the set of transactions appearing in the block. More specifically, it's the root of a merkle tree whose leaves are the block's Txids, in lexicographic order. For the empty block, it's 0. Note that the TxnRoot does not authenticate the signatures on the transactions, only the transactions themselves. Two blocks with the same transactions but in a different order and with different signatures will have the same TxnRoot.
        /// </summary>
        [AlgoApiField("txn")]
        [Tooltip("TransactionsRoot authenticates the set of transactions appearing in the block. More specifically, it's the root of a merkle tree whose leaves are the block's Txids, in lexicographic order. For the empty block, it's 0. Note that the TxnRoot does not authenticate the signatures on the transactions, only the transactions themselves. Two blocks with the same transactions but in a different order and with different signatures will have the same TxnRoot.")]
        public Sha512_256_Hash RootTransaction;

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

        [AlgoApiField("fees")]
        public Address FeeSink
        {
            get => Rewards.FeeSink;
            set => Rewards.FeeSink = value;
        }

        [AlgoApiField("rwcalr")]
        public ulong RewardsCalculationRound
        {
            get => Rewards.RewardsCalculationRound;
            set => Rewards.RewardsCalculationRound = value;
        }

        [AlgoApiField("earn")]
        public ulong RewardsLevel
        {
            get => Rewards.RewardsLevel;
            set => Rewards.RewardsLevel = value;
        }

        [AlgoApiField("rwd")]
        public Address RewardsPool
        {
            get => Rewards.RewardsPool;
            set => Rewards.RewardsPool = value;
        }

        [AlgoApiField("rate")]
        public ulong RewardsRate
        {
            get => Rewards.RewardsRate;
            set => Rewards.RewardsRate = value;
        }

        [AlgoApiField("frac")]
        public ulong RewardsResidue
        {
            get => Rewards.RewardsResidue;
            set => Rewards.RewardsResidue = value;
        }

        [AlgoApiField("proto")]
        public FixedString128Bytes CurrentProtocol
        {
            get => UpgradeState.CurrentProtocol;
            set => UpgradeState.CurrentProtocol = value;
        }

        [AlgoApiField("nextproto")]
        public FixedString128Bytes NextProtocol
        {
            get => UpgradeState.NextProtocol;
            set => UpgradeState.NextProtocol = value;
        }

        [AlgoApiField("nextyes")]
        public ulong NextProtocolApprovals
        {
            get => UpgradeState.NextProtocolApprovals;
            set => UpgradeState.NextProtocolApprovals = value;
        }

        [AlgoApiField("nextswitch")]
        public ulong NextProtocolSwitchOn
        {
            get => UpgradeState.NextProtocolSwitchOn;
            set => UpgradeState.NextProtocolSwitchOn = value;
        }

        [AlgoApiField("nextbefore")]
        public ulong NextProtocolVoteBefore
        {
            get => UpgradeState.NextProtocolVoteBefore;
            set => UpgradeState.NextProtocolVoteBefore = value;
        }

        [AlgoApiField("upgradeyes")]
        public Optional<bool> UpgradeApprove
        {
            get => UpgradeVote.UpgradeApprove;
            set => UpgradeVote.UpgradeApprove = value;
        }

        [AlgoApiField("upgradedelay")]
        public ulong UpgradeDelay
        {
            get => UpgradeVote.UpgradeDelay;
            set => UpgradeVote.UpgradeDelay = value;
        }

        [AlgoApiField("upgradeprop")]
        public Address UpgradePropose
        {
            get => UpgradeVote.UpgradePropose;
            set => UpgradeVote.UpgradePropose = value;
        }

        [AlgoApiField("cc")]
        public AlgoApiObject Cc;

        public bool Equals(Block other)
        {
            return GenesisId.Equals(other.GenesisId)
                && GenesisHash.Equals(other.GenesisHash)
                && PreviousBlockHash.Equals(other.PreviousBlockHash)
                && Round.Equals(other.Round)
                && CurrentProtocol.Equals(other.CurrentProtocol)
                && Seed.Equals(other.Seed)
                ;
        }
    }
}
