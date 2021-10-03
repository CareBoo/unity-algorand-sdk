using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct BlockTransaction
        : IEquatable<BlockTransaction>
    {
        [AlgoApiField("txn", "txn")]
        public Transaction Transaction;

        [AlgoApiField("sig", "sig")]
        public Sig Sig
        {
            get => Transaction.Signature.Sig;
            set => Transaction.Signature.Sig = value;
        }

        [AlgoApiField("msig", "msig")]
        public MultiSig MultiSig
        {
            get => Transaction.Signature.MultiSig;
            set => Transaction.Signature.MultiSig = value;
        }

        [AlgoApiField("lsig", "lsig")]
        public LogicSig LogicSig
        {
            get => Transaction.Signature.LogicSig;
            set => Transaction.Signature.LogicSig = value;
        }

        [AlgoApiField("hgi", "hgi")]
        public Optional<bool> Hgi;

        [AlgoApiField("rr", "rr")]
        public ulong Rr;

        [AlgoApiField("rs", "rs")]
        public ulong Rs;

        public bool Equals(BlockTransaction other)
        {
            return Transaction.Equals(other.Transaction);
        }

        public static implicit operator Transaction(BlockTransaction blockTxn)
        {
            return blockTxn.Transaction;
        }
    }
}
