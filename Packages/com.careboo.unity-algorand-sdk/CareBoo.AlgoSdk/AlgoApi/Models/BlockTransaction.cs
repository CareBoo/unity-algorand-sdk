using System;
using AlgoSdk.MsgPack;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct BlockTransaction
        : IMessagePackObject
        , IEquatable<BlockTransaction>
    {
        [AlgoApiKey("txn")]
        public RawTransaction Transaction;

        [AlgoApiKey("sig")]
        public Signature Sig;

        [AlgoApiKey("msig")]
        public MultiSig MultiSig;

        [AlgoApiKey("lsig")]
        public LogicSig LogicSig;

        [AlgoApiKey("hgi")]
        public Optional<bool> Hgi;

        [AlgoApiKey("rr")]
        public ulong Rr;

        [AlgoApiKey("rs")]
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
