using System;
using Unity.Collections;
using UnityEngine;

namespace Algorand.Unity
{
    public interface IBlockUpgradeState
    {
        /// <summary>
        /// [proto] The current protocol version.
        /// </summary>
        FixedString128Bytes CurrentProtocol { get; set; }

        /// <summary>
        /// [nextproto] The next proposed protocol version.
        /// </summary>
        FixedString128Bytes NextProtocol { get; set; }

        /// <summary>
        /// [nextyes] Number of blocks which approved the protocol upgrade.
        /// </summary>
        ulong NextProtocolApprovals { get; set; }

        /// <summary>
        /// [nextswitch] Round on which the protocol upgrade will take effect.
        /// </summary>
        ulong NextProtocolSwitchOn { get; set; }

        /// <summary>
        /// [nextbefore] Deadline round for this protocol upgrade (No votes will be consider after this round).
        /// </summary>
        ulong NextProtocolVoteBefore { get; set; }
    }

    [AlgoApiObject]
    [Serializable]
    public partial struct BlockUpgradeState
        : IEquatable<BlockUpgradeState>
        , IBlockUpgradeState
    {
        [SerializeField]
        [Tooltip("The current protocol version.")]
        private FixedString128Bytes currentProtocol;

        [SerializeField]
        [Tooltip("The next proposed protocol version.")]
        private FixedString128Bytes nextProtocol;

        [SerializeField]
        [Tooltip("Number of blocks which approved the protocol upgrade.")]
        private ulong nextProtocolApprovals;

        [SerializeField]
        [Tooltip("Round on which the protocol upgrade will take effect.")]
        private ulong nextProtocolSwitchOn;

        [SerializeField]
        [Tooltip("Deadline round for this protocol upgrade (No votes will be consider after this round).")]
        private ulong nextProtocolVoteBefore;

        [AlgoApiField("current-protocol")]
        public FixedString128Bytes CurrentProtocol
        {
            get => currentProtocol;
            set => currentProtocol = value;
        }

        [AlgoApiField("next-protocol")]
        public FixedString128Bytes NextProtocol
        {
            get => nextProtocol;
            set => nextProtocol = value;
        }

        [AlgoApiField("next-protocol-approvals")]
        public ulong NextProtocolApprovals
        {
            get => nextProtocolApprovals;
            set => nextProtocolApprovals = value;
        }

        [AlgoApiField("next-protocol-switch-on")]
        public ulong NextProtocolSwitchOn
        {
            get => nextProtocolSwitchOn;
            set => nextProtocolSwitchOn = value;
        }

        [AlgoApiField("next-protocol-vote-before")]
        public ulong NextProtocolVoteBefore
        {
            get => nextProtocolVoteBefore;
            set => nextProtocolVoteBefore = value;
        }

        public bool Equals(BlockUpgradeState other)
        {
            return CurrentProtocol.Equals(other.CurrentProtocol)
                && NextProtocol.Equals(other.NextProtocol)
                && NextProtocolApprovals.Equals(other.NextProtocolApprovals)
                && NextProtocolSwitchOn.Equals(other.NextProtocolSwitchOn)
                && NextProtocolVoteBefore.Equals(other.NextProtocolVoteBefore)
                ;
        }
    }
}
