using System;

namespace AlgoSdk
{
    public interface IAppliedSignedTxn
        : ISignedTxn<Transaction>
        , IApplyData
    { }

    /// <summary>
    /// A signed transaction that has already been executed.
    /// </summary>
    [AlgoApiObject]
    public partial struct AppliedSignedTxn
        : IEquatable<AppliedSignedTxn>
        , ISignedTxn<Transaction>
        , IApplyData
    {
        SignedTransaction signedTxn;

        ApplyData applyData;

        [AlgoApiField("sig", "sig")]
        public Sig Sig
        {
            get => signedTxn.Sig;
            set => signedTxn.Sig = value;
        }

        [AlgoApiField("msig", "msig")]
        public Multisig Msig
        {
            get => signedTxn.Msig;
            set => signedTxn.Msig = value;
        }

        [AlgoApiField("lsig", "lsig")]
        public LogicSig Lsig
        {
            get => signedTxn.Lsig;
            set => signedTxn.Lsig = value;
        }

        [AlgoApiField("txn", "txn")]
        public Transaction Txn
        {
            get => signedTxn.Transaction;
            set => signedTxn.Transaction = value;
        }

        [AlgoApiField("sgnr", "sgnr")]
        public Address AuthAddr
        {
            get => signedTxn.AuthAddr;
            set => signedTxn.AuthAddr = value;
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
        public AppEvalDelta EvalDelta
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

        public bool Equals(AppliedSignedTxn other)
        {
            return signedTxn.Equals(other.signedTxn);
        }
    }
}
