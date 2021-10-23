using System;
using Unity.Collections;

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
    public struct BlockUpgradeState
        : IEquatable<BlockUpgradeState>
        , IBlockUpgradeState
    {
        [AlgoApiField("current-protocol", null)]
        public FixedString128Bytes CurrentProtocol { get; set; }

        [AlgoApiField("next-protocol", null)]
        public FixedString128Bytes NextProtocol { get; set; }

        [AlgoApiField("next-protocol-approvals", null)]
        public ulong NextProtocolApprovals { get; set; }

        [AlgoApiField("next-protocol-switch-on", null)]
        public ulong NextProtocolSwitchOn { get; set; }

        [AlgoApiField("next-protocol-vote-before", null)]
        public ulong NextProtocolVoteBefore { get; set; }


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
