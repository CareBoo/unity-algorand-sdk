

using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct RawSignedTransaction
        : IEquatable<RawSignedTransaction>
    {
        [AlgoApiKey(null, "txn")]
        public RawTransaction Transaction;

        [AlgoApiKey(null, "sig")]
        public Signature Sig;

        [AlgoApiKey(null, "msig")]
        public MultiSig MultiSig;

        [AlgoApiKey(null, "lsig")]
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
