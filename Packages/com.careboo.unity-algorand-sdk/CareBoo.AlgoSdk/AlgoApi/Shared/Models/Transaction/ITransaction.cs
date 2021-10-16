using System;
using System.Text;
using AlgoSdk.Crypto;
using AlgoSdk.LowLevel;
using Unity.Collections;
using Unity.Mathematics;

namespace AlgoSdk
{
    public interface ITransaction
    {
        void CopyTo(ref Transaction transaction);
        void CopyFrom(Transaction transaction);
    }

    public static class TransactionExtensions
    {
        public static Signed<T> Sign<T>(
            this T txn,
            Ed25519.SecretKeyHandle secretKey
            )
            where T : struct, ITransaction, IEquatable<T>
        {
            var signature = txn.GetSignature(secretKey);
            return new Signed<T> { Transaction = txn, Signature = signature };
        }

        public static Sig GetSignature<T>(
            this T txn,
            Ed25519.SecretKeyHandle secretKey
            )
            where T : struct, ITransaction, IEquatable<T>
        {
            using var msg = txn.ToSignatureMessage(Allocator.Temp);
            return secretKey.Sign(msg);
        }

        public static NativeByteArray ToSignatureMessage<T>(
            this T txn,
            Allocator allocator
            )
            where T : struct, ITransaction, IEquatable<T>
        {
            using var data = AlgoApiSerializer.SerializeMessagePack(txn, Allocator.Temp);
            var result = new NativeByteArray(Transaction.SignaturePrefix.Length + data.Length, allocator);
            for (var i = 0; i < Transaction.SignaturePrefix.Length; i++)
                result[i] = Transaction.SignaturePrefix[i];
            for (var i = 0; i < data.Length; i++)
                result[i + Transaction.SignaturePrefix.Length] = data[i];
            return result;
        }

        public static ulong GetSuggestedFee<T>(
            this T txn,
            TransactionParams txnParams
            )
            where T : struct, ITransaction, IEquatable<T>
        {
            var fee = txnParams.FlatFee ? txnParams.Fee : txnParams.Fee * (ulong)txn.EstimateBlockSizeBytes();
            return math.max(fee, txnParams.MinFee);
        }

        public static int EstimateBlockSizeBytes<T>(this T txn)
            where T : struct, ITransaction, IEquatable<T>
        {
            var keyPair = AlgoSdk.Crypto.Random.Bytes<Ed25519.Seed>().ToKeyPair();
            var signedTxn = txn.Sign(keyPair.SecretKey);
            using var signedBytes = AlgoApiSerializer.SerializeMessagePack(signedTxn, Allocator.Temp);
            return signedBytes.Length;
        }
    }
}
