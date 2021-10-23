using System;
using Unity.Collections;

namespace AlgoSdk
{
    /// <summary>
    /// Details about a pending transaction. If the transaction was recently confirmed, includes confirmation details like the round and reward details.
    /// </summary>
    [AlgoApiObject]
    public struct PendingTransaction
        : IEquatable<PendingTransaction>
    {
        /// <summary>
        /// The application index if the transaction was found and it created an application.
        /// </summary>
        [AlgoApiField("application-index", "application-index")]
        public ulong ApplicationIndex;

        /// <summary>
        /// The number of the asset's unit that were transferred to the close-to address.
        /// </summary>
        [AlgoApiField("asset-closing-amount", "asset-closing-amount")]
        public ulong AssetClosingAmount;

        /// <summary>
        /// The asset index if the transaction was found and it created an asset.
        /// </summary>
        [AlgoApiField("asset-index", "asset-index")]
        public ulong AssetIndex;

        /// <summary>
        /// Rewards in microalgos applied to the close remainder to account.
        /// </summary>
        [AlgoApiField("close-rewards", "close-rewards")]
        public ulong CloseRewards;

        /// <summary>
        /// Closing amount for the transaction.
        /// </summary>
        [AlgoApiField("closing-amount", "closing-amount")]
        public ulong ClosingAmount;

        /// <summary>
        /// The round where this transaction was confirmed, if present.
        /// </summary>
        [AlgoApiField("confirmed-round", "confirmed-round")]
        public ulong ConfirmedRound;

        /// <summary>
        /// [gd] Global state key/value changes for the application being executed by this transaction.
        /// </summary>
        [AlgoApiField("global-state-delta", "gd")]
        public EvalDeltaKeyValue[] GlobalStateDelta;

        /// <summary>
        /// Inner transactions produced by application execution.
        /// </summary>
        [AlgoApiField("inner-txns", "inner-txns")]
        public PendingTransaction[] InnerTransactions;

        /// <summary>
        /// [ld] Local state key/value changes for the application being executed by this transaction.
        /// </summary>
        [AlgoApiField("local-state-delta", "ld")]
        public AccountStateDelta[] LocalStateDelta;

        /// <summary>
        /// [lg] Logs for the application being executed by this transaction.
        /// </summary>
        [AlgoApiField("logs", "lg")]
        public string[] Logs;

        /// <summary>
        /// Indicates that the transaction was kicked out of this node's transaction pool (and specifies why that happened). An empty string indicates the transaction wasn't kicked out of this node's txpool due to an error.
        /// </summary>
        [AlgoApiField("pool-error", "pool-error")]
        public FixedString128Bytes PoolError;

        /// <summary>
        /// Rewards in microalgos applied to the receiver account.
        /// </summary>
        [AlgoApiField("receiver-rewards", "receiver-rewards")]
        public ulong ReceiverRewards;

        /// <summary>
        /// Rewards in microalgos applied to the sender account.
        /// </summary>
        [AlgoApiField("sender-rewards", "sender-rewards")]
        public ulong SenderRewards;

        /// <summary>
        /// The raw signed transaction.
        /// </summary>
        [AlgoApiField("txn", "txn")]
        public SignedTransaction Transaction;

        public bool Equals(PendingTransaction other)
        {
            return Transaction.Equals(other.Transaction);
        }
    }
}
