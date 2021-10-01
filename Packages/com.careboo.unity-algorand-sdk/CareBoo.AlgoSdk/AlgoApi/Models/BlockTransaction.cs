using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct BlockTransaction
        : IEquatable<BlockTransaction>
    {
        [AlgoApiKey(null, "txn")]
        public RawTransaction Transaction
        {
            get => signedTxn.Transaction;
            set => signedTxn.Transaction = value;
        }

        [AlgoApiKey(null, "sig")]
        public Signature Sig
        {
            get => signedTxn.Sig;
            set => signedTxn.Sig = value;
        }

        [AlgoApiKey(null, "msig")]
        public MultiSig MultiSig
        {
            get => signedTxn.MultiSig;
            set => signedTxn.MultiSig = value;
        }

        [AlgoApiKey(null, "lsig")]
        public LogicSig LogicSig
        {
            get => signedTxn.LogicSig;
            set => signedTxn.LogicSig = value;
        }

        [AlgoApiKey(null, "hgi")]
        public Optional<bool> Hgi;

        [AlgoApiKey(null, "rr")]
        public ulong Rr;

        [AlgoApiKey(null, "rs")]
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
