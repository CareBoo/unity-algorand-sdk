using System;

namespace AlgoSdk
{
    /// <summary>
    /// Contains information about a transaction's execution
    /// </summary>
    public interface IApplyData
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
        AppEvalDelta EvalDelta { get; set; }

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
    public struct ApplyData
        : IEquatable<ApplyData>
        , IApplyData
    {
        public MicroAlgos ClosingAmount { get; set; }

        public ulong AssetClosingAmount { get; set; }

        public MicroAlgos SenderRewards { get; set; }

        public MicroAlgos ReceiverRewards { get; set; }

        public MicroAlgos CloseRewards { get; set; }

        public AppEvalDelta EvalDelta { get; set; }

        public AssetIndex ConfigAsset { get; set; }

        public AppIndex ApplicationId { get; set; }


        public bool Equals(ApplyData other)
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
