using System;
using System.Text;
using AlgoSdk.Crypto;
using AlgoSdk.LowLevel;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public partial struct Transaction
        : IEquatable<Transaction>
        , ITransaction
    {
        /// <summary>
        /// Prefix bytes for signing transaction bytes.
        /// </summary>
        public static readonly byte[] SignaturePrefix = Encoding.UTF8.GetBytes("TX");

        /// <summary>
        /// Params found in all transactions.
        /// </summary>
        public TransactionHeader HeaderParams;

        /// <summary>
        /// Params found in Payment Transactions.
        /// </summary>
        [AlgoApiField("payment-transaction", null, readOnly: true)]
        public PaymentTxn.Params PaymentParams;

        /// <summary>
        /// Params found in Asset Configuration Transactions.
        /// </summary>
        [AlgoApiField("asset-config-transaction", null, readOnly: true)]
        public AssetConfigTxn.Params AssetConfigurationParams;

        /// <summary>
        /// Params found in Asset Transfer Transactions.
        /// </summary>
        [AlgoApiField("asset-transfer-transaction", null, readOnly: true)]
        public AssetTransferTxn.Params AssetTransferParams;

        /// <summary>
        /// Params found in Asset Freeze Transactions.
        /// </summary>
        [AlgoApiField("asset-freeze-transaction", null, readOnly: true)]
        public AssetFreezeTxn.Params AssetFreezeParams;

        /// <summary>
        /// Params found in Application Call Transactions.
        /// </summary>
        [AlgoApiField("application-transaction", null, readOnly: true)]
        public AppCallTxn.Params ApplicationCallParams;

        /// <summary>
        /// Params found in Key Registration Transactions.
        /// </summary>
        [AlgoApiField("keyreg-transaction", null, readOnly: true)]
        public KeyRegTxn.Params KeyRegistrationParams;

        /// <summary>
        /// The signature used for this Transaction.
        /// </summary>
        [AlgoApiField("signature", null, readOnly: true)]
        public TransactionSignature Signature;

        public bool Equals(Transaction other)
        {
            return HeaderParams.Equals(other.HeaderParams)
                && HeaderParams.TransactionType switch
                {
                    TransactionType.Payment => PaymentParams.Equals(other.PaymentParams),
                    TransactionType.AssetConfiguration => AssetConfigurationParams.Equals(other.AssetConfigurationParams),
                    TransactionType.ApplicationCall => ApplicationCallParams.Equals(other.ApplicationCallParams),
                    TransactionType.AssetFreeze => AssetFreezeParams.Equals(other.AssetFreezeParams),
                    TransactionType.AssetTransfer => AssetTransferParams.Equals(other.AssetTransferParams),
                    TransactionType.KeyRegistration => KeyRegistrationParams.Equals(other.KeyRegistrationParams),
                    _ => true
                }
                && Signature.Equals(other.Signature)
                ;
        }

        /// <summary>
        /// Converts this transaction to a specific transaction type implementing <see cref="ITransaction"/>.
        /// </summary>
        /// <typeparam name="T">The type of the transaction to convert to.</typeparam>
        /// <returns>A typed transaction implementing <see cref="ITransaction"/>.</returns>
        public T ToTypedTxn<T>()
            where T : struct, ITransaction
        {
            T result = default;
            result.CopyFrom(this);
            return result;
        }

        /// <summary>
        /// Estimate the size this transaction will take up in a block in bytes.
        /// </summary>
        /// <returns>Size in bytes.</returns>
        public int EstimateBlockSizeBytes()
        {
            var rndSeed = AlgoSdk.Crypto.Random.Bytes<Ed25519.Seed>();
            using var keyPair = rndSeed.ToKeyPair();
            var signedTxn = Sign(keyPair.SecretKey);
            return AlgoApiSerializer.SerializeMessagePack(signedTxn).Length;
        }

        /// <summary>
        /// Sign this transaction with a private key.
        /// </summary>
        /// <param name="secretKey">The account private key to use to sign this transaction.</param>
        /// <returns>A <see cref="SignedTransaction"/>.</returns>
        public SignedTransaction Sign(Ed25519.SecretKeyHandle secretKey)
        {
            Signature = GetSignature(secretKey);
            return new SignedTransaction { Transaction = this };
        }

        /// <summary>
        /// Get the signature of this transaction using a private key.
        /// </summary>
        /// <param name="secretKey">The private key to use to sign this transaction.</param>
        /// <returns>A <see cref="Sig"/>.</returns>
        public Sig GetSignature(Ed25519.SecretKeyHandle secretKey)
        {
            using var message = ToSignatureMessage(Allocator.Temp);
            return secretKey.Sign(message);
        }

        /// <summary>
        /// Serializes this transaction to a message to use for signing.
        /// </summary>
        /// <param name="allocator">How memory should be allocated for the returned byte array.</param>
        /// <returns>A <see cref="NativeByteArray"/></returns>
        public NativeByteArray ToSignatureMessage(Allocator allocator)
        {
            using var data = AlgoApiSerializer.SerializeMessagePack(this, Allocator.Temp);
            var result = new NativeByteArray(SignaturePrefix.Length + data.Length, allocator);
            for (var i = 0; i < SignaturePrefix.Length; i++)
                result[i] = SignaturePrefix[i];
            for (var i = 0; i < data.Length; i++)
                result[i + SignaturePrefix.Length] = data[i];
            return result;
        }

        /// <summary>
        /// Calculate the ID for this transaction.
        /// </summary>
        /// <returns>A <see cref="TransactionId"/> calculated from its current parameters.</returns>
        public TransactionId GetId()
        {
            using var txnData = ToSignatureMessage(Allocator.Temp);
            return Sha512.Hash256Truncated(txnData);
        }

        /// <summary>
        /// Calculates the group id for atomic transfers.
        /// </summary>
        /// <param name="txns">The transactions belonging to this group. Cannot be more than <see cref="TransactionGroup.MaxSize"/> transactions.</param>
        /// <returns>A <see cref="TransactionId"/></returns>
        public static TransactionId GetGroupId(params Transaction[] txns)
        {
            if (txns == null || txns.Length == 0)
                throw new ArgumentException("Cannot get the group id of 0 transactions", nameof(txns));
            if (txns.Length > TransactionGroup.MaxSize)
                throw new ArgumentException($"Cannot get the group id of a group of more than {TransactionGroup.MaxSize} transactions", nameof(txns));
            for (var i = 0; i < txns.Length; i++)
                txns[i].Group = default;
            var txnMsgs = new TransactionId[txns.Length];
            for (var i = 0; i < txns.Length; i++)
            {
                txns[i].Group = default;
                txnMsgs[i] = txns[i].GetId();
            }
            return GetGroupId(txnMsgs);
        }

        /// <summary>
        /// Calculates the group id for atomic transfers.
        /// </summary>
        /// <param name="txids">The transaction ids belonging to this group. Cannot be more than <see cref="TransactionGroup.MaxSize"/> transactions.</param>
        /// <returns>A <see cref="TransactionId"/></returns>
        public static TransactionId GetGroupId(params TransactionId[] txids)
        {
            if (txids == null || txids.Length == 0)
                throw new ArgumentException("Cannot get the group id of 0 transactions", nameof(txids));
            if (txids.Length > TransactionGroup.MaxSize)
                throw new ArgumentException($"Cannot get the group id of a group of more than {TransactionGroup.MaxSize} transactions", nameof(txids));
            var group = new TransactionGroup { Txns = txids };
            using var msgpack = AlgoApiSerializer.SerializeMessagePack(group, Allocator.Temp);
            var data = new NativeByteArray(TransactionGroup.IdPrefix.Length + msgpack.Length, Allocator.Temp);
            try
            {
                data.CopyFrom(TransactionGroup.IdPrefix, 0);
                data.CopyFrom(msgpack.AsArray(), TransactionGroup.IdPrefix.Length);
                return Sha512.Hash256Truncated(data);
            }
            finally
            {
                data.Dispose();
            }
        }

        public void CopyTo(ref Transaction transaction)
        {
            transaction.HeaderParams = HeaderParams;
            transaction.PaymentParams = PaymentParams;
            transaction.KeyRegistrationParams = KeyRegistrationParams;
            transaction.AssetTransferParams = AssetTransferParams;
            transaction.AssetFreezeParams = AssetFreezeParams;
            transaction.AssetConfigurationParams = AssetConfigurationParams;
            transaction.ApplicationCallParams = ApplicationCallParams;
        }

        public void CopyFrom(Transaction transaction)
        {
            HeaderParams = transaction.HeaderParams;
            PaymentParams = transaction.PaymentParams;
            KeyRegistrationParams = transaction.KeyRegistrationParams;
            AssetTransferParams = transaction.AssetTransferParams;
            AssetFreezeParams = transaction.AssetFreezeParams;
            AssetConfigurationParams = transaction.AssetConfigurationParams;
            ApplicationCallParams = transaction.ApplicationCallParams;
        }
    }
}
