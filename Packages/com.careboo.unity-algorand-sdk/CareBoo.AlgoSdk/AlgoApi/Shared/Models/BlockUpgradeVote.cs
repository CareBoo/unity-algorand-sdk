using System;
using Unity.Collections;

namespace AlgoSdk
{
    public struct BlockUpgradeVote : IEquatable<BlockUpgradeVote>
    {
        [AlgoApiField("upgrade-approve", null)]
        public Optional<bool> UpgradeApprove;

        [AlgoApiField("upgrade-delay", null)]
        public ulong UpgradeDelay;

        [AlgoApiField("upgrade-propose", null)]
        public Address UpgradePropose;

        public bool Equals(BlockUpgradeVote other)
        {
            return UpgradeApprove.Equals(other.UpgradeApprove)
                && UpgradeDelay.Equals(other.UpgradeDelay)
                && UpgradePropose.Equals(other.UpgradePropose)
                ;
        }
    }
}
