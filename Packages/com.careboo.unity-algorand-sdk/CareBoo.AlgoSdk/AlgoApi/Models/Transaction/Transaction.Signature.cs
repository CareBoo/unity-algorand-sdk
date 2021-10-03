using System;

namespace AlgoSdk
{
    public struct TransactionSignature
        : IEquatable<TransactionSignature>
    {
        [AlgoApiField("logicsig", null, readOnly: true)]
        public LogicSig LogicSig;

        [AlgoApiField("multisig", null, readOnly: true)]
        public MultiSig MultiSig;

        [AlgoApiField("sig", null, readOnly: true)]
        public Sig Sig;

        public bool Equals(TransactionSignature other)
        {
            return LogicSig.Equals(other.LogicSig)
                && MultiSig.Equals(other.MultiSig)
                && Sig.Equals(other.Sig)
                ;
        }
    }
}
