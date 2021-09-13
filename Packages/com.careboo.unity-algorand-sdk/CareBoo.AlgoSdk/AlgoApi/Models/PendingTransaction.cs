using System;
using AlgoSdk.MsgPack;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct PendingTransaction
        : IMessagePackObject
        , IEquatable<PendingTransaction>
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
            return this.Equals(ref other);
        }
    }
}

namespace AlgoSdk.MsgPack
{
    internal static partial class FieldMaps
    {
        internal static readonly Field<PendingTransaction>.Map pendingTransactionFields =
            new Field<PendingTransaction>.Map()
                .Assign("application-index", (ref PendingTransaction x) => ref x.ApplicationIndex)
                .Assign("asset-closing-amount", (ref PendingTransaction x) => ref x.AssetClosingAmount)
                .Assign("asset-index", (ref PendingTransaction x) => ref x.AssetIndex)
                .Assign("close-rewards", (ref PendingTransaction x) => ref x.CloseRewards)
                .Assign("confirmed-round", (ref PendingTransaction x) => ref x.ConfirmedRound)
                .Assign("global-state-delta", (ref PendingTransaction x) => ref x.GlobalStateDelta, ArrayComparer<EvalDeltaKeyValue>.Instance)
                .Assign("local-state-delta", (ref PendingTransaction x) => ref x.LocalStateDelta, ArrayComparer<AccountStateDelta>.Instance)
                .Assign("pool-error", (ref PendingTransaction x) => ref x.PoolError)
                .Assign("receiver-rewards", (ref PendingTransaction x) => ref x.ReceiverRewards)
                .Assign("sender-rewards", (ref PendingTransaction x) => ref x.SenderRewards)
                .Assign("txn", (ref PendingTransaction x) => ref x.Transaction)
                ;
    }
}
