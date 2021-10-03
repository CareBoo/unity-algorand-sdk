using System;

namespace AlgoSdk
{
    [AlgoApiFormatter(typeof(SignedTransactionFormatter))]
    public struct SignedTransaction
        : IEquatable<SignedTransaction>
    {
        public Transaction Transaction;

        public TransactionSignature Signature
        {
            get => Transaction.Signature;
            set => Transaction.Signature = value;
        }

        public bool Equals(SignedTransaction other)
        {
            return Transaction.Equals(other.Transaction);
        }
    }
}
