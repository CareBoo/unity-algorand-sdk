using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct PendingTransactions
        : IEquatable<PendingTransactions>
    {
        [AlgoApiField("top-transactions", "top-transactions")]
        public SignedTransaction[] TopTransactions;

        [AlgoApiField("total-transactions", "total-transactions")]
        public ulong TotalTransactions;

        public bool Equals(PendingTransactions other)
        {
            return TotalTransactions.Equals(other.TotalTransactions)
                && ArrayComparer.Equals(TopTransactions, other.TopTransactions)
                ;
        }
    }
}
