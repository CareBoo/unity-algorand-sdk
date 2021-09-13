using System;
using AlgoSdk.MsgPack;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct Status
        : IMessagePackObject
        , IEquatable<Status>
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
            return this.Equals(ref other);
        }
    }
}

namespace AlgoSdk.MsgPack
{
    internal static partial class FieldMaps
    {
        internal static readonly Field<Status>.Map statusFields =
            new Field<Status>.Map()
                .Assign("catchpoint", (ref Status x) => ref x.Catchpoint, StringComparer.Instance)
                .Assign("catchpoint-acquired-blocks", (ref Status x) => ref x.CatchpointAcquiredBlocks)
                .Assign("catchpoint-processed-accounts", (ref Status x) => ref x.CatchpointProcessedAmounts)
                .Assign("catchpoint-total-accounts", (ref Status x) => ref x.CatchpointTotalAccounts)
                .Assign("catchpoint-total-blocks", (ref Status x) => ref x.CatchpointTotalBlocks)
                .Assign("catchpoint-verified-accounts", (ref Status x) => ref x.CatchpointVerifiedAccounts)
                .Assign("catchup-time", (ref Status x) => ref x.CatchupTime)
                .Assign("last-catchpoint", (ref Status x) => ref x.LastCatchpoint, StringComparer.Instance)
                .Assign("last-round", (ref Status x) => ref x.LastRound)
                .Assign("last-version", (ref Status x) => ref x.LastVersion, StringComparer.Instance)
                .Assign("next-version", (ref Status x) => ref x.NextVersion, StringComparer.Instance)
                .Assign("next-version-round", (ref Status x) => ref x.NextVersionRound)
                .Assign("next-version-supported", (ref Status x) => ref x.NextVersionSupported)
                .Assign("stopped-at-unsupported-round", (ref Status x) => ref x.StoppedAtUnsupportedRound)
                .Assign("time-since-last-round", (ref Status x) => ref x.TimeSinceLastRound)
                ;
    }
}
