using System;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct BlockRewards
        : IEquatable<BlockRewards>
    {
        [AlgoApiKey("fee-sink")]
        public FixedString128Bytes FeeSink;

        [AlgoApiKey("rewards-calculation-round")]
        public ulong RewardsCalculationRound;

        [AlgoApiKey("rewards-level")]
        public ulong RewardsLevel;

        [AlgoApiKey("rewards-pool")]
        public FixedString128Bytes RewardsPool;

        [AlgoApiKey("rewards-rate")]
        public ulong RewardsRate;

        [AlgoApiKey("rewards-residue")]
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
