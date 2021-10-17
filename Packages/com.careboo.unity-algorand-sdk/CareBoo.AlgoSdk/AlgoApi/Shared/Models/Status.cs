using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct Status
        : IEquatable<Status>
    {
        [AlgoApiField("catchpoint", null)]
        public string Catchpoint;
        [AlgoApiField("catchpoint-acquired-blocks", null)]
        public ulong CatchpointAcquiredBlocks;
        [AlgoApiField("catchpoint-processed-accounts", null)]
        public ulong CatchpointProcessedAmounts;
        [AlgoApiField("catchpoint-total-accounts", null)]
        public ulong CatchpointTotalAccounts;
        [AlgoApiField("catchpoint-total-blocks", null)]
        public ulong CatchpointTotalBlocks;
        [AlgoApiField("catchpoint-verified-accounts", null)]
        public ulong CatchpointVerifiedAccounts;
        [AlgoApiField("catchup-time", null)]
        public ulong CatchupTime;
        [AlgoApiField("last-catchpoint", null)]
        public string LastCatchpoint;
        [AlgoApiField("last-round", null)]
        public ulong LastRound;
        [AlgoApiField("last-version", null)]
        public string LastVersion;
        [AlgoApiField("next-version", null)]
        public string NextVersion;
        [AlgoApiField("next-version-round", null)]
        public ulong NextVersionRound;
        [AlgoApiField("next-version-supported", null)]
        public bool NextVersionSupported;
        [AlgoApiField("stopped-at-unsupported-round", null)]
        public bool StoppedAtUnsupportedRound;
        [AlgoApiField("time-since-last-round", null)]
        public ulong TimeSinceLastRound;

        public bool Equals(Status other)
        {
            return StringComparer.Equals(Catchpoint, other.Catchpoint)
                && LastRound.Equals(other.LastRound)
                && TimeSinceLastRound.Equals(other.TimeSinceLastRound)
                ;
        }
    }
}
