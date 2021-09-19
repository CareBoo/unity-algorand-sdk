using System;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct DryrunRequest
        : IEquatable<DryrunRequest>
    {
        [AlgoApiKey("accounts")]
        public Account[] Accounts;
        [AlgoApiKey("apps")]
        public Application[] Applications;
        [AlgoApiKey("latest-timestamp")]
        public ulong LatestTimestamp;
        [AlgoApiKey("protocol-version")]
        public FixedString64Bytes ProtocolVersion;
        [AlgoApiKey("round")]
        public ulong Round;
        [AlgoApiKey("sources")]
        public DryrunSource[] Sources;
        [AlgoApiKey("txns")]
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
