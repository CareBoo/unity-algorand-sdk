using System;
using UnityEngine;

namespace Algorand.Unity
{
    /// <summary>
    /// Contains information about a transaction's execution
    /// </summary>
    public interface IApplyData<TTxn> : IAppEvalDelta<TTxn>
        where TTxn : IAppliedSignedTxn<TTxn>
    {
        /// <summary>
        /// Closing amount for transaction.
        /// </summary>
        MicroAlgos ClosingAmount { get; set; }

        /// <summary>
        /// Closing amount for asset transaction.
        /// </summary>
        ulong AssetClosingAmount { get; set; }

        /// <summary>
        /// Rewards applied to Sender account.
        /// </summary>
        MicroAlgos SenderRewards { get; set; }

        /// <summary>
        /// Rewards applied to Receiver account.
        /// </summary>
        MicroAlgos ReceiverRewards { get; set; }

        /// <summary>
        /// Rewards applied to CloseRemainderTo account.
        /// </summary>
        MicroAlgos CloseRewards { get; set; }

        /// <summary>
        /// Application global and local state delta
        /// </summary>
        EvalDelta<TTxn> EvalDelta { get; set; }

        /// <summary>
        /// If an asset is configured or created, the id used.
        /// </summary>
        AssetIndex ConfigAsset { get; set; }

        /// <summary>
        /// If an app is called, the id used.
        /// </summary>
        AppIndex ApplicationId { get; set; }
    }

    [Serializable]
    public struct ApplyData<TTxn>
        : IEquatable<ApplyData<TTxn>>
        , IApplyData<TTxn>
        where TTxn : IAppliedSignedTxn<TTxn>
    {
        [SerializeField, Tooltip("Closing amount for transaction.")]
        private MicroAlgos closingAmount;

        [SerializeField, Tooltip("Closing amount for asset transaction.")]
        private ulong assetClosingAmount;

        [SerializeField, Tooltip("Rewards applied to Sender account.")]
        private MicroAlgos senderRewards;

        [SerializeField, Tooltip("Rewards applied to Receiver account.")]
        private MicroAlgos receiverRewards;

        [SerializeField, Tooltip("Rewards applied to CloseRemainderTo account.")]
        private MicroAlgos closeRewards;

        [SerializeField, Tooltip("Application global and local state delta.")]
        private EvalDelta<TTxn> evalDelta;

        [SerializeField, Tooltip("If an asset is configured or created, the id used.")]
        private AssetIndex configAsset;

        [SerializeField, Tooltip("If an app is called, the id used.")]
        private AppIndex applicationId;

        /// <inheritdoc />
        public MicroAlgos ClosingAmount
        {
            get => closingAmount;
            set => closingAmount = value;
        }

        /// <inheritdoc />
        public ulong AssetClosingAmount
        {
            get => assetClosingAmount;
            set => assetClosingAmount = value;
        }

        /// <inheritdoc />
        public MicroAlgos SenderRewards
        {
            get => senderRewards;
            set => senderRewards = value;
        }

        /// <inheritdoc />
        public MicroAlgos ReceiverRewards
        {
            get => receiverRewards;
            set => receiverRewards = value;
        }

        /// <inheritdoc />
        public MicroAlgos CloseRewards
        {
            get => closeRewards;
            set => closeRewards = value;
        }

        /// <inheritdoc />
        public EvalDelta<TTxn> EvalDelta
        {
            get => evalDelta;
            set => evalDelta = value;
        }

        /// <inheritdoc />
        public AssetIndex ConfigAsset
        {
            get => configAsset;
            set => configAsset = value;
        }

        /// <inheritdoc />
        public AppIndex ApplicationId
        {
            get => applicationId;
            set => applicationId = value;
        }

        /// <inheritdoc />
        public StateDelta GlobalDelta
        {
            get => evalDelta.GlobalDelta;
            set => evalDelta.GlobalDelta = value;
        }

        /// <inheritdoc />
        public StateDelta[] LocalDeltas
        {
            get => evalDelta.LocalDeltas;
            set => evalDelta.LocalDeltas = value;
        }

        /// <inheritdoc />
        public string[] Logs
        {
            get => evalDelta.Logs;
            set => evalDelta.Logs = value;
        }

        /// <inheritdoc />
        public TTxn[] InnerTxns
        {
            get => evalDelta.InnerTxns;
            set => evalDelta.InnerTxns = value;
        }

        public bool Equals(ApplyData<TTxn> other)
        {
            return ClosingAmount.Equals(other.ClosingAmount)
                && AssetClosingAmount.Equals(other.AssetClosingAmount)
                && SenderRewards.Equals(other.SenderRewards)
                && ReceiverRewards.Equals(other.ReceiverRewards)
                && CloseRewards.Equals(other.CloseRewards)
                && EvalDelta.Equals(other.EvalDelta)
                && ConfigAsset.Equals(other.ConfigAsset)
                && ApplicationId.Equals(other.ApplicationId)
                ;
        }
    }
}
