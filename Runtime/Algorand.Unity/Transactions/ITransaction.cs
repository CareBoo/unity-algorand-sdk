using System;
using Algorand.Unity.Crypto;
using Algorand.Unity.LowLevel;
using Cysharp.Threading.Tasks;
using Unity.Collections;
using Unity.Mathematics;

namespace Algorand.Unity
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
        /// <summary>
        /// Converts a transaction to bytes to prepare it for signing.
        /// </summary>
        /// <param name="txn">The transaction to convert.</param>
        /// <typeparam name="T">The type of the transaction.</typeparam>
        /// <returns>Bytes in the transaction that are used for signing.</returns>
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

        /// <summary>
        /// Suggest a fee given the estimated block size in bytes of this transaction.
        /// </summary>
        /// <param name="txn">the transaction for which to estimate a fee.</param>
        /// <param name="txnParams">The current suggested transaction params from the blockchain.</param>
        /// <typeparam name="T">The type of the transaction.</typeparam>
        /// <returns>A suggested fee in <see cref="MicroAlgos"/>.</returns>
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

        /// <summary>
        /// Sign a transaction with the given signer into raw, signed bytes.
        /// </summary>
        /// <param name="txn">The transaction to sign.</param>
        /// <param name="signer">The signer that will sign the transaction.</param>
        /// <typeparam name="TTxn">The type of the transaction to sign.</typeparam>
        /// <typeparam name="TSigner">The type of the transaction signer.</typeparam>
        /// <returns>Raw message pack bytes, ready to push to the algod service.</returns>
        public static byte[] SignWith<TTxn, TSigner>(this TTxn txn, TSigner signer)
            where TTxn : ITransaction, IEquatable<TTxn>
            where TSigner : ISigner
        {
            var group = new TTxn[] { txn };
            var signed = signer.SignTxns(group, TxnIndices.Select(0))[0];
            return AlgoApiSerializer.SerializeMessagePack(signed);
        }

        /// <summary>
        /// Sign a transaction with the given signer into raw, signed bytes.
        /// </summary>
        /// <param name="txn">The transaction to sign.</param>
        /// <param name="signer">The signer that will sign the transaction.</param>
        /// <typeparam name="TTxn">The type of the transaction to sign.</typeparam>
        /// <typeparam name="TSigner">The type of the transaction signer.</typeparam>
        /// <returns>Raw message pack bytes, ready to push to the algod service.</returns>
        public static async UniTask<byte[]> SignWithAsync<TTxn, TSigner>(this TTxn txn, TSigner signer)
            where TTxn : ITransaction, IEquatable<TTxn>
            where TSigner : IAsyncSigner
        {
            var group = new TTxn[] { txn };
            var signedTxns = await signer.SignTxnsAsync(group, TxnIndices.Select(0));
            return AlgoApiSerializer.SerializeMessagePack(signedTxns[0]);
        }
    }
}
