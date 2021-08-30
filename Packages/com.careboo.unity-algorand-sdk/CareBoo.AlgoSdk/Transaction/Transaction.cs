using System;
using System.Text;
using AlgoSdk.Crypto;
using AlgoSdk.LowLevel;
using AlgoSdk.MsgPack;
using MessagePack;
using Unity.Collections;

namespace AlgoSdk
{
    public interface ITransaction
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
        static readonly byte[] SignaturePrefix = Encoding.UTF8.GetBytes("TX");

        public static SignedTransaction<TTransaction> Sign<TTransaction>(
            this ref TTransaction transaction, in Ed25519.SecretKeyHandle secretKey
            )
            where TTransaction : struct, ITransaction, IEquatable<TTransaction>
        {
            using var message = transaction.ToSignatureMessage(Allocator.Temp);
            Signature signature = secretKey.Sign(message);
            return new SignedTransaction<TTransaction>(in signature, in transaction);
        }

        public static NativeByteArray ToSignatureMessage<TTransaction>(
            this ref TTransaction transaction,
            Allocator allocator
            )
            where TTransaction : struct, ITransaction
        {
            var rawTransaction = new RawTransaction();
            transaction.CopyTo(ref rawTransaction);
            var data = AlgoApiSerializer.SerializeMessagePack(rawTransaction);

            var result = new NativeByteArray(SignaturePrefix.Length + data.Length, allocator);
            for (var i = 0; i < SignaturePrefix.Length; i++)
                result[i] = SignaturePrefix[i];
            for (var i = 0; i < data.Length; i++)
                result[i + SignaturePrefix.Length] = data[i];
            return result;
        }
    }
}
