using System;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct PendingTransaction
        : IEquatable<PendingTransaction>
    {
        [AlgoApiField("application-index", null)]
        public ulong ApplicationIndex;
        [AlgoApiField("asset-closing-amount", null)]
        public ulong AssetClosingAmount;
        [AlgoApiField("asset-index", null)]
        public ulong AssetIndex;
        [AlgoApiField("close-rewards", null)]
        public ulong CloseRewards;
        [AlgoApiField("closing-amount", null)]
        public ulong ClosingAmount;
        [AlgoApiField("confirmed-round", null)]
        public ulong ConfirmedRound;
        [AlgoApiField("global-state-delta", null)]
        public EvalDeltaKeyValue[] GlobalStateDelta;
        [AlgoApiField("local-state-delta", null)]
        public AccountStateDelta[] LocalStateDelta;
        [AlgoApiField("pool-error", null)]
        public FixedString128Bytes PoolError;
        [AlgoApiField("receiver-rewards", null)]
        public ulong ReceiverRewards;
        [AlgoApiField("sender-rewards", null)]
        public ulong SenderRewards;
        [AlgoApiField("txn", null)]
        public RawSignedTransaction Transaction;

        public bool Equals(PendingTransaction other)
        {
            return Transaction.Equals(other.Transaction);
        }
    }
}
