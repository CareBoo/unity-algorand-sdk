using System;

namespace AlgoSdk
{
    public interface IBlockUpgradeVote
    {
        /// <summary>
        /// [upgradeyes] Indicates a yes vote for the current proposal.
        /// </summary>
        Optional<bool> UpgradeApprove { get; set; }

        /// <summary>
        /// [upgradedelay] Indicates the time between acceptance and execution.
        /// </summary>
        ulong UpgradeDelay { get; set; }

        /// <summary>
        /// [upgradeprop] Indicates a proposed upgrade.
        /// </summary>
        Address UpgradePropose { get; set; }
    }

    public struct BlockUpgradeVote
        : IEquatable<BlockUpgradeVote>
        , IBlockUpgradeVote
    {
        [AlgoApiField("upgrade-approve", null)]
        public Optional<bool> UpgradeApprove { get; set; }

        [AlgoApiField("upgrade-delay", null)]
        public ulong UpgradeDelay { get; set; }

        [AlgoApiField("upgrade-propose", null)]
        public Address UpgradePropose { get; set; }

        public bool Equals(BlockUpgradeVote other)
        {
            return UpgradeApprove.Equals(other.UpgradeApprove)
                && UpgradeDelay.Equals(other.UpgradeDelay)
                && UpgradePropose.Equals(other.UpgradePropose)
                ;
        }
    }
}
