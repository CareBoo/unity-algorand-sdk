using System;
using AlgoSdk.MsgPack;

namespace AlgoSdk
{
    public struct BlockTransaction
        : IMessagePackObject
        , IEquatable<BlockTransaction>
    {
        public RawTransaction Transaction;
        public Signature Sig;
        public MultiSig MultiSig;
        public LogicSig LogicSig;
        public Optional<bool> Hgi;
        public ulong Rr;
        public ulong Rs;

        public bool Equals(BlockTransaction other)
        {
            return this.Equals(ref other);
        }

        public static implicit operator RawSignedTransaction(BlockTransaction blockTxn)
        {
            return new RawSignedTransaction()
            {
                Transaction = blockTxn.Transaction,
                Sig = blockTxn.Sig,
                MultiSig = blockTxn.MultiSig,
                LogicSig = blockTxn.LogicSig
            };
        }
    }
}

namespace AlgoSdk.MsgPack
{
    internal static partial class FieldMaps
    {
        private static readonly Field<BlockTransaction>.Map blockTransactionFields =
            new Field<BlockTransaction>.Map()
                .Assign("txn", (ref BlockTransaction r) => ref r.Transaction)
                .Assign("sig", (ref BlockTransaction r) => ref r.Sig)
                .Assign("msig", (ref BlockTransaction r) => ref r.LogicSig)
                .Assign("lsig", (ref BlockTransaction r) => ref r.MultiSig)
                .Assign("hgi", (ref BlockTransaction r) => ref r.Hgi)
                .Assign("rr", (ref BlockTransaction r) => ref r.Rr)
                .Assign("rs", (ref BlockTransaction r) => ref r.Rs)
                ;
    }
}
