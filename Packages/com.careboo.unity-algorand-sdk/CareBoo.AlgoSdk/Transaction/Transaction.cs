using System;
using AlgoSdk.Crypto;

namespace AlgoSdk
{
    public interface ITransaction
    {
        Transaction.Header.ReadOnly Header { get; }
    }

    public enum TransactionType : ushort
    {
        None,
        Payment,
        KeyRegistration,
        AssetTransfer,
        AssetFreeze,
        AssetConfiguration
    }

    public static partial class Transaction
    {
        public static SignedTransaction<Signature, TTransaction> Sign<TTransaction>(
            this ref TTransaction transaction, in PrivateKey privateKey
            )
            where TTransaction : unmanaged, ITransaction, IDisposable
        {
            var signature = new Signature();
            return new SignedTransaction<Signature, TTransaction>(in signature, ref transaction);
        }
    }
}
