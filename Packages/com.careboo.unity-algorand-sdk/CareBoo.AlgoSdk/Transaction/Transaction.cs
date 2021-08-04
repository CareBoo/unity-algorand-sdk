using System;
using AlgoSdk.Crypto;
using AlgoSdk.LowLevel;
using AlgoSdk.MsgPack;
using MessagePack;
using Unity.Collections;

namespace AlgoSdk
{
    public interface ITransaction : IDisposable
    {
        Transaction.Header Header { get; }
        void CopyTo(ref RawTransaction rawTransaction);
        void CopyFrom(in RawTransaction rawTransaction);
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
        public static SignedTransaction<TTransaction> Sign<TTransaction>(
            this ref TTransaction transaction, in Ed25519.SecretKeyHandle secretKey
            )
            where TTransaction : struct, ITransaction, IEquatable<TTransaction>
        {
            using var message = transaction.ToMessagePack(Allocator.Temp);
            Signature signature = secretKey.Sign(message);
            return new SignedTransaction<TTransaction>(in signature, in transaction);
        }

        public static NativeByteArray ToMessagePack<TTransaction>(
            this ref TTransaction transaction,
            Allocator allocator
            )
            where TTransaction : struct, ITransaction, IDisposable
        {
            var rawTransaction = new RawTransaction();
            transaction.CopyTo(ref rawTransaction);
            var data = MessagePackSerializer.Serialize(rawTransaction, Config.Options);
            return new NativeByteArray(data, allocator);
        }
    }
}
