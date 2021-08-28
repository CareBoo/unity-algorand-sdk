using System;
using AlgoSdk.MsgPack;

namespace AlgoSdk
{
    public struct PendingTransactions
        : IMessagePackObject
        , IEquatable<PendingTransactions>
    {
        public PendingTransaction[] TopTransactions;
        public ulong TotalTransactions;

        public bool Equals(PendingTransactions other)
        {
            return this.Equals(ref other);
        }
    }
}

namespace AlgoSdk.MsgPack
{
    internal static partial class FieldMaps
    {
        internal static readonly Field<PendingTransactions>.Map pendingTransactionsFields =
            new Field<PendingTransactions>.Map()
                .Assign("top-transactions", (ref PendingTransactions x) => ref x.TopTransactions, ArrayComparer<PendingTransaction>.Instance)
                .Assign("total-transactions", (ref PendingTransactions x) => ref x.TotalTransactions)
                ;
    }
}
