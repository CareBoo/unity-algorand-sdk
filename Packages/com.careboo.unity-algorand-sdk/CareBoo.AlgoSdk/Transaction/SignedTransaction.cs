using System;

namespace AlgoSdk
{
    public readonly struct SignedTransaction<TSignature, TTransaction>
        where TSignature : struct, ISignature
        where TTransaction : struct, ITransaction, IDisposable
    {
        public readonly TSignature Signature;
        public readonly TTransaction Transaction;

        public SignedTransaction(
            in TSignature signature,
            in TTransaction transaction
        )
        {
            Signature = signature;
            Transaction = transaction;
        }

        public void Dispose()
        {
            Transaction.Dispose();
        }
    }
}
