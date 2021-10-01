using System;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct DryrunRequest
        : IEquatable<DryrunRequest>
    {
        [AlgoApiKey("accounts", null)]
        public Account[] Accounts;
        [AlgoApiKey("apps", null)]
        public Application[] Applications;
        [AlgoApiKey("latest-timestamp", null)]
        public ulong LatestTimestamp;
        [AlgoApiKey("protocol-version", null)]
        public FixedString64Bytes ProtocolVersion;
        [AlgoApiKey("round", null)]
        public ulong Round;
        [AlgoApiKey("sources", null)]
        public DryrunSource[] Sources;
        [AlgoApiKey("txns", null)]
        public RawTransaction[] Transactions;

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
