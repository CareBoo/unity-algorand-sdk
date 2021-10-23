using System;

namespace AlgoSdk
{
    /// <summary>
    /// The node status from <see cref="IAlgodClient.GetCurrentStatus"/> and <see cref="IAlgodClient.GetStatusAfterWaitingForRound(ulong)"/>
    /// </summary>
    [AlgoApiObject]
    public struct Status
        : IEquatable<Status>
    {
        /// <summary>
        /// The current catchpoint that is being caught up to
        /// </summary>
        [AlgoApiField("catchpoint", null)]
        public string Catchpoint;

        /// <summary>
        /// The number of blocks that have already been obtained by the node as part of the catchup
        /// </summary>
        [AlgoApiField("catchpoint-acquired-blocks", null)]
        public ulong CatchpointAcquiredBlocks;

        /// <summary>
        /// The number of accounts from the current catchpoint that have been processed so far as part of the catchup
        /// </summary>
        [AlgoApiField("catchpoint-processed-accounts", null)]
        public ulong CatchpointProcessedAmounts;

        /// <summary>
        /// The total number of accounts included in the current catchpoint
        /// </summary>
        [AlgoApiField("catchpoint-total-accounts", null)]
        public ulong CatchpointTotalAccounts;

        /// <summary>
        /// The total number of blocks that are required to complete the current catchpoint catchup
        /// </summary>
        [AlgoApiField("catchpoint-total-blocks", null)]
        public ulong CatchpointTotalBlocks;

        /// <summary>
        /// The number of accounts from the current catchpoint that have been verified so far as part of the catchup
        /// </summary>
        [AlgoApiField("catchpoint-verified-accounts", null)]
        public ulong CatchpointVerifiedAccounts;

        /// <summary>
        /// CatchupTime in nanoseconds
        /// </summary>
        [AlgoApiField("catchup-time", null)]
        public ulong CatchupTime;

        /// <summary>
        /// The last catchpoint seen by the node
        /// </summary>
        [AlgoApiField("last-catchpoint", null)]
        public string LastCatchpoint;

        /// <summary>
        /// indicates the last round seen
        /// </summary>
        [AlgoApiField("last-round", null)]
        public ulong LastRound;

        /// <summary>
        /// indicates the last consensus version supported
        /// </summary>
        [AlgoApiField("last-version", null)]
        public string LastVersion;

        /// <summary>
        /// NextVersion of consensus protocol to use
        /// </summary>
        [AlgoApiField("next-version", null)]
        public string NextVersion;

        /// <summary>
        /// NextVersionRound is the round at which the next consensus version will apply
        /// </summary>
        [AlgoApiField("next-version-round", null)]
        public ulong NextVersionRound;

        /// <summary>
        /// NextVersionSupported indicates whether the next consensus version is supported by this node
        /// </summary>
        [AlgoApiField("next-version-supported", null)]
        public bool NextVersionSupported;

        /// <summary>
        /// StoppedAtUnsupportedRound indicates that the node does not support the new rounds and has stopped making progress
        /// </summary>
        [AlgoApiField("stopped-at-unsupported-round", null)]
        public bool StoppedAtUnsupportedRound;

        /// <summary>
        /// TimeSinceLastRound in nanoseconds
        /// </summary>
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
