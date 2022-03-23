using System;
using Unity.Collections;
using UnityEngine;

namespace AlgoSdk
{
    public interface IBlockTransaction : IAppliedSignedTxn<AppliedSignedTxn>
    {
        Optional<bool> HasGenesisId { get; set; }
        Optional<bool> HasGenesisHash { get; set; }
    }

    /// <summary>
    /// A transaction found in a <see cref="BlockResponse"/> from <see cref="IAlgodClient.GetBlock"/>.
    /// </summary>
    /// <remarks>
    /// This is a <c>SignedTxnInBlock</c> from the algorand go project.
    /// </remarks>
    [AlgoApiObject]
    [Serializable]
    public partial struct BlockTransaction
        : IEquatable<BlockTransaction>
        , IBlockTransaction
    {
        [SerializeField]
        AppliedSignedTxn txn;

        [SerializeField]
        Optional<bool> hasGenesisId;

        [SerializeField]
        Optional<bool> hasGenesisHash;

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
            get => txn.ClosingAmount;
            set => txn.ClosingAmount = value;
        }

        [AlgoApiField("aca", "aca")]
        public ulong AssetClosingAmount
        {
            get => txn.AssetClosingAmount;
            set => txn.AssetClosingAmount = value;
        }

        [AlgoApiField("rr", "rr")]
        public MicroAlgos ReceiverRewards
        {
            get => txn.ReceiverRewards;
            set => txn.ReceiverRewards = value;
        }

        [AlgoApiField("rs", "rs")]
        public MicroAlgos SenderRewards
        {
            get => txn.SenderRewards;
            set => txn.SenderRewards = value;
        }

        [AlgoApiField("rc", "rc")]
        public MicroAlgos CloseRewards
        {
            get => txn.CloseRewards;
            set => txn.CloseRewards = value;
        }

        [AlgoApiField("dt", "dt")]
        public AppEvalDelta<AppliedSignedTxn> EvalDelta
        {
            get => txn.EvalDelta;
            set => txn.EvalDelta = value;
        }

        [AlgoApiField("caid", "caid")]
        public AssetIndex ConfigAsset
        {
            get => txn.ConfigAsset;
            set => txn.ConfigAsset = value;
        }

        [AlgoApiField("apid", "apid")]
        public AppIndex ApplicationId
        {
            get => txn.ApplicationId;
            set => txn.ApplicationId = value;
        }

        [AlgoApiField("hgi", "hgi")]
        public Optional<bool> HasGenesisId { get; set; }

        [AlgoApiField("hgh", "hgh")]
        public Optional<bool> HasGenesisHash { get; set; }

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
            get => txn.GlobalDelta;
            set => txn.GlobalDelta = value;
        }

        public AccountStateDelta[] LocalDeltas
        {
            get => txn.LocalDeltas;
            set => txn.LocalDeltas = value;
        }

        public string[] Logs
        {
            get => txn.Logs;
            set => txn.Logs = value;
        }

        public AppliedSignedTxn[] InnerTxns
        {
            get => txn.InnerTxns;
            set => txn.InnerTxns = value;
        }

        public bool Equals(BlockTransaction other)
        {
            return txn.Equals(other.txn);
        }

        public bool Equals(AppliedSignedTxn other)
        {
            return txn.Equals(other);
        }

        public static implicit operator Transaction(BlockTransaction blockTxn)
        {
            return blockTxn.txn.Txn;
        }

        [Obsolete("Use Msig instead")]
        public Multisig Multisig
        {
            get => txn.Msig;
            set => txn.Msig = value;
        }

        [Obsolete("Use Lsig instead")]
        public LogicSig LogicSig
        {
            get => txn.Lsig;
            set => txn.Lsig = value;
        }

        [Obsolete("Use HasGenesisId instead.")]
        public Optional<bool> Hgi { get; set; }

        [Obsolete("Use Txn instead.")]
        public Transaction Transaction
        {
            get => txn.Txn;
            set => txn.Txn = value;
        }

        [Obsolete("Replaced with SenderRewards")]
        public ulong Rs { get => SenderRewards; set => SenderRewards = value; }

        [Obsolete("Replaced with ReceiverRewards")]
        public ulong Rr { get => ReceiverRewards; set => ReceiverRewards = value; }
    }
}
