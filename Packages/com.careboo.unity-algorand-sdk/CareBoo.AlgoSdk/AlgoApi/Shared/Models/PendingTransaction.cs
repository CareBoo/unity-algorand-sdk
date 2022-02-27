using System;
using Unity.Collections;
using UnityEngine;

namespace AlgoSdk
{
    /// <summary>
    /// Details about a pending transaction. If the transaction was recently confirmed, includes confirmation details like the round and reward details.
    /// </summary>
    [AlgoApiObject]
    [Serializable]
    public partial struct PendingTransaction
        : IEquatable<PendingTransaction>
    {
        /// <summary>
        /// The application index if the transaction was found and it created an application.
        /// </summary>
        [AlgoApiField("application-index", "application-index")]
        [Tooltip("The application index if the transaction was found and it created an application.")]
        public ulong ApplicationIndex;

        /// <summary>
        /// The number of the asset's unit that were transferred to the close-to address.
        /// </summary>
        [AlgoApiField("asset-closing-amount", "asset-closing-amount")]
        [Tooltip("The number of the asset's unit that were transferred to the close-to address.")]
        public ulong AssetClosingAmount;

        /// <summary>
        /// The asset index if the transaction was found and it created an asset.
        /// </summary>
        [AlgoApiField("asset-index", "asset-index")]
        [Tooltip("The asset index if the transaction was found and it created an asset.")]
        public ulong AssetIndex;

        /// <summary>
        /// Rewards in microalgos applied to the close remainder to account.
        /// </summary>
        [AlgoApiField("close-rewards", "close-rewards")]
        [Tooltip("Rewards in microalgos applied to the close remainder to account.")]
        public ulong CloseRewards;

        /// <summary>
        /// Closing amount for the transaction.
        /// </summary>
        [AlgoApiField("closing-amount", "closing-amount")]
        [Tooltip("Closing amount for the transaction.")]
        public ulong ClosingAmount;

        /// <summary>
        /// The round where this transaction was confirmed, if present.
        /// </summary>
        [AlgoApiField("confirmed-round", "confirmed-round")]
        [Tooltip("The round where this transaction was confirmed, if present.")]
        public ulong ConfirmedRound;

        /// <summary>
        /// [gd] Global state key/value changes for the application being executed by this transaction.
        /// </summary>
        [AlgoApiField("global-state-delta", "gd")]
        [Tooltip("Global state key/value changes for the application being executed by this transaction.")]
        public EvalDeltaKeyValue[] GlobalStateDelta;

        /// <summary>
        /// Inner transactions produced by application execution.
        /// </summary>
        [AlgoApiField("inner-txns", "inner-txns")]
        [Tooltip("Inner transactions produced by application execution.")]
        public PendingTransaction[] InnerTransactions;

        /// <summary>
        /// [ld] Local state key/value changes for the application being executed by this transaction.
        /// </summary>
        [AlgoApiField("local-state-delta", "ld")]
        [Tooltip("Local state key/value changes for the application being executed by this transaction.")]
        public AccountStateDelta[] LocalStateDelta;

        /// <summary>
        /// [lg] Logs for the application being executed by this transaction.
        /// </summary>
        [AlgoApiField("logs", "lg")]
        [Tooltip("Logs for the application being executed by this transaction.")]
        public string[] Logs;

        /// <summary>
        /// Indicates that the transaction was kicked out of this node's transaction pool (and specifies why that happened). An empty string indicates the transaction wasn't kicked out of this node's txpool due to an error.
        /// </summary>
        [AlgoApiField("pool-error", "pool-error")]
        [Tooltip("Indicates that the transaction was kicked out of this node's transaction pool (and specifies why that happened). An empty string indicates the transaction wasn't kicked out of this node's txpool due to an error.")]
        public FixedString128Bytes PoolError;

        /// <summary>
        /// Rewards in microalgos applied to the receiver account.
        /// </summary>
        [AlgoApiField("receiver-rewards", "receiver-rewards")]
        [Tooltip("Rewards in microalgos applied to the receiver account.")]
        public ulong ReceiverRewards;

        /// <summary>
        /// Rewards in microalgos applied to the sender account.
        /// </summary>
        [AlgoApiField("sender-rewards", "sender-rewards")]
        [Tooltip("Rewards in microalgos applied to the sender account.")]
        public ulong SenderRewards;

        /// <summary>
        /// The raw signed transaction.
        /// </summary>
        [AlgoApiField("txn", "txn")]
        [Tooltip("The raw signed transaction.")]
        public SignedTransaction Transaction;

        public bool Equals(PendingTransaction other)
        {
            return Transaction.Equals(other.Transaction);
        }
    }
}
