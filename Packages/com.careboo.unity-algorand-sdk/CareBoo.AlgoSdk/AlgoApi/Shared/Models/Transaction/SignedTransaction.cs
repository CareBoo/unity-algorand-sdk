using System;
using AlgoSdk.Formatters;

namespace AlgoSdk
{
    /// <summary>
    /// An untyped signed transaction. See <see cref="Signed{}"/> for a typed version.
    /// This is used as a wrapper around <see cref="Transaction"/> for the Algorand API.
    /// </summary>
    [AlgoApiFormatter(typeof(SignedTransactionFormatter))]
    [Serializable]
    public partial struct SignedTransaction
        : IEquatable<SignedTransaction>
    {
        /// <summary>
        /// The untyped <see cref="Transaction"/> backing this struct.
        /// </summary>
        public Transaction Transaction;

        /// <summary>
        /// The signatured this transaction is signed with.
        /// </summary>
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

    /// <summary>
    /// A typed signed transaction.
    /// </summary>
    /// <typeparam name="TTransaction">The type of the transaction backing this struct.</typeparam>
    [AlgoApiFormatter(typeof(SignedTransactionFormatter<>))]
    [Serializable]
    public partial struct Signed<TTransaction>
        : IEquatable<Signed<TTransaction>>
        where TTransaction : struct, ITransaction, IEquatable<TTransaction>
    {
        /// <summary>
        /// The typed transaction that was signed.
        /// </summary>
        public TTransaction Transaction;

        /// <summary>
        /// The signature this transaction was signed with.
        /// </summary>
        public TransactionSignature Signature;

        public bool Equals(Signed<TTransaction> other)
        {
            return Transaction.Equals(other.Transaction)
                && Signature.Equals(other.Signature)
                ;
        }

        public SignedTransaction ToUntyped()
        {
            Transaction raw = default;
            Transaction.CopyTo(ref raw);
            raw.Signature = Signature;
            return new SignedTransaction { Transaction = raw };
        }

        public static implicit operator SignedTransaction(Signed<TTransaction> typedSignedTxn)
        {
            return typedSignedTxn.ToUntyped();
        }
    }
}
