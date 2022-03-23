using System;
using Unity.Collections;
using UnityEngine;

namespace AlgoSdk
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
        [SerializeField]
        SignedTxn txn;

        [SerializeField]
        ApplyData<AppliedSignedTxn> applyData;

        [AlgoApiField("sig", "sig")]
        public Sig Sig
        {
            get => txn.Sig;
            set => txn.Sig = value;
        }

        [AlgoApiField("msig", "msig")]
        public Multisig Msig
        {
            get => txn.Msig;
            set => txn.Msig = value;
        }

        [AlgoApiField("lsig", "lsig")]
        public LogicSig Lsig
        {
            get => txn.Lsig;
            set => txn.Lsig = value;
        }

        [AlgoApiField("txn", "txn")]
        public Transaction Txn
        {
            get => txn.Txn;
            set => txn.Txn = value;
        }

        [AlgoApiField("sgnr", "sgnr")]
        public Address AuthAddr
        {
            get => txn.AuthAddr;
            set => txn.AuthAddr = value;
        }

        [AlgoApiField("ca", "ca")]
        public MicroAlgos ClosingAmount
        {
            get => applyData.ClosingAmount;
            set => applyData.ClosingAmount = value;
        }

        [AlgoApiField("aca", "aca")]
        public ulong AssetClosingAmount
        {
            get => applyData.AssetClosingAmount;
            set => applyData.AssetClosingAmount = value;
        }

        [AlgoApiField("rs", "rs")]
        public MicroAlgos SenderRewards
        {
            get => applyData.SenderRewards;
            set => applyData.SenderRewards = value;
        }

        [AlgoApiField("rr", "rr")]
        public MicroAlgos ReceiverRewards
        {
            get => applyData.ReceiverRewards;
            set => applyData.ReceiverRewards = value;
        }

        [AlgoApiField("rc", "rc")]
        public MicroAlgos CloseRewards
        {
            get => applyData.CloseRewards;
            set => applyData.CloseRewards = value;
        }

        [AlgoApiField("dt", "dt")]
        public AppEvalDelta<AppliedSignedTxn> EvalDelta
        {
            get => applyData.EvalDelta;
            set => applyData.EvalDelta = value;
        }

        [AlgoApiField("caid", "caid")]
        public AssetIndex ConfigAsset
        {
            get => applyData.ConfigAsset;
            set => applyData.ConfigAsset = value;
        }

        [AlgoApiField("apid", "apid")]
        public AppIndex ApplicationId
        {
            get => applyData.ApplicationId;
            set => applyData.ApplicationId = value;
        }

        public TransactionSignature Signature
        {
            get => txn.Signature;
            set => txn.Signature = value;
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

        public AppStateDelta GlobalDelta
        {
            get => applyData.GlobalDelta;
            set => applyData.GlobalDelta = value;
        }

        public AccountStateDelta[] LocalDeltas
        {
            get => applyData.LocalDeltas;
            set => applyData.LocalDeltas = value;
        }

        public string[] Logs
        {
            get => applyData.Logs;
            set => applyData.Logs = value;
        }

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
