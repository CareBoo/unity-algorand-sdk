using System;
using UnityEngine;

namespace AlgoSdk
{
    public interface IBlockTransaction : IAppliedSignedTxn
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
    public partial struct BlockTransaction
        : IEquatable<BlockTransaction>
        , IBlockTransaction
    {
        AppliedSignedTxn txn;

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
        public AppEvalDelta EvalDelta
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

        public bool Equals(BlockTransaction other)
        {
            return txn.Equals(other.txn);
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
        [Tooltip("The transaction")]
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
