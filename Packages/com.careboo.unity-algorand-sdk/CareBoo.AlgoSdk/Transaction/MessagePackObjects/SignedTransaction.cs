using System;
using AlgoSdk.Crypto;

namespace AlgoSdk
{
    public readonly ref struct SignedTransaction<TSignature, TTransaction>
        where TSignature : unmanaged, ISignature
        where TTransaction : unmanaged, ITransaction, IDisposable
    {
        public readonly TSignature Signature;
        public readonly TTransaction Transaction;

        public SignedTransaction(
            in TSignature signature,
            ref TTransaction transaction
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
