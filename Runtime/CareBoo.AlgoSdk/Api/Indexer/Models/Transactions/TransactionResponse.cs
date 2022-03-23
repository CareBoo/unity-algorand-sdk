using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public partial struct TransactionResponse
        : IEquatable<TransactionResponse>
        , IIndexerResponse<Transaction>
    {
        [AlgoApiField("transaction", null)]
        public Transaction Transaction { get; set; }

        [AlgoApiField("current-round", null)]
        public ulong CurrentRound { get; set; }

        Transaction IIndexerResponse<Transaction>.Result
        {
            get => Transaction;
            set => Transaction = value;
        }

        public bool Equals(TransactionResponse other)
        {
            return Transaction.Equals(other.Transaction)
                && CurrentRound.Equals(other.CurrentRound)
                ;
        }
    }
}
