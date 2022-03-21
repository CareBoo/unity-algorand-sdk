using System;
using AlgoSdk.Formatters;

namespace AlgoSdk
{
    public interface ISignedTxn<TTxn>
        where TTxn : ITransaction
    {
        /// <summary>
        /// The signature used to sign the transaction if there was one.
        /// </summary>
        Sig Sig { get; set; }

        /// <summary>
        /// The multi-signature used to sign the transaction if there was one.
        /// </summary>
        Multisig Msig { get; set; }

        /// <summary>
        /// The logic sig used to sign the transaction if there was one.
        /// </summary>
        LogicSig Lsig { get; set; }

        /// <summary>
        /// The transaction signed.
        /// </summary>
        TTxn Txn { get; set; }

        /// <summary>
        /// The signer of the transaction if this account was rekeyed.
        /// </summary>
        Address AuthAddr { get; set; }
    }

    /// <summary>
    /// An untyped signed transaction. See <see cref="Signed{}"/> for a typed version.
    /// This is used as a wrapper around <see cref="Transaction"/> for the Algorand API.
    /// </summary>
    [AlgoApiFormatter(typeof(SignedTransactionFormatter))]
    [Serializable]
    public partial struct SignedTransaction
        : IEquatable<SignedTransaction>
        , ISignedTxn<Transaction>
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

        public Transaction Txn
        {
            get => Transaction;
            set => Transaction = value;
        }

        public Sig Sig
        {
            get => Signature.Sig;
            set => Signature = value;
        }

        public Multisig Msig
        {
            get => Signature.Multisig;
            set => Signature = value;
        }

        public LogicSig Lsig
        {
            get => Signature.LogicSig;
            set => Signature = value;
        }

        public Address AuthAddr { get; set; }

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
        , ISignedTxn<TTransaction>
        where TTransaction : ITransaction
    {
        /// <summary>
        /// The typed transaction that was signed.
        /// </summary>
        public TTransaction Transaction;

        /// <summary>
        /// The signature this transaction was signed with.
        /// </summary>
        public TransactionSignature Signature;

        public TTransaction Txn
        {
            get => Transaction;
            set => Transaction = value;
        }

        public Sig Sig
        {
            get => Signature.Sig;
            set => Signature = value;
        }

        public Multisig Msig
        {
            get => Signature.Multisig;
            set => Signature = value;
        }

        public LogicSig Lsig
        {
            get => Signature.LogicSig;
            set => Signature = value;
        }

        public Address AuthAddr { get; set; }

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
