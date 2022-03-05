using System;
using Unity.Collections;
using UnityEngine;

namespace AlgoSdk
{
    /// <summary>
    /// Request data type for dryrun endpoint. Given the Transactions and simulated ledger state upload, run TEAL scripts and return debugging information.
    /// </summary>
    [AlgoApiObject]
    [Serializable]
    public partial struct DryrunRequest
        : IEquatable<DryrunRequest>
    {
        [AlgoApiField("accounts", null)]
        public AccountInfo[] Accounts;

        [AlgoApiField("apps", null)]
        public Application[] Applications;

        /// <summary>
        /// LatestTimestamp is available to some TEAL scripts. Defaults to the latest confirmed timestamp this algod is attached to.
        /// </summary>
        [AlgoApiField("latest-timestamp", null)]
        [Tooltip("LatestTimestamp is available to some TEAL scripts. Defaults to the latest confirmed timestamp this algod is attached to.")]
        public ulong LatestTimestamp;

        /// <summary>
        /// ProtocolVersion specifies a specific version string to operate under, otherwise whatever the current protocol of the network this algod is running in.
        /// </summary>
        [AlgoApiField("protocol-version", null)]
        [Tooltip("ProtocolVersion specifies a specific version string to operate under, otherwise whatever the current protocol of the network this algod is running in.")]
        public FixedString64Bytes ProtocolVersion;

        /// <summary>
        /// Round is available to some TEAL scripts. Defaults to the current round on the network this algod is attached to.
        /// </summary>
        [AlgoApiField("round", null)]
        [Tooltip("Round is available to some TEAL scripts. Defaults to the current round on the network this algod is attached to.")]
        public ulong Round;

        [AlgoApiField("sources", null)]
        public DryrunSource[] Sources;

        [AlgoApiField("txns", null)]
        public Transaction[] Transactions;

        public bool Equals(DryrunRequest other)
        {
            return ArrayComparer.Equals(Accounts, other.Accounts)
                && ArrayComparer.Equals(Applications, other.Applications)
                && LatestTimestamp.Equals(other.LatestTimestamp)
                && ProtocolVersion.Equals(other.ProtocolVersion)
                && Round.Equals(other.Round)
                && ArrayComparer.Equals(Sources, other.Sources)
                && ArrayComparer.Equals(Transactions, other.Transactions);
            ;
        }
    }
}
