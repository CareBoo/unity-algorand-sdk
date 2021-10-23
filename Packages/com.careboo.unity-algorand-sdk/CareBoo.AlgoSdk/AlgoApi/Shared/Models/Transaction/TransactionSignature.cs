using System;

namespace AlgoSdk
{
    /// <summary>
    /// An untyped wrapper around different transaction signatures.
    /// </summary>
    [AlgoApiObject]
    public struct TransactionSignature
        : IEquatable<TransactionSignature>
    {
        [AlgoApiField("logicsig", null, readOnly: true)]
        public LogicSig LogicSig;

        [AlgoApiField("multisig", null, readOnly: true)]
        public Multisig Multisig;

        [AlgoApiField("sig", null, readOnly: true)]
        public Sig Sig;

        public bool Equals(TransactionSignature other)
        {
            return LogicSig.Equals(other.LogicSig)
                && Multisig.Equals(other.Multisig)
                && Sig.Equals(other.Sig)
                ;
        }

        public static implicit operator TransactionSignature(LogicSig lsig)
        {
            return new TransactionSignature { LogicSig = lsig };
        }

        public static implicit operator TransactionSignature(Multisig msig)
        {
            return new TransactionSignature { Multisig = msig };
        }

        public static implicit operator TransactionSignature(Sig sig)
        {
            return new TransactionSignature { Sig = sig };
        }
    }
}
