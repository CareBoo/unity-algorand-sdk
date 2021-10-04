using System;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct TransactionsResponse
        : IEquatable<TransactionsResponse>
    {
        [AlgoApiField("current-round", null)]
        public ulong CurrentRound;

        [AlgoApiField("next-token", null)]
        public FixedString128Bytes NextToken;

        [AlgoApiField("transactions", null)]
        public Transaction[] Transactions;

        public bool Equals(TransactionsResponse other)
        {
            return CurrentRound.Equals(other.CurrentRound)
                && NextToken.Equals(other.NextToken)
                && ArrayComparer.Equals(Transactions, other.Transactions)
                ;
        }
    }
}
