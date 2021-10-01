using System;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct PendingTransaction
        : IEquatable<PendingTransaction>
    {
        [AlgoApiKey("application-index", null)]
        public ulong ApplicationIndex;
        [AlgoApiKey("asset-closing-amount", null)]
        public ulong AssetClosingAmount;
        [AlgoApiKey("asset-index", null)]
        public ulong AssetIndex;
        [AlgoApiKey("close-rewards", null)]
        public ulong CloseRewards;
        [AlgoApiKey("closing-amount", null)]
        public ulong ClosingAmount;
        [AlgoApiKey("confirmed-round", null)]
        public ulong ConfirmedRound;
        [AlgoApiKey("global-state-delta", null)]
        public EvalDeltaKeyValue[] GlobalStateDelta;
        [AlgoApiKey("local-state-delta", null)]
        public AccountStateDelta[] LocalStateDelta;
        [AlgoApiKey("pool-error", null)]
        public FixedString128Bytes PoolError;
        [AlgoApiKey("receiver-rewards", null)]
        public ulong ReceiverRewards;
        [AlgoApiKey("sender-rewards", null)]
        public ulong SenderRewards;
        [AlgoApiKey("txn", null)]
        public RawSignedTransaction Transaction;

        public bool Equals(PendingTransaction other)
        {
            return Transaction.Equals(other.Transaction);
        }
    }
}
