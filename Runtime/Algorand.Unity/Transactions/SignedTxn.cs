using System;
using Unity.Collections;

namespace Algorand.Unity
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
        MultisigSig Msig { get; set; }

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
    }

    /// <summary>
    /// An untyped signed transaction. See <see cref="SignedTxn{}"/> for a typed version.
    /// </summary>
    [AlgoApiObject]
    public partial struct SignedTxn
        : IEquatable<SignedTxn>
        , ISignedTxn
    {
        private Transaction txn;
        private TransactionSignature signature;
        private Address authAddr;

        /// <inheritdoc />
        [AlgoApiField("sig")]
        public Sig Sig
        {
            get => signature.Sig;
            set => signature.Sig = value;
        }

        /// <inheritdoc />
        [AlgoApiField("msig")]
        public MultisigSig Msig
        {
            get => signature.Multisig;
            set => signature.Multisig = value;
        }

        /// <inheritdoc />
        [AlgoApiField("lsig")]
        public LogicSig Lsig
        {
            get => signature.LogicSig;
            set => signature.LogicSig = value;
        }

        /// <inheritdoc />
        [AlgoApiField("txn")]
        public Transaction Txn
        {
            get => txn;
            set => txn = value;
        }

        /// <inheritdoc />
        [AlgoApiField("sgnr")]
        public Address AuthAddr
        {
            get => authAddr;
            set => authAddr = value;
        }

        /// <inheritdoc />
        public TransactionSignature Signature
        {
            get => signature;
            set => signature = value;
        }

        /// <inheritdoc />
        public TransactionHeader Header
        {
            get => txn.Header;
            set => txn.Header = value;
        }

        /// <inheritdoc />
        public PaymentTxn.Params PaymentParams
        {
            get => txn.PaymentParams;
            set => txn.PaymentParams = value;
        }

        /// <inheritdoc />
        public AssetConfigTxn.Params AssetConfigParams
        {
            get => txn.AssetConfigParams;
            set => txn.AssetConfigParams = value;
        }

        /// <inheritdoc />
        public AssetTransferTxn.Params AssetTransferParams
        {
            get => txn.AssetTransferParams;
            set => txn.AssetTransferParams = value;
        }

        /// <inheritdoc />
        public AssetFreezeTxn.Params AssetFreezeParams
        {
            get => txn.AssetFreezeParams;
            set => txn.AssetFreezeParams = value;
        }

        /// <inheritdoc />
        public AppCallTxn.Params AppCallParams
        {
            get => txn.AppCallParams;
            set => txn.AppCallParams = value;
        }

        /// <inheritdoc />
        public KeyRegTxn.Params KeyRegParams
        {
            get => txn.KeyRegParams;
            set => txn.KeyRegParams = value;
        }

        /// <inheritdoc />
        public MicroAlgos Fee
        {
            get => txn.Fee;
            set => txn.Fee = value;
        }

        /// <inheritdoc />
        public ulong FirstValidRound
        {
            get => txn.FirstValidRound;
            set => txn.FirstValidRound = value;
        }

        /// <inheritdoc />
        public GenesisHash GenesisHash
        {
            get => txn.GenesisHash;
            set => txn.GenesisHash = value;
        }

        /// <inheritdoc />
        public ulong LastValidRound
        {
            get => txn.LastValidRound;
            set => txn.LastValidRound = value;
        }

        /// <inheritdoc />
        public Address Sender
        {
            get => txn.Sender;
            set => txn.Sender = value;
        }

        /// <inheritdoc />
        public TransactionType TransactionType
        {
            get => txn.TransactionType;
            set => txn.TransactionType = value;
        }

        /// <inheritdoc />
        public FixedString32Bytes GenesisId
        {
            get => txn.GenesisId;
            set => txn.GenesisId = value;
        }

        /// <inheritdoc />
        public TransactionId Group
        {
            get => txn.Group;
            set => txn.Group = value;
        }

        /// <inheritdoc />
        public TransactionId Lease
        {
            get => txn.Lease;
            set => txn.Lease = value;
        }

        /// <inheritdoc />
        public byte[] Note
        {
            get => txn.Note;
            set => txn.Note = value;
        }

        /// <inheritdoc />
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
        private TTxn txn;
        private TransactionSignature signature;
        private Address authAddr;

        /// <inheritdoc />
        [AlgoApiField("sig")]
        public Sig Sig
        {
            get => signature.Sig;
            set => signature.Sig = value;
        }

        /// <inheritdoc />
        [AlgoApiField("msig")]
        public MultisigSig Msig
        {
            get => signature.Multisig;
            set => signature.Multisig = value;
        }

        /// <inheritdoc />
        [AlgoApiField("lsig")]
        public LogicSig Lsig
        {
            get => signature.LogicSig;
            set => signature.LogicSig = value;
        }

        /// <inheritdoc />
        [AlgoApiField("txn")]
        public TTxn Txn
        {
            get => txn;
            set => txn = value;
        }

        /// <inheritdoc />
        [AlgoApiField("sgnr")]
        public Address AuthAddr
        {
            get => authAddr;
            set => authAddr = value;
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
        public SignedTxn ToUntyped()
        {
            Transaction raw = default;
            Txn.CopyTo(ref raw);
            return new SignedTxn { Txn = raw, Signature = signature, AuthAddr = authAddr };
        }

        public static implicit operator SignedTxn(SignedTxn<TTxn> signedTxn) => signedTxn.ToUntyped();
    }
}
