using System;
using Unity.Collections;

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

    public interface ISignedTxn
        : ISignedTxn<Transaction>
        , IUntypedTransaction
    {
        /// <summary>
        /// The untyped signature, can be sig, multisig, or logic sig
        /// </summary>
        TransactionSignature Signature { get; set; }
    }

    /// <summary>
    /// An untyped signed transaction. See <see cref="SignedTxn{}"/> for a typed version.
    /// </summary>
    [AlgoApiObject]
    public partial struct SignedTxn
        : IEquatable<SignedTxn>
        , ISignedTxn
    {
        Transaction txn;
        TransactionSignature signature;
        Address authAddr;

        [AlgoApiField("sig", "sig")]
        public Sig Sig
        {
            get => signature.Sig;
            set => signature.Sig = value;
        }

        [AlgoApiField("msig", "msig")]
        public Multisig Msig
        {
            get => signature.Multisig;
            set => signature.Multisig = value;
        }

        [AlgoApiField("lsig", "lsig")]
        public LogicSig Lsig
        {
            get => signature.LogicSig;
            set => signature.LogicSig = value;
        }

        [AlgoApiField("txn", "txn")]
        public Transaction Txn
        {
            get => txn;
            set => txn = value;
        }

        [AlgoApiField("sgnr", "sgnr")]
        public Address AuthAddr
        {
            get => authAddr;
            set => authAddr = value;
        }

        public TransactionSignature Signature
        {
            get => signature;
            set => signature = value;
        }

        public TransactionHeader Header
        {
            get => txn.Header;
            set => txn.Header = value;
        }

        public PaymentTxn.Params PaymentParams
        {
            get => txn.PaymentParams;
            set => txn.PaymentParams = value;
        }

        public AssetConfigTxn.Params AssetConfigParams
        {
            get => txn.AssetConfigParams;
            set => txn.AssetConfigParams = value;
        }

        public AssetTransferTxn.Params AssetTransferParams
        {
            get => txn.AssetTransferParams;
            set => txn.AssetTransferParams = value;
        }

        public AssetFreezeTxn.Params AssetFreezeParams
        {
            get => txn.AssetFreezeParams;
            set => txn.AssetFreezeParams = value;
        }

        public AppCallTxn.Params AppCallParams
        {
            get => txn.AppCallParams;
            set => txn.AppCallParams = value;
        }

        public KeyRegTxn.Params KeyRegParams
        {
            get => txn.KeyRegParams;
            set => txn.KeyRegParams = value;
        }

        public MicroAlgos Fee
        {
            get => txn.Fee;
            set => txn.Fee = value;
        }

        public ulong FirstValidRound
        {
            get => txn.FirstValidRound;
            set => txn.FirstValidRound = value;
        }

        public GenesisHash GenesisHash
        {
            get => txn.GenesisHash;
            set => txn.GenesisHash = value;
        }

        public ulong LastValidRound
        {
            get => txn.LastValidRound;
            set => txn.LastValidRound = value;
        }

        public Address Sender
        {
            get => txn.Sender;
            set => txn.Sender = value;
        }

        public TransactionType TransactionType
        {
            get => txn.TransactionType;
            set => txn.TransactionType = value;
        }

        public FixedString32Bytes GenesisId
        {
            get => txn.GenesisId;
            set => txn.GenesisId = value;
        }

        public TransactionId Group
        {
            get => txn.Group;
            set => txn.Group = value;
        }

        public TransactionId Lease
        {
            get => txn.Lease;
            set => txn.Lease = value;
        }

        public byte[] Note
        {
            get => txn.Note;
            set => txn.Note = value;
        }

        public Address RekeyTo
        {
            get => txn.RekeyTo;
            set => txn.RekeyTo = value;
        }

        public bool Equals(SignedTxn other)
        {
            return Signature.Equals(other.Signature)
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
        TTxn txn;
        TransactionSignature signature;
        Address authAddr;

        [AlgoApiField("sig", "sig")]
        public Sig Sig
        {
            get => signature.Sig;
            set => signature.Sig = value;
        }

        [AlgoApiField("msig", "msig")]
        public Multisig Msig
        {
            get => signature.Multisig;
            set => signature.Multisig = value;
        }

        [AlgoApiField("lsig", "lsig")]
        public LogicSig Lsig
        {
            get => signature.LogicSig;
            set => signature.LogicSig = value;
        }

        [AlgoApiField("txn", "txn")]
        public TTxn Txn
        {
            get => txn;
            set => txn = value;
        }

        [AlgoApiField("sgnr", "sgnr")]
        public Address AuthAddr
        {
            get => authAddr;
            set => authAddr = value;
        }

        public TransactionSignature Signature
        {
            get => signature;
            set => signature = value;
        }

        public bool Equals(SignedTxn<TTxn> other)
        {
            return Signature.Equals(other.Signature)
                && Txn.Equals(other.Txn)
                && AuthAddr.Equals(other.AuthAddr)
                ;
        }

        public SignedTxn ToUntyped()
        {
            Transaction raw = default;
            Txn.CopyTo(ref raw);
            return new SignedTxn { Txn = raw, Signature = signature, AuthAddr = authAddr };
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
