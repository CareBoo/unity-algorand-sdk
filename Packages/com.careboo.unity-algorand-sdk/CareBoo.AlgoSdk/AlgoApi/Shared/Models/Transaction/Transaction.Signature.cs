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

        public static implicit operator TransactionSignature(LogicSig lsig)
        {
            return new TransactionSignature { LogicSig = lsig };
        }

        public static implicit operator TransactionSignature(MultiSig msig)
        {
            return new TransactionSignature { MultiSig = msig };
        }

        public static implicit operator TransactionSignature(Sig sig)
        {
            return new TransactionSignature { Sig = sig };
        }
    }
}
