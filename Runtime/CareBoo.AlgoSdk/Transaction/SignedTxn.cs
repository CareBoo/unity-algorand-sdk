using System;

namespace AlgoSdk
{
    public interface ISignedTxn<TTxn>
        where TTxn : ITransaction, IEquatable<TTxn>
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
    /// An untyped signed transaction. See <see cref="SignedTxn{}"/> for a typed version.
    /// </summary>
    [AlgoApiObject]
    public partial struct SignedTxn
        : IEquatable<SignedTxn>
        , ISignedTxn<Transaction>
    {
        [AlgoApiField("sig", "sig")]
        public Sig Sig { get; set; }

        [AlgoApiField("msig", "msig")]
        public Multisig Msig { get; set; }

        [AlgoApiField("lsig", "lsig")]
        public LogicSig Lsig { get; set; }

        [AlgoApiField("txn", "txn")]
        public Transaction Txn { get; set; }

        [AlgoApiField("sgnr", "sgnr")]
        public Address AuthAddr { get; set; }

        public bool Equals(SignedTxn other)
        {
            return Sig.Equals(other.Sig)
                && Msig.Equals(other.Msig)
                && Lsig.Equals(other.Lsig)
                && Txn.Equals(other.Txn)
                && AuthAddr.Equals(other.AuthAddr)
                ;
        }
    }

    /// <summary>
    /// A typed signed transaction.
    /// </summary>
    /// <typeparam name="TTxn">The type of the transaction backing this struct.</typeparam>
    [AlgoApiObject]
    public partial struct SignedTxn<TTxn>
        : IEquatable<SignedTxn<TTxn>>
        , ISignedTxn<TTxn>
        where TTxn : ITransaction, IEquatable<TTxn>
    {
        [AlgoApiField("sig", "sig")]
        public Sig Sig { get; set; }

        [AlgoApiField("msig", "msig")]
        public Multisig Msig { get; set; }

        [AlgoApiField("lsig", "lsig")]
        public LogicSig Lsig { get; set; }

        [AlgoApiField("txn", "txn")]
        public TTxn Txn { get; set; }

        [AlgoApiField("sgnr", "sgnr")]
        public Address AuthAddr { get; set; }

        public bool Equals(SignedTxn<TTxn> other)
        {
            return Sig.Equals(other.Sig)
                && Msig.Equals(other.Msig)
                && Lsig.Equals(other.Lsig)
                && Txn.Equals(other.Txn)
                && AuthAddr.Equals(other.AuthAddr)
                ;
        }

        public SignedTxn ToUntyped()
        {
            Transaction raw = default;
            Txn.CopyTo(ref raw);
            return new SignedTxn { Txn = raw };
        }

        public static implicit operator SignedTxn(SignedTxn<TTxn> signedTxn) => signedTxn.ToUntyped();
    }

    /// <summary>
    /// An untyped signed transaction. See <see cref="SignedTxn{}"/> for a typed version.
    /// </summary>
    [Serializable]
    [Obsolete("Use SignedTxn instead.")]
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
        public TransactionSignature Signature { get; set; }

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

        public static implicit operator SignedTransaction(SignedTxn signedTxn)
        {
            return new SignedTransaction
            {
                Transaction = signedTxn.Txn,
                Signature = new TransactionSignature { Sig = signedTxn.Sig, Multisig = signedTxn.Msig, LogicSig = signedTxn.Lsig },
                AuthAddr = signedTxn.AuthAddr
            };
        }

        public static implicit operator SignedTxn(SignedTransaction signedTxn)
        {
            return new SignedTxn
            {
                Txn = signedTxn.Txn,
                Sig = signedTxn.Sig,
                Msig = signedTxn.Msig,
                Lsig = signedTxn.Lsig,
                AuthAddr = signedTxn.AuthAddr
            };
        }
    }

    /// <summary>
    /// A typed signed transaction.
    /// </summary>
    /// <typeparam name="TTransaction">The type of the transaction backing this struct.</typeparam>
    [Serializable]
    [Obsolete("Use SignedTxn<TTxn> instead.")]
    public partial struct Signed<TTransaction>
        : IEquatable<Signed<TTransaction>>
        , ISignedTxn<TTransaction>
        where TTransaction : ITransaction, IEquatable<TTransaction>
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

        public static implicit operator Signed<TTransaction>(SignedTxn<TTransaction> signedTxn)
        {
            return new Signed<TTransaction>
            {
                Transaction = signedTxn.Txn,
                Signature = new TransactionSignature { Sig = signedTxn.Sig, Multisig = signedTxn.Msig, LogicSig = signedTxn.Lsig },
                AuthAddr = signedTxn.AuthAddr
            };
        }

        public static implicit operator SignedTxn<TTransaction>(Signed<TTransaction> signedTxn)
        {
            return new SignedTxn<TTransaction>
            {
                Txn = signedTxn.Txn,
                Sig = signedTxn.Sig,
                Msig = signedTxn.Msig,
                Lsig = signedTxn.Lsig,
                AuthAddr = signedTxn.AuthAddr
            };
        }
    }
}
