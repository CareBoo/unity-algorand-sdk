using System;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct BlockUpgradeStatus
        : IEquatable<BlockUpgradeStatus>
    {
        [AlgoApiField("current-protocol", null)]
        public FixedString128Bytes CurrentProtocol;

        [AlgoApiField("next-protocol", null)]
        public FixedString128Bytes NextProtocol;

        [AlgoApiField("next-protocol-approvals", null)]
        public Optional<ulong> NextProtocolApprovals;

        [AlgoApiField("next-protocol-switch-on", null)]
        public Optional<ulong> NextProtocolSwitchOn;

        [AlgoApiField("next-protocol-vote-before", null)]
        public Optional<ulong> NextProtocolVoteBefore;


        public bool Equals(BlockUpgradeStatus other)
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
