using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct BlockTransaction
        : IEquatable<BlockTransaction>
    {
        [AlgoApiKey("txn")]
        public RawTransaction Transaction
        {
            get => signedTxn.Transaction;
            set => signedTxn.Transaction = value;
        }

        [AlgoApiKey("sig")]
        public Signature Sig
        {
            get => signedTxn.Sig;
            set => signedTxn.Sig = value;
        }

        [AlgoApiKey("msig")]
        public MultiSig MultiSig
        {
            get => signedTxn.MultiSig;
            set => signedTxn.MultiSig = value;
        }

        [AlgoApiKey("lsig")]
        public LogicSig LogicSig
        {
            get => signedTxn.LogicSig;
            set => signedTxn.LogicSig = value;
        }

        [AlgoApiKey("hgi")]
        public Optional<bool> Hgi;

        [AlgoApiKey("rr")]
        public ulong Rr;

        [AlgoApiKey("rs")]
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
