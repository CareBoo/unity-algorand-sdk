

using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct RawSignedTransaction
        : IEquatable<RawSignedTransaction>
    {
        [AlgoApiField(null, "txn")]
        public RawTransaction Transaction;

        [AlgoApiField(null, "sig")]
        public Signature Sig;

        [AlgoApiField(null, "msig")]
        public MultiSig MultiSig;

        [AlgoApiField(null, "lsig")]
        public LogicSig LogicSig;

        public bool Equals(RawSignedTransaction other)
        {
            return Transaction.Equals(other.Transaction)
                && Sig.Equals(other.Sig)
                && MultiSig.Equals(other.MultiSig)
                && LogicSig.Equals(other.LogicSig)
                ;
        }
    }
}
