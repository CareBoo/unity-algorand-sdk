using System;
using AlgoSdk.Crypto;
using AlgoSdk.LowLevel;
using AlgoSdk.MsgPack;
using MessagePack;
using Unity.Collections;

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
            this ref TTransaction transaction, in Ed25519.SecretKeyHandle secretKey
            )
            where TTransaction : struct, ITransaction, IDisposable
        {
            using var message = transaction.ToMessagePack(Allocator.Temp);
            Signature signature = secretKey.Sign(message);
            return new SignedTransaction<Signature, TTransaction>(in signature, in transaction);
        }

        public static NativeByteArray ToMessagePack<TTransaction>(
            this ref TTransaction transaction,
            Allocator allocator
            )
            where TTransaction : struct, ITransaction, IDisposable
        {
            var rawTransaction = new RawTransaction();
            transaction.CopyToRawTransaction(ref rawTransaction);
            var data = MessagePackSerializer.Serialize(rawTransaction, Config.Options);
            return new NativeByteArray(data, allocator);
        }
    }
}
