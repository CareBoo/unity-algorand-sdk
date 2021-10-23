using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct TransactionResponse
        : IEquatable<TransactionResponse>
        , IIndexerResponse
    {
        [AlgoApiField("transaction", null)]
        public Transaction Transaction { get; set; }

        [AlgoApiField("current-round", null)]
        public ulong CurrentRound { get; set; }

        public bool Equals(TransactionResponse other)
        {
            return Transaction.Equals(other.Transaction)
                && CurrentRound.Equals(other.CurrentRound)
                ;
        }
    }
}
