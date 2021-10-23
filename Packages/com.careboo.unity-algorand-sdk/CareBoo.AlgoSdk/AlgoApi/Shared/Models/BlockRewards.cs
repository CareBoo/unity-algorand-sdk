using System;

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
        [AlgoApiField("fee-sink", null)]
        public Address FeeSink { get; set; }

        [AlgoApiField("rewards-calculation-round", null)]
        public ulong RewardsCalculationRound { get; set; }

        [AlgoApiField("rewards-level", null)]
        public ulong RewardsLevel { get; set; }

        [AlgoApiField("rewards-pool", null)]
        public Address RewardsPool { get; set; }

        [AlgoApiField("rewards-rate", null)]
        public ulong RewardsRate { get; set; }

        [AlgoApiField("rewards-residue", null)]
        public ulong RewardsResidue { get; set; }

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
