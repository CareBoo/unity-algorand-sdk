using System;
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

    public struct SignedTransaction<TTransaction>
        : IDisposable
        , ISignedTransaction
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
            Transaction.CopyFrom(data.Transaction.Value);
        }

        public void CopyTo(ref RawSignedTransaction data)
        {
            data.Sig = Signature;
            var transaction = data.Transaction.Value;
            Transaction.CopyTo(ref transaction);
            data.Transaction = transaction;
        }

        public void Dispose()
        {
            Transaction.Dispose();
        }

        public bool Equals(SignedTransaction<TTransaction> other)
        {
            return Signature.Equals(other.Signature)
                && Transaction.Equals(other.Transaction)
                ;
        }

        public bool Verify()
        {
            using var message = Transaction.ToMessagePack(Allocator.Temp);
            return Signature.Verify(message, Transaction.Header.Sender);
        }
    }
}
