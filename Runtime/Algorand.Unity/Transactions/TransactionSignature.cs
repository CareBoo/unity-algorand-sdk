using System;

namespace Algorand.Unity
{
    /// <summary>
    /// An untyped wrapper around different transaction signatures.
    /// </summary>
    [AlgoApiObject]
    [Serializable]
    public partial struct TransactionSignature
        : IEquatable<TransactionSignature>
    {
        [AlgoApiField("logicsig")]
        public LogicSig LogicSig;

        [AlgoApiField("multisig")]
        public MultisigSig Multisig;

        [AlgoApiField("sig")]
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

        public static implicit operator TransactionSignature(MultisigSig msig)
        {
            return new TransactionSignature { Multisig = msig };
        }

        public static implicit operator TransactionSignature(Sig sig)
        {
            return new TransactionSignature { Sig = sig };
        }
    }
}
