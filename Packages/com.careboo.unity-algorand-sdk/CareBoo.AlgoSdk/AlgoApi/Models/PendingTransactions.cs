using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct PendingTransactions
        : IEquatable<PendingTransactions>
    {
        [AlgoApiKey("top-transactions", null)]
        public PendingTransaction[] TopTransactions;

        [AlgoApiKey("total-transactions", null)]
        public ulong TotalTransactions;

        public bool Equals(PendingTransactions other)
        {
            return TotalTransactions.Equals(other.TotalTransactions)
                && ArrayComparer.Equals(TopTransactions, other.TopTransactions)
                ;
        }
    }
}
