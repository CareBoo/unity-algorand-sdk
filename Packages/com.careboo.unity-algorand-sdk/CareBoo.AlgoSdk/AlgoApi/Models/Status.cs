using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct Status
        : IEquatable<Status>
    {
        [AlgoApiKey("catchpoint", null)]
        public string Catchpoint;
        [AlgoApiKey("catchpoint-acquired-blocks", null)]
        public ulong CatchpointAcquiredBlocks;
        [AlgoApiKey("catchpoint-processed-accounts", null)]
        public ulong CatchpointProcessedAmounts;
        [AlgoApiKey("catchpoint-total-accounts", null)]
        public ulong CatchpointTotalAccounts;
        [AlgoApiKey("catchpoint-total-blocks", null)]
        public ulong CatchpointTotalBlocks;
        [AlgoApiKey("catchpoint-verified-accounts", null)]
        public ulong CatchpointVerifiedAccounts;
        [AlgoApiKey("catchup-time", null)]
        public ulong CatchupTime;
        [AlgoApiKey("last-catchpoint", null)]
        public string LastCatchpoint;
        [AlgoApiKey("last-round", null)]
        public ulong LastRound;
        [AlgoApiKey("last-version", null)]
        public string LastVersion;
        [AlgoApiKey("next-version", null)]
        public string NextVersion;
        [AlgoApiKey("next-version-round", null)]
        public ulong NextVersionRound;
        [AlgoApiKey("next-version-supported", null)]
        public bool NextVersionSupported;
        [AlgoApiKey("stopped-at-unsupported-round", null)]
        public bool StoppedAtUnsupportedRound;
        [AlgoApiKey("time-since-last-round", null)]
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
