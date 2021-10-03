using System;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct BalancesQuery
        : IEquatable<BalancesQuery>
    {
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

        public bool Equals(BalancesQuery other)
        {
            return CurrencyGreaterThan.Equals(other.CurrencyGreaterThan)
                && CurrencyLessThan.Equals(other.CurrencyLessThan)
                && IncludeAll.Equals(other.IncludeAll)
                && Limit.Equals(other.Limit)
                && Next.Equals(other.Next)
                && Round.Equals(other.Round)
                ;
        }
    }
}
