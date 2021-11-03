using System;
using UnityEngine;

namespace AlgoSdk
{
    public interface IBlockRewards
    {
        /// <summary>
        /// [fees] accepts transaction fees, it can only spend to the incentive pool.
        /// </summary>
        Address FeeSink { get; set; }

        /// <summary>
        /// [rwcalr] number of leftover MicroAlgos after the distribution of rewards-rate MicroAlgos for every reward unit in the next round.
        /// </summary>
        ulong RewardsCalculationRound { get; set; }

        /// <summary>
        /// [earn] How many rewards, in MicroAlgos, have been distributed to each RewardUnit of MicroAlgos since genesis.
        /// </summary>
        ulong RewardsLevel { get; set; }

        /// <summary>
        /// [rwd] accepts periodic injections from the fee-sink and continually redistributes them as rewards.
        /// </summary>
        Address RewardsPool { get; set; }

        /// <summary>
        /// [rate] Number of new MicroAlgos added to the participation stake from rewards at the next round.
        /// </summary>
        ulong RewardsRate { get; set; }

        /// <summary>
        /// [frac] Number of leftover MicroAlgos after the distribution of RewardsRate/rewardUnits MicroAlgos for every reward unit in the next round.
        /// </summary>
        ulong RewardsResidue { get; set; }
    }

    [AlgoApiObject]
    public struct BlockRewards
        : IEquatable<BlockRewards>
        , IBlockRewards
    {
        [SerializeField]
        [Tooltip("accepts transaction fees, it can only spend to the incentive pool.")]
        Address feeSink;

        [SerializeField]
        [Tooltip("number of leftover MicroAlgos after the distribution of rewards-rate MicroAlgos for every reward unit in the next round.")]
        ulong rewardsCalculationRound;

        [SerializeField]
        [Tooltip("How many rewards, in MicroAlgos, have been distributed to each RewardUnit of MicroAlgos since genesis.")]
        ulong rewardsLevel;

        [SerializeField]
        [Tooltip("accepts periodic injections from the fee-sink and continually redistributes them as rewards.")]
        Address rewardsPool;

        [SerializeField]
        [Tooltip("Number of new MicroAlgos added to the participation stake from rewards at the next round.")]
        ulong rewardsRate;

        [SerializeField]
        [Tooltip("Number of leftover MicroAlgos after the distribution of RewardsRate/rewardUnits MicroAlgos for every reward unit in the next round.")]
        ulong rewardsResidue;

        [AlgoApiField("fee-sink", null)]
        public Address FeeSink
        {
            get => feeSink;
            set => feeSink = value;
        }

        [AlgoApiField("rewards-calculation-round", null)]
        public ulong RewardsCalculationRound
        {
            get => rewardsCalculationRound;
            set => rewardsCalculationRound = value;
        }

        [AlgoApiField("rewards-level", null)]
        public ulong RewardsLevel
        {
            get => rewardsLevel;
            set => rewardsLevel = value;
        }

        [AlgoApiField("rewards-pool", null)]
        public Address RewardsPool
        {
            get => rewardsPool;
            set => rewardsPool = value;
        }

        [AlgoApiField("rewards-rate", null)]
        public ulong RewardsRate
        {
            get => rewardsRate;
            set => rewardsRate = value;
        }

        [AlgoApiField("rewards-residue", null)]
        public ulong RewardsResidue
        {
            get => rewardsResidue;
            set => rewardsResidue = value;
        }

        public bool Equals(BlockRewards other)
        {
            return FeeSink.Equals(other.FeeSink)
                && RewardsCalculationRound.Equals(other.RewardsCalculationRound)
                && RewardsLevel.Equals(other.RewardsLevel)
                && RewardsPool.Equals(other.RewardsPool)
                && RewardsRate.Equals(other.RewardsRate)
                && RewardsResidue.Equals(other.RewardsResidue)
                ;
        }
    }
}
