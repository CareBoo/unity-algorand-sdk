using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct Status
        : IEquatable<Status>
    {
        [AlgoApiKey("catchpoint")]
        public string Catchpoint;
        [AlgoApiKey("catchpoint-acquired-blocks")]
        public ulong CatchpointAcquiredBlocks;
        [AlgoApiKey("catchpoint-processed-accounts")]
        public ulong CatchpointProcessedAmounts;
        [AlgoApiKey("catchpoint-total-accounts")]
        public ulong CatchpointTotalAccounts;
        [AlgoApiKey("catchpoint-total-blocks")]
        public ulong CatchpointTotalBlocks;
        [AlgoApiKey("catchpoint-verified-accounts")]
        public ulong CatchpointVerifiedAccounts;
        [AlgoApiKey("catchup-time")]
        public ulong CatchupTime;
        [AlgoApiKey("last-catchpoint")]
        public string LastCatchpoint;
        [AlgoApiKey("last-round")]
        public ulong LastRound;
        [AlgoApiKey("last-version")]
        public string LastVersion;
        [AlgoApiKey("next-version")]
        public string NextVersion;
        [AlgoApiKey("next-version-round")]
        public ulong NextVersionRound;
        [AlgoApiKey("next-version-supported")]
        public bool NextVersionSupported;
        [AlgoApiKey("stopped-at-unsupported-round")]
        public bool StoppedAtUnsupportedRound;
        [AlgoApiKey("time-since-last-round")]
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
