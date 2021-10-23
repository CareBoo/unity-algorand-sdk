using System;
using AlgoSdk.Crypto;
using Unity.Collections;

namespace AlgoSdk
{
    /// <summary>
    /// Block information.
    /// </summary>
    [AlgoApiObject]
    public struct Block
        : IEquatable<Block>
        , IBlockRewards
        , IBlockUpgradeState
        , IBlockUpgradeVote
    {
        /// <summary>
        /// [gh] hash to which this block belongs.
        /// </summary>
        [AlgoApiField("genesis-hash", "gh")]
        public GenesisHash GenesisHash;

        /// <summary>
        /// [gen] ID to which this block belongs.
        /// </summary>
        [AlgoApiField("genesis-id", "gen")]
        public FixedString64Bytes GenesisId;

        /// <summary>
        /// [prev] Previous block hash.
        /// </summary>
        [AlgoApiField("previous-block-hash", "prev")]
        public Sha512_256_Hash PreviousBlockHash;

        /// <summary>
        /// See <see cref="BlockRewards"/>
        /// </summary>
        [AlgoApiField("rewards", null)]
        public BlockRewards Rewards;

        /// <summary>
        /// [rnd] Current round on which this block was appended to the chain.
        /// </summary>
        [AlgoApiField("round", "rnd")]
        public ulong Round;

        /// <summary>
        /// [seed] Sortition seed.
        /// </summary>
        [AlgoApiField("seed", "seed")]
        public Sha512_256_Hash Seed;

        /// <summary>
        /// [ts] Block creation timestamp in seconds since epoch
        /// </summary>
        [AlgoApiField("timestamp", "ts")]
        public ulong Timestamp;

        /// <summary>
        /// [txns] list of transactions corresponding to a given round.
        /// </summary>
        [AlgoApiField("transactions", "txns")]
        public BlockTransaction[] Transactions;

        /// <summary>
        /// [txn] TransactionsRoot authenticates the set of transactions appearing in the block. More specifically, it's the root of a merkle tree whose leaves are the block's Txids, in lexicographic order. For the empty block, it's 0. Note that the TxnRoot does not authenticate the signatures on the transactions, only the transactions themselves. Two blocks with the same transactions but in a different order and with different signatures will have the same TxnRoot.
        /// </summary>
        [AlgoApiField("transaction-root", "txn")]
        public Sha512_256_Hash RootTransaction;

        /// <summary>
        /// [tc] TxnCounter counts the number of transactions committed in the ledger, from the time at which support for this feature was introduced.
        /// </summary>
        /// <remarks>
        /// Specifically, TxnCounter is the number of the next transaction that will be committed after this block. It is 0 when no transactions have ever been committed (since TxnCounter started being supported).
        /// </remarks>
        [AlgoApiField("txn-counter", "tc")]
        public ulong TransactionCounter;

        /// <summary>
        /// See <see cref="BlockUpgradeState"/>
        /// </summary>
        [AlgoApiField("upgrade-state", null)]
        public BlockUpgradeState UpgradeState;

        /// <summary>
        /// See <see cref="BlockUpgradeVote"/>
        /// </summary>
        [AlgoApiField("upgrade-vote", null)]
        public BlockUpgradeVote UpgradeVote;

        /// <summary>
        ///
        /// </summary>
        [AlgoApiField(null, "fees")]
        public Address FeeSink
        {
            get => Rewards.FeeSink;
            set => Rewards.FeeSink = value;
        }

        /// <summary>
        ///
        /// </summary>
        [AlgoApiField(null, "rwcalr")]
        public ulong RewardsCalculationRound
        {
            get => Rewards.RewardsCalculationRound;
            set => Rewards.RewardsCalculationRound = value;
        }

        /// <summary>
        ///
        /// </summary>
        [AlgoApiField(null, "earn")]
        public ulong RewardsLevel
        {
            get => Rewards.RewardsLevel;
            set => Rewards.RewardsLevel = value;
        }

        /// <summary>
        ///
        /// </summary>
        [AlgoApiField(null, "rwd")]
        public Address RewardsPool
        {
            get => Rewards.RewardsPool;
            set => Rewards.RewardsPool = value;
        }

        /// <summary>
        ///
        /// </summary>
        [AlgoApiField(null, "rate")]
        public ulong RewardsRate
        {
            get => Rewards.RewardsRate;
            set => Rewards.RewardsRate = value;
        }

        /// <summary>
        ///
        /// </summary>
        [AlgoApiField(null, "frac")]
        public ulong RewardsResidue
        {
            get => Rewards.RewardsResidue;
            set => Rewards.RewardsResidue = value;
        }

        /// <summary>
        ///
        /// </summary>
        [AlgoApiField(null, "proto")]
        public FixedString128Bytes CurrentProtocol
        {
            get => UpgradeState.CurrentProtocol;
            set => UpgradeState.CurrentProtocol = value;
        }

        /// <summary>
        ///
        /// </summary>
        [AlgoApiField(null, "nextproto")]
        public FixedString128Bytes NextProtocol
        {
            get => UpgradeState.NextProtocol;
            set => UpgradeState.NextProtocol = value;
        }

        /// <summary>
        ///
        /// </summary>
        [AlgoApiField(null, "nextyes")]
        public ulong NextProtocolApprovals
        {
            get => UpgradeState.NextProtocolApprovals;
            set => UpgradeState.NextProtocolApprovals = value;
        }

        /// <summary>
        ///
        /// </summary>
        [AlgoApiField(null, "nextswitch")]
        public ulong NextProtocolSwitchOn
        {
            get => UpgradeState.NextProtocolSwitchOn;
            set => UpgradeState.NextProtocolSwitchOn = value;
        }

        /// <summary>
        ///
        /// </summary>
        [AlgoApiField(null, "nextbefore")]
        public ulong NextProtocolVoteBefore
        {
            get => UpgradeState.NextProtocolVoteBefore;
            set => UpgradeState.NextProtocolVoteBefore = value;
        }

        /// <summary>
        ///
        /// </summary>
        [AlgoApiField(null, "upgradeyes")]
        public Optional<bool> UpgradeApprove
        {
            get => UpgradeVote.UpgradeApprove;
            set => UpgradeVote.UpgradeApprove = value;
        }

        /// <summary>
        ///
        /// </summary>
        [AlgoApiField(null, "upgradedelay")]
        public ulong UpgradeDelay
        {
            get => UpgradeVote.UpgradeDelay;
            set => UpgradeVote.UpgradeDelay = value;
        }

        /// <summary>
        ///
        /// </summary>
        [AlgoApiField(null, "upgradeprop")]
        public Address UpgradePropose
        {
            get => UpgradeVote.UpgradePropose;
            set => UpgradeVote.UpgradePropose = value;
        }

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
