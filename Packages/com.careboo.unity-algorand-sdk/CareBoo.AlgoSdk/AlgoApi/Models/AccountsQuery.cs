using System;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct AccountsQuery
        : IEquatable<AccountsQuery>
    {
        [AlgoApiField("application-id", null)]
        public ulong ApplicationId;

        [AlgoApiField("asset-id", null)]
        public ulong AssetId;

        [AlgoApiField("auth-addr", null)]
        public Address AuthAddr;

        [AlgoApiField("currency-greater-than", null)]
        public ulong CurrencyGreaterThan;

        [AlgoApiField("currency-less-than", null)]
        public ulong CurrencyLessThan;

        [AlgoApiField("include-all", null)]
        public Optional<bool> IncludeAll;

        [AlgoApiField("limit", null)]
        public ulong Limit;

        [AlgoApiField("next", null)]
        public FixedString128Bytes Next;

        [AlgoApiField("round", null)]
        public ulong Round;

        public bool Equals(AccountsQuery other)
        {
            return ApplicationId.Equals(other.ApplicationId)
                && AssetId.Equals(other.AssetId)
                && AuthAddr.Equals(other.AuthAddr)
                && CurrencyGreaterThan.Equals(other.CurrencyGreaterThan)
                && CurrencyLessThan.Equals(other.CurrencyLessThan)
                && IncludeAll.Equals(other.IncludeAll)
                && Limit.Equals(other.Limit)
                && Next.Equals(other.Next)
                && Round.Equals(other.Round)
                ;
        }
    }
}
