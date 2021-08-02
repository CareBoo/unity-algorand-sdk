using System;
using AlgoSdk.Crypto;
using AlgoSdk.MsgPack;

namespace AlgoSdk
{
    public interface ITransaction
    {
        Transaction.Header GetHeader();
        void CopyToRawTransaction(ref RawTransaction rawTransaction);
        void CopyFromRawTransaction(in RawTransaction rawTransaction);
    }

    public enum TransactionType : ushort
    {
        None,
        Payment,
        KeyRegistration,
        AssetTransfer,
        AssetFreeze,
        AssetConfiguration,
        ApplicationCall,
        Count,
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

        public static ITransaction GetTypedTransaction(this in Header header)
        {
            return null;
        }
    }
}
