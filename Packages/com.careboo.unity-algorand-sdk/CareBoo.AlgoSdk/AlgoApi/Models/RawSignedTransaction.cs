

using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct RawSignedTransaction
        : IEquatable<RawSignedTransaction>
    {
        [AlgoApiKey("txn")]
        public RawTransaction Transaction;
        [AlgoApiKey("sig")]
        public Signature Sig;
        [AlgoApiKey("msig")]
        public MultiSig MultiSig;
        [AlgoApiKey("lsig")]
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
