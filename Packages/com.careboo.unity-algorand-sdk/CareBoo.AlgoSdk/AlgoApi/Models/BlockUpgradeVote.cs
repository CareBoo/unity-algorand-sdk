using System;
using Unity.Collections;

namespace AlgoSdk
{
    public struct BlockUpgradeVote : IEquatable<BlockUpgradeVote>
    {
        [AlgoApiKey("upgrade-approve", null)]
        public Optional<bool> UpgradeApprove;

        [AlgoApiKey("upgrade-delay", null)]
        public Optional<ulong> UpgradeDelay;

        [AlgoApiKey("upgrade-propose", null)]
        public FixedString128Bytes UpgradePropose;

        public bool Equals(BlockUpgradeVote other)
        {
            return UpgradeApprove.Equals(other.UpgradeApprove)
                && UpgradeDelay.Equals(other.UpgradeDelay)
                && UpgradePropose.Equals(other.UpgradePropose)
                ;
        }
    }
}
