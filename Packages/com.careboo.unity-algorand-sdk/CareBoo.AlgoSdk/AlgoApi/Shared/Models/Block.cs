using System;
using AlgoSdk.Crypto;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public partial struct Block
        : IEquatable<Block>
    {
        [AlgoApiField("genesis-hash", "gh")]
        public GenesisHash GenesisHash;

        [AlgoApiField("genesis-id", "gen")]
        public FixedString64Bytes GenesisId;

        [AlgoApiField("previous-block-hash", "prev")]
        public Sha512_256_Hash PreviousBlockHash;

        [AlgoApiField("rewards", null)]
        public BlockRewards Rewards;

        [AlgoApiField("round", "rnd")]
        public ulong Round;

        [AlgoApiField("seed", "seed")]
        public Sha512_256_Hash Seed;

        [AlgoApiField("timestamp", "ts")]
        public ulong Timestamp;

        [AlgoApiField("transactions", "txns")]
        public BlockTransaction[] Transactions;

        [AlgoApiField("transaction-root", "txn")]
        public Sha512_256_Hash RootTransaction;

        [AlgoApiField("txn-counter", "tc")]
        public ulong TransactionCounter;

        [AlgoApiField("upgrade-state", null)]
        public BlockUpgradeState UpgradeState;

        [AlgoApiField("upgrade-vote", null)]
        public BlockUpgradeVote UpgradeVote;


        [AlgoApiField(null, "fees")]
        public Address FeeSink
        {
            get => Rewards.FeeSink;
            set => Rewards.FeeSink = value;
        }

        [AlgoApiField(null, "rwcalr")]
        public ulong RewardsCalculationRound
        {
            get => Rewards.RewardsCalculationRound;
            set => Rewards.RewardsCalculationRound = value;
        }

        [AlgoApiField(null, "earn")]
        public ulong RewardsLevel
        {
            get => Rewards.RewardsLevel;
            set => Rewards.RewardsLevel = value;
        }

        [AlgoApiField(null, "rwd")]
        public Address RewardsPool
        {
            get => Rewards.RewardsPool;
            set => Rewards.RewardsPool = value;
        }

        [AlgoApiField(null, "rate")]
        public ulong RewardsRate
        {
            get => Rewards.RewardsRate;
            set => Rewards.RewardsRate = value;
        }

        [AlgoApiField(null, "frac")]
        public ulong RewardsResidue
        {
            get => Rewards.RewardsResidue;
            set => Rewards.RewardsResidue = value;
        }

        [AlgoApiField(null, "proto")]
        public FixedString128Bytes CurrentProtocol
        {
            get => UpgradeState.CurrentProtocol;
            set => UpgradeState.CurrentProtocol = value;
        }

        [AlgoApiField(null, "nextproto")]
        public FixedString128Bytes NextProtocol
        {
            get => UpgradeState.NextProtocol;
            set => UpgradeState.NextProtocol = value;
        }

        [AlgoApiField(null, "nextyes")]
        public ulong NextProtocolApprovals
        {
            get => UpgradeState.NextProtocolApprovals;
            set => UpgradeState.NextProtocolApprovals = value;
        }

        [AlgoApiField(null, "nextswitch")]
        public ulong NextProtocolSwitchOn
        {
            get => UpgradeState.NextProtocolSwitchOn;
            set => UpgradeState.NextProtocolSwitchOn = value;
        }

        [AlgoApiField(null, "nextbefore")]
        public ulong NextProtocolVoteBefore
        {
            get => UpgradeState.NextProtocolVoteBefore;
            set => UpgradeState.NextProtocolVoteBefore = value;
        }

        [AlgoApiField(null, "upgradeyes")]
        public Optional<bool> UpgradeApprove
        {
            get => UpgradeVote.UpgradeApprove;
            set => UpgradeVote.UpgradeApprove = value;
        }

        [AlgoApiField(null, "upgradedelay")]
        public ulong UpgradeDelay
        {
            get => UpgradeVote.UpgradeDelay;
            set => UpgradeVote.UpgradeDelay = value;
        }

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
