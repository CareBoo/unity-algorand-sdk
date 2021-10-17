using System;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct DryrunRequest
        : IEquatable<DryrunRequest>
    {
        [AlgoApiField("accounts", null)]
        public Account[] Accounts;

        [AlgoApiField("apps", null)]
        public Application[] Applications;

        [AlgoApiField("latest-timestamp", null)]
        public ulong LatestTimestamp;

        [AlgoApiField("protocol-version", null)]
        public FixedString64Bytes ProtocolVersion;

        [AlgoApiField("round", null)]
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
