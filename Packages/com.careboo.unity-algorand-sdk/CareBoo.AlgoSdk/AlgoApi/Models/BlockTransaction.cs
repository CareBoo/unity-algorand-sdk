using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct BlockTransaction
        : IEquatable<BlockTransaction>
    {
        [AlgoApiField("txn", "txn")]
        public Transaction Transaction
        {
            get => signedTxn.Transaction;
            set => signedTxn.Transaction = value;
        }

        [AlgoApiField("sig", "sig")]
        public Signature Sig
        {
            get => signedTxn.Sig;
            set => signedTxn.Sig = value;
        }

        [AlgoApiField("msig", "msig")]
        public MultiSig MultiSig
        {
            get => signedTxn.MultiSig;
            set => signedTxn.MultiSig = value;
        }

        [AlgoApiField("lsig", "lsig")]
        public LogicSig LogicSig
        {
            get => signedTxn.LogicSig;
            set => signedTxn.LogicSig = value;
        }

        [AlgoApiField("hgi", "hgi")]
        public Optional<bool> Hgi;

        [AlgoApiField("rr", "rr")]
        public ulong Rr;

        [AlgoApiField("rs", "rs")]
        public ulong Rs;

        RawSignedTransaction signedTxn;

        public bool Equals(BlockTransaction other)
        {
            return signedTxn.Equals(other.signedTxn);
        }

        public static implicit operator RawSignedTransaction(BlockTransaction blockTxn)
        {
            return blockTxn.signedTxn;
        }
    }
}
