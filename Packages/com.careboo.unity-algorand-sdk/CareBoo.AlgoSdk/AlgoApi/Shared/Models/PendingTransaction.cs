using System;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct PendingTransaction
        : IEquatable<PendingTransaction>
    {
        [AlgoApiField("application-index", "application-index")]
        public ulong ApplicationIndex;

        [AlgoApiField("asset-closing-amount", "asset-closing-amount")]
        public ulong AssetClosingAmount;

        [AlgoApiField("asset-index", "asset-index")]
        public ulong AssetIndex;

        [AlgoApiField("close-rewards", "close-rewards")]
        public ulong CloseRewards;

        [AlgoApiField("closing-amount", "closing-amount")]
        public ulong ClosingAmount;

        [AlgoApiField("confirmed-round", "confirmed-round")]
        public ulong ConfirmedRound;

        [AlgoApiField("global-state-delta", "global-state-delta")]
        public EvalDeltaKeyValue[] GlobalStateDelta;

        [AlgoApiField("local-state-delta", "local-state-delta")]
        public ApplicationStateDelta[] LocalStateDelta;

        [AlgoApiField("pool-error", "pool-error")]
        public FixedString128Bytes PoolError;

        [AlgoApiField("receiver-rewards", "receiver-rewards")]
        public ulong ReceiverRewards;

        [AlgoApiField("sender-rewards", "sender-rewards")]
        public ulong SenderRewards;

        [AlgoApiField("txn", "txn")]
        public SignedTransaction Transaction;

        public bool Equals(PendingTransaction other)
        {
            return Transaction.Equals(other.Transaction);
        }
    }
}
