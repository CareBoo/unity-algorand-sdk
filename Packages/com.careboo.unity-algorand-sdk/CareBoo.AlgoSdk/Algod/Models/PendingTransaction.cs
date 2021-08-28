using System;
using AlgoSdk.MsgPack;
using Unity.Collections;

namespace AlgoSdk
{
    public struct PendingTransaction
        : IMessagePackObject
        , IEquatable<PendingTransaction>
    {
        public ulong ApplicationIndex;
        public ulong AssetClosingAmount;
        public ulong AssetIndex;
        public ulong CloseRewards;
        public ulong ClosingAmount;
        public ulong ConfirmedRound;
        public StateDelta GlobalStateDelta;
        public AccountStateDelta[] LocalStateDelta;
        public FixedString128Bytes PoolError;
        public ulong ReceiverRewards;
        public ulong SenderRewards;
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
                .Assign("global-state-delta", (ref PendingTransaction x) => ref x.GlobalStateDelta)
                .Assign("local-state-delta", (ref PendingTransaction x) => ref x.LocalStateDelta, ArrayComparer<AccountStateDelta>.Instance)
                .Assign("pool-error", (ref PendingTransaction x) => ref x.PoolError)
                .Assign("receiver-rewards", (ref PendingTransaction x) => ref x.ReceiverRewards)
                .Assign("sender-rewards", (ref PendingTransaction x) => ref x.SenderRewards)
                .Assign("txn", (ref PendingTransaction x) => ref x.Transaction)
                ;
    }
}
