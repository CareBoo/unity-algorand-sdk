using System;
using System.Text;
using AlgoSdk.Crypto;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    [Serializable]
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
        [Obsolete("Use SignedTxn instead for signatures.")]
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
        /// Sign this transaction with a private key.
        /// </summary>
        /// <param name="secretKey">The account private key to use to sign this transaction.</param>
        /// <returns>A <see cref="SignedTxn"/>.</returns>
        [Obsolete("Use Account.SignTxn instead")]
        public SignedTxn Sign(Ed25519.SecretKeyHandle secretKey)
        {
            return new SignedTxn
            {
                Txn = this,
                Sig = GetSignature(secretKey)
            };
        }

        /// <summary>
        /// Get the signature of this transaction using a private key.
        /// </summary>
        /// <param name="secretKey">The private key to use to sign this transaction.</param>
        /// <returns>A <see cref="Sig"/>.</returns>
        [Obsolete("Use PrivateKey.Sign instead")]
        public Sig GetSignature(Ed25519.SecretKeyHandle secretKey)
        {
            using var message = this.ToSignatureMessage(Allocator.Temp);
            return secretKey.Sign(message);
        }

        /// <summary>
        /// Calculates the group id for atomic transfers.
        /// </summary>
        /// <param name="txns">The transactions belonging to this group. Cannot be more than <see cref="TransactionGroup.MaxSize"/> transactions.</param>
        /// <returns>A <see cref="TransactionId"/></returns>
        [Obsolete("Use TransactionGroup.Of(...).GetId() instead.")]
        public static TransactionId GetGroupId(params Transaction[] txns)
        {
            return TransactionGroup.Of(txns).GetId();
        }

        /// <summary>
        /// Calculates the group id for atomic transfers.
        /// </summary>
        /// <param name="txids">The transaction ids belonging to this group. Cannot be more than <see cref="TransactionGroup.MaxSize"/> transactions.</param>
        /// <returns>A <see cref="TransactionId"/></returns>
        [Obsolete("Use TransactionGroup.Of(...).GetId() instead.")]
        public static TransactionId GetGroupId(params TransactionId[] txids)
        {
            return new TransactionGroup { Txns = txids }.GetId();
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
