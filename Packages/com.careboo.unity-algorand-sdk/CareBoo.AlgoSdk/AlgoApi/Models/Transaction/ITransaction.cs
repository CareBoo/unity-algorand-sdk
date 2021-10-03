using System;
using System.Text;
using AlgoSdk.Crypto;
using AlgoSdk.LowLevel;
using Unity.Collections;

namespace AlgoSdk
{
    public interface ITransaction
    {
        Transaction.Header Header { get; }
        void CopyTo(ref Transaction transaction);
        void CopyFrom(Transaction transaction);
    }

    public static class TransactionExtensions
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
            this ref TTransaction source,
            Allocator allocator
            )
            where TTransaction : struct, ITransaction
        {
            var transaction = new Transaction();
            source.CopyTo(ref transaction);
            using var data = new NativeList<byte>(Allocator.Temp);
            AlgoApiSerializer.SerializeMessagePack(transaction, data);

            var result = new NativeByteArray(SignaturePrefix.Length + data.Length, allocator);
            for (var i = 0; i < SignaturePrefix.Length; i++)
                result[i] = SignaturePrefix[i];
            for (var i = 0; i < data.Length; i++)
                result[i + SignaturePrefix.Length] = data[i];
            return result;
        }
    }
}
