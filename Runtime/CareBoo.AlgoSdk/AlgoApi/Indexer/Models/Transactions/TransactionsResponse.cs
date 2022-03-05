using System;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public partial struct TransactionsResponse
        : IEquatable<TransactionsResponse>
        , IPaginatedResponse
    {
        [AlgoApiField("current-round", null)]
        public ulong CurrentRound { get; set; }

        [AlgoApiField("next-token", null)]
        public FixedString128Bytes NextToken { get; set; }

        [AlgoApiField("transactions", null)]
        public Transaction[] Transactions { get; set; }

        public bool Equals(TransactionsResponse other)
        {
            return CurrentRound.Equals(other.CurrentRound)
                && NextToken.Equals(other.NextToken)
                && ArrayComparer.Equals(Transactions, other.Transactions)
                ;
        }
    }
}
