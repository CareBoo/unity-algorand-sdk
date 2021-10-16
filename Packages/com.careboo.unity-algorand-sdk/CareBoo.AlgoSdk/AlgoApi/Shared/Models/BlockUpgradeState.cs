using System;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct BlockUpgradeState
        : IEquatable<BlockUpgradeState>
    {
        [AlgoApiField("current-protocol", null)]
        public FixedString128Bytes CurrentProtocol;

        [AlgoApiField("next-protocol", null)]
        public FixedString128Bytes NextProtocol;

        [AlgoApiField("next-protocol-approvals", null)]
        public ulong NextProtocolApprovals;

        [AlgoApiField("next-protocol-switch-on", null)]
        public ulong NextProtocolSwitchOn;

        [AlgoApiField("next-protocol-vote-before", null)]
        public ulong NextProtocolVoteBefore;


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
