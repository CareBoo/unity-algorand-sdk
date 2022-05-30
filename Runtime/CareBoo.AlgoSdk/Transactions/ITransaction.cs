using System;
using AlgoSdk.Crypto;
using AlgoSdk.LowLevel;
using Unity.Collections;
using Unity.Mathematics;

namespace AlgoSdk
{
    public interface ITransaction : ITransactionHeader
    {
        /// <summary>
        /// Copy this transactions fields to a <see cref="Transaction"/> which contains all possible transaction fields.
        /// </summary>
        /// <param name="transaction">A raw transaction with all possible transaction fields.</param>
        void CopyTo(ref Transaction transaction);

        /// <summary>
        /// Copy relevant fields to this transaction.
        /// </summary>
        /// <param name="transaction">A raw transaction with all possible transaction fields.</param>
        void CopyFrom(Transaction transaction);
    }

    public static class TransactionExtensions
    {
        public static byte[] ToSignatureMessage<T>(this T txn)
            where T : ITransaction
        {
            using var messageForSigning = txn.ToSignatureMessage(Allocator.Temp);
            return messageForSigning.ToArray();
        }

        /// <summary>
        /// Serializes this transaction to a message to use for signing.
        /// </summary>
        /// <param name="allocator">How memory should be allocated for the returned byte array.</param>
        /// <returns>A <see cref="NativeByteArray"/></returns>
        public static NativeByteArray ToSignatureMessage<T>(
            this T txn,
            Allocator allocator
            )
            where T : ITransaction
        {
            using var data = AlgoApiSerializer.SerializeMessagePack(txn, Allocator.Temp);
            var result = new NativeByteArray(Transaction.SignaturePrefix.Length + data.Length, allocator);
            for (var i = 0; i < Transaction.SignaturePrefix.Length; i++)
                result[i] = Transaction.SignaturePrefix[i];
            for (var i = 0; i < data.Length; i++)
                result[i + Transaction.SignaturePrefix.Length] = data[i];
            return result;
        }

        public static MicroAlgos GetSuggestedFee<T>(
            this T txn,
            TransactionParams txnParams
            )
            where T : ITransaction, IEquatable<T>
        {
            ulong fee = txnParams.FlatFee ? (ulong)txnParams.Fee : (ulong)txnParams.Fee * (ulong)txn.EstimateBlockSizeBytes();
            return math.max(fee, txnParams.MinFee);
        }

        /// <summary>
        /// Estimate the size this transaction will take up in a block in bytes.
        /// </summary>
        /// <returns>Size in bytes.</returns>
        public static int EstimateBlockSizeBytes<T>(this T txn)
            where T : ITransaction, IEquatable<T>
        {
            var account = Account.GenerateAccount();
            var signedTxn = account.SignTxn(txn);
            return AlgoApiSerializer.SerializeMessagePack(signedTxn).Length;
        }

        /// <summary>
        /// Calculate the ID for this transaction.
        /// </summary>
        /// <returns>A <see cref="TransactionId"/> calculated from its current parameters.</returns>
        public static TransactionId GetId<T>(this T txn)
            where T : ITransaction
        {
            using var txnData = txn.ToSignatureMessage(Allocator.Temp);
            return Sha512.Hash256Truncated(txnData);
        }
    }
}
