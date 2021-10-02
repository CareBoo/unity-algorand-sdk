using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct BlockTransaction
        : IEquatable<BlockTransaction>
    {
        [AlgoApiField(null, "txn")]
        public RawTransaction Transaction
        {
            get => signedTxn.Transaction;
            set => signedTxn.Transaction = value;
        }

        [AlgoApiField(null, "sig")]
        public Signature Sig
        {
            get => signedTxn.Sig;
            set => signedTxn.Sig = value;
        }

        [AlgoApiField(null, "msig")]
        public MultiSig MultiSig
        {
            get => signedTxn.MultiSig;
            set => signedTxn.MultiSig = value;
        }

        [AlgoApiField(null, "lsig")]
        public LogicSig LogicSig
        {
            get => signedTxn.LogicSig;
            set => signedTxn.LogicSig = value;
        }

        [AlgoApiField(null, "hgi")]
        public Optional<bool> Hgi;

        [AlgoApiField(null, "rr")]
        public ulong Rr;

        [AlgoApiField(null, "rs")]
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
