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

        public void CopyTo(ref RawSignedTransaction data);
        public void CopyFrom(in RawSignedTransaction data);
    }

    [AlgoApiFormatter(typeof(SignedTransactionFormatter<Transaction.Payment>))]
    [AlgoApiFormatter(typeof(SignedTransactionFormatter<Transaction.AssetConfiguration>))]
    [AlgoApiFormatter(typeof(SignedTransactionFormatter<Transaction.AssetFreeze>))]
    [AlgoApiFormatter(typeof(SignedTransactionFormatter<Transaction.AssetTransfer>))]
    public struct SignedTransaction<TTransaction>
        : ISignedTransaction
        , IEquatable<SignedTransaction<TTransaction>>
        where TTransaction : struct, ITransaction, IEquatable<TTransaction>
    {
        public Signature Signature;
        public TTransaction Transaction;

        public SignedTransaction(
            in Signature signature,
            in TTransaction transaction
        )
        {
            Signature = signature;
            Transaction = transaction;
        }

        ISignature ISignedTransaction.Signature => Signature;

        ITransaction ISignedTransaction.Transaction => Transaction;

        public void CopyFrom(in RawSignedTransaction data)
        {
            Signature = data.Sig;
            Transaction.CopyFrom(data.Transaction);
        }

        public void CopyTo(ref RawSignedTransaction data)
        {
            data.Sig = Signature;
            Transaction.CopyTo(ref data.Transaction);
        }

        public RawSignedTransaction ToRaw()
        {
            RawSignedTransaction result = new RawSignedTransaction();
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
