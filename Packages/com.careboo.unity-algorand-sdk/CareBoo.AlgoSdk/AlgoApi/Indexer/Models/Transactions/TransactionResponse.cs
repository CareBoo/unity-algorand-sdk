using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct TransactionResponse
        : IEquatable<TransactionResponse>
    {
        [AlgoApiField("transaction", null)]
        public Transaction Transaction;

        [AlgoApiField("current-round", null)]
        public ulong CurrentRound;

        public bool Equals(TransactionResponse other)
        {
            return Transaction.Equals(other.Transaction)
                && CurrentRound.Equals(other.CurrentRound)
                ;
        }
    }
}
