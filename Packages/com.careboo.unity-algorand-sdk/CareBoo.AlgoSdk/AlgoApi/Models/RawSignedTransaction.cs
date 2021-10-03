

using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct RawSignedTransaction
        : IEquatable<RawSignedTransaction>
    {
        [AlgoApiField("txn", "txn")]
        public RawTransaction Transaction;

        [AlgoApiField("sig", "sig")]
        public Signature Sig;

        [AlgoApiField("msig", "msig")]
        public MultiSig MultiSig;

        [AlgoApiField("lsig", "lsig")]
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
