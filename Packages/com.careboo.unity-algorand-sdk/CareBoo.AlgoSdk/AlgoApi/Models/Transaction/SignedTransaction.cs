using System;
using AlgoSdk.Formatters;
using Unity.Collections;

namespace AlgoSdk
{
    public interface ISignedTransaction
    {
        ISignature Signature { get; }
        ITransaction Transaction { get; }
        bool Verify();

        public void CopyTo(ref SignedTransaction data);
        public void CopyFrom(in SignedTransaction data);
    }

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
    }

    [AlgoApiFormatter(typeof(SignedTransactionFormatter<Transaction.Payment>))]
    [AlgoApiFormatter(typeof(SignedTransactionFormatter<Transaction.AssetConfiguration>))]
    [AlgoApiFormatter(typeof(SignedTransactionFormatter<Transaction.AssetFreeze>))]
    [AlgoApiFormatter(typeof(SignedTransactionFormatter<Transaction.AssetTransfer>))]
    [AlgoApiFormatter(typeof(SignedTransactionFormatter<Transaction.ApplicationCall>))]
    [AlgoApiFormatter(typeof(SignedTransactionFormatter<Transaction.KeyRegistration>))]
    public struct SignedTransaction<TTransaction>
        : ISignedTransaction
        , IEquatable<SignedTransaction<TTransaction>>
        where TTransaction : struct, ITransaction, IEquatable<TTransaction>
    {
        public Sig Signature;
        public TTransaction Transaction;

        public SignedTransaction(
            in Sig signature,
            in TTransaction transaction
        )
        {
            Signature = signature;
            Transaction = transaction;
        }

        ISignature ISignedTransaction.Signature => Signature;

        ITransaction ISignedTransaction.Transaction => Transaction;

        public void CopyFrom(in SignedTransaction data)
        {
            Signature = data.Sig;
            Transaction.CopyFrom(data.Transaction);
        }

        public void CopyTo(ref SignedTransaction data)
        {
            data.Sig = Signature;
            Transaction.CopyTo(ref data.Transaction);
        }

        public SignedTransaction ToRaw()
        {
            SignedTransaction result = new SignedTransaction();
            CopyTo(ref result);
            return result;
        }

        public bool Equals(SignedTransaction<TTransaction> other)
        {
            return Signature.Equals(other.Signature)
                && Transaction.Equals(other.Transaction)
                ;
        }

        public bool Verify()
        {
            using var message = Transaction.ToSignatureMessage(Allocator.Temp);
            return Signature.Verify(message, Transaction.Header.Sender);
        }
    }
}
