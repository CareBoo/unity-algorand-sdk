using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct SignedTransaction
        : IEquatable<SignedTransaction>
    {
        [AlgoApiField("txn", "txn")]
        public Transaction Transaction;

        [AlgoApiField("sig", "sig")]
        public Sig Sig
        {
            get => Transaction.Signature.Sig;
            set => Transaction.Signature.Sig = value;
        }

        [AlgoApiField("msig", "msig")]
        public MultiSig MultiSig
        {
            get => Transaction.Signature.MultiSig;
            set => Transaction.Signature.MultiSig = value;
        }

        [AlgoApiField("lsig", "lsig")]
        public LogicSig LogicSig
        {
            get => Transaction.Signature.LogicSig;
            set => Transaction.Signature.LogicSig = value;
        }

        public bool Equals(SignedTransaction other)
        {
            return Transaction.Equals(other.Transaction)
                && Transaction.Signature.Equals(other.Transaction.Signature)
                ;
        }

        public bool Verify() => Transaction.VerifySignature();

        public static implicit operator SignedTransaction(Transaction txn)
        {
            return new SignedTransaction { Transaction = txn };
        }
    }
}
