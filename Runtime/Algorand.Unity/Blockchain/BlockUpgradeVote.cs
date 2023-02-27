using System;
using UnityEngine;

namespace Algorand.Unity
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

    [AlgoApiObject]
    [Serializable]
    public partial struct BlockUpgradeVote
        : IEquatable<BlockUpgradeVote>
        , IBlockUpgradeVote
    {
        [SerializeField]
        [Tooltip("Indicates a yes vote for the current proposal.")]
        private Optional<bool> upgradeApprove;

        [SerializeField]
        [Tooltip("Indicates the time between acceptance and execution.")]
        private ulong upgradeDelay;

        [SerializeField]
        [Tooltip("Indicates a proposed upgrade.")]
        private Address upgradePropose;

        [AlgoApiField("upgrade-approve")]
        public Optional<bool> UpgradeApprove
        {
            get => upgradeApprove;
            set => upgradeApprove = value;
        }

        [AlgoApiField("upgrade-delay")]
        public ulong UpgradeDelay
        {
            get => upgradeDelay;
            set => upgradeDelay = value;
        }

        [AlgoApiField("upgrade-propose")]
        public Address UpgradePropose
        {
            get => upgradePropose;
            set => upgradePropose = value;
        }

        public bool Equals(BlockUpgradeVote other)
        {
            return UpgradeApprove.Equals(other.UpgradeApprove)
                && UpgradeDelay.Equals(other.UpgradeDelay)
                && UpgradePropose.Equals(other.UpgradePropose)
                ;
        }
    }
}
