using System;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct BlockRewards
        : IEquatable<BlockRewards>
    {
        [AlgoApiField("fee-sink", null)]
        public Address FeeSink;

        [AlgoApiField("rewards-calculation-round", null)]
        public ulong RewardsCalculationRound;

        [AlgoApiField("rewards-level", null)]
        public ulong RewardsLevel;

        [AlgoApiField("rewards-pool", null)]
        public Address RewardsPool;

        [AlgoApiField("rewards-rate", null)]
        public ulong RewardsRate;

        [AlgoApiField("rewards-residue", null)]
        public ulong RewardsResidue;

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
