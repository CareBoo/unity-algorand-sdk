using System;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct PendingTransaction
        : IEquatable<PendingTransaction>
    {
        [AlgoApiKey("application-index")]
        public ulong ApplicationIndex;
        [AlgoApiKey("asset-closing-amount")]
        public ulong AssetClosingAmount;
        [AlgoApiKey("asset-index")]
        public ulong AssetIndex;
        [AlgoApiKey("close-rewards")]
        public ulong CloseRewards;
        [AlgoApiKey("closing-amount")]
        public ulong ClosingAmount;
        [AlgoApiKey("confirmed-round")]
        public ulong ConfirmedRound;
        [AlgoApiKey("global-state-delta")]
        public EvalDeltaKeyValue[] GlobalStateDelta;
        [AlgoApiKey("local-state-delta")]
        public AccountStateDelta[] LocalStateDelta;
        [AlgoApiKey("pool-error")]
        public FixedString128Bytes PoolError;
        [AlgoApiKey("receiver-rewards")]
        public ulong ReceiverRewards;
        [AlgoApiKey("sender-rewards")]
        public ulong SenderRewards;
        [AlgoApiKey("txn")]
        public RawSignedTransaction Transaction;

        public bool Equals(PendingTransaction other)
        {
            return Transaction.Equals(other.Transaction);
        }
    }
}
