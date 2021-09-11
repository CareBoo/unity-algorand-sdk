using System;
using AlgoSdk.MsgPack;

namespace AlgoSdk
{
    public struct Status
        : IMessagePackObject
        , IEquatable<Status>
    {
        public string Catchpoint;
        public ulong CatchpointAcquiredBlocks;
        public ulong CatchpointProcessedAmounts;
        public ulong CatchpointTotalAccounts;
        public ulong CatchpointTotalBlocks;
        public ulong CatchpointVerifiedAccounts;
        public ulong CatchupTime;
        public string LastCatchpoint;
        public ulong LastRound;
        public string LastVersion;
        public string NextVersion;
        public ulong NextVersionRound;
        public bool NextVersionSupported;
        public bool StoppedAtUnsupportedRound;
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
