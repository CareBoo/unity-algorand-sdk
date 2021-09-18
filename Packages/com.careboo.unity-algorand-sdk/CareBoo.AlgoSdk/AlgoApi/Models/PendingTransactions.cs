using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct PendingTransactions
        : IEquatable<PendingTransactions>
    {
        [AlgoApiKey("top-transactions")]
        public PendingTransaction[] TopTransactions;

        [AlgoApiKey("total-transactions")]
        public ulong TotalTransactions;

        public bool Equals(PendingTransactions other)
        {
            return TotalTransactions.Equals(other.TotalTransactions)
                && ArrayComparer.Equals(TopTransactions, other.TopTransactions)
                ;
        }
    }
}
