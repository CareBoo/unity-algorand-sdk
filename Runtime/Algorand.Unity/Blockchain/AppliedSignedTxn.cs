using System;
using Unity.Collections;
using UnityEngine;

namespace Algorand.Unity
{
    public interface IAppliedSignedTxn<TTxn>
        : ISignedTxn
        , IApplyData<TTxn>
        , IEquatable<TTxn>
        where TTxn : IAppliedSignedTxn<TTxn>
    { }

    /// <summary>
    /// A signed transaction that has already been executed.
    /// </summary>
    [AlgoApiObject]
    [Serializable]
    public partial struct AppliedSignedTxn
        : IEquatable<AppliedSignedTxn>
        , IAppliedSignedTxn<AppliedSignedTxn>
    {
        [SerializeField] private SignedTxn txn;

        [SerializeField] private ApplyData<AppliedSignedTxn> applyData;

        /// <inheritdoc />
        [AlgoApiField("sig")]
        public Sig Sig
        {
            get => txn.Sig;
            set => txn.Sig = value;
        }

        /// <inheritdoc />
        [AlgoApiField("msig")]
        public MultisigSig Msig
        {
            get => txn.Msig;
            set => txn.Msig = value;
        }

        /// <inheritdoc />
        [AlgoApiField("lsig")]
        public LogicSig Lsig
        {
            get => txn.Lsig;
            set => txn.Lsig = value;
        }

        /// <inheritdoc />
        [AlgoApiField("txn")]
        public Transaction Txn
        {
            get => txn.Txn;
            set => txn.Txn = value;
        }

        /// <inheritdoc />
        [AlgoApiField("sgnr")]
        public Address AuthAddr
        {
            get => txn.AuthAddr;
            set => txn.AuthAddr = value;
        }

        /// <inheritdoc />
        [AlgoApiField("ca")]
        public MicroAlgos ClosingAmount
        {
            get => applyData.ClosingAmount;
            set => applyData.ClosingAmount = value;
        }

        /// <inheritdoc />
        [AlgoApiField("aca")]
        public ulong AssetClosingAmount
        {
            get => applyData.AssetClosingAmount;
            set => applyData.AssetClosingAmount = value;
        }

        /// <inheritdoc />
        [AlgoApiField("rs")]
        public MicroAlgos SenderRewards
        {
            get => applyData.SenderRewards;
            set => applyData.SenderRewards = value;
        }

        /// <inheritdoc />
        [AlgoApiField("rr")]
        public MicroAlgos ReceiverRewards
        {
            get => applyData.ReceiverRewards;
            set => applyData.ReceiverRewards = value;
        }

        /// <inheritdoc />
        [AlgoApiField("rc")]
        public MicroAlgos CloseRewards
        {
            get => applyData.CloseRewards;
            set => applyData.CloseRewards = value;
        }

        /// <inheritdoc />
        [AlgoApiField("dt")]
        public EvalDelta<AppliedSignedTxn> EvalDelta
        {
            get => applyData.EvalDelta;
            set => applyData.EvalDelta = value;
        }

        /// <inheritdoc />
        [AlgoApiField("caid")]
        public AssetIndex ConfigAsset
        {
            get => applyData.ConfigAsset;
            set => applyData.ConfigAsset = value;
        }

        /// <inheritdoc />
        [AlgoApiField("apid")]
        public AppIndex ApplicationId
        {
            get => applyData.ApplicationId;
            set => applyData.ApplicationId = value;
        }

        /// <inheritdoc />
        public TransactionSignature Signature
        {
            get => txn.Signature;
            set => txn.Signature = value;
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

        /// <inheritdoc />
        public StateDelta GlobalDelta
        {
            get => applyData.GlobalDelta;
            set => applyData.GlobalDelta = value;
        }

        /// <inheritdoc />
        public StateDelta[] LocalDeltas
        {
            get => applyData.LocalDeltas;
            set => applyData.LocalDeltas = value;
        }

        /// <inheritdoc />
        public string[] Logs
        {
            get => applyData.Logs;
            set => applyData.Logs = value;
        }

        /// <inheritdoc />
        public AppliedSignedTxn[] InnerTxns
        {
            get => applyData.InnerTxns;
            set => applyData.InnerTxns = value;
        }

        public bool Equals(AppliedSignedTxn other)
        {
            return txn.Equals(other.txn);
        }
    }
}
