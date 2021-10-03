using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct PendingTransactions
        : IEquatable<PendingTransactions>
    {
        [AlgoApiField("top-transactions", null)]
        public PendingTransaction[] TopTransactions;

        [AlgoApiField("total-transactions", null)]
        public ulong TotalTransactions;

        public bool Equals(PendingTransactions other)
        {
            return TotalTransactions.Equals(other.TotalTransactions)
                && ArrayComparer.Equals(TopTransactions, other.TopTransactions)
                ;
        }
    }
}
