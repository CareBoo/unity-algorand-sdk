using System;
using Unity.Collections;
using UnityEngine;

namespace AlgoSdk
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
    public struct BlockUpgradeState
        : IEquatable<BlockUpgradeState>
        , IBlockUpgradeState
    {
        [SerializeField]
        [Tooltip("The current protocol version.")]
        FixedString128Bytes currentProtocol;

        [SerializeField]
        [Tooltip("The next proposed protocol version.")]
        FixedString128Bytes nextProtocol;

        [SerializeField]
        [Tooltip("Number of blocks which approved the protocol upgrade.")]
        ulong nextProtocolApprovals;

        [SerializeField]
        [Tooltip("Round on which the protocol upgrade will take effect.")]
        ulong nextProtocolSwitchOn;

        [SerializeField]
        [Tooltip("Deadline round for this protocol upgrade (No votes will be consider after this round).")]
        ulong nextProtocolVoteBefore;

        [AlgoApiField("current-protocol", null)]
        public FixedString128Bytes CurrentProtocol
        {
            get => currentProtocol;
            set => currentProtocol = value;
        }

        [AlgoApiField("next-protocol", null)]
        public FixedString128Bytes NextProtocol
        {
            get => nextProtocol;
            set => nextProtocol = value;
        }

        [AlgoApiField("next-protocol-approvals", null)]
        public ulong NextProtocolApprovals
        {
            get => nextProtocolApprovals;
            set => nextProtocolApprovals = value;
        }

        [AlgoApiField("next-protocol-switch-on", null)]
        public ulong NextProtocolSwitchOn
        {
            get => nextProtocolSwitchOn;
            set => nextProtocolSwitchOn = value;
        }

        [AlgoApiField("next-protocol-vote-before", null)]
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
