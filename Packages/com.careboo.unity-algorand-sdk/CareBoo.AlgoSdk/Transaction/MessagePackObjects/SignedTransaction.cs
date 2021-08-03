using System;
using AlgoSdk.Crypto;

namespace AlgoSdk
{
    public readonly ref struct SignedTransaction<TTransaction>
        where TTransaction : struct, ITransaction, IDisposable
    {
        public readonly Ed25519.Signature Signature;
        public readonly TTransaction Transaction;

        public SignedTransaction(
            in Ed25519.Signature signature,
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
