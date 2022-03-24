using System;
using System.Text;
using AlgoSdk.Crypto;
using Unity.Collections;

namespace AlgoSdk
{
    public interface IUntypedTransaction : ITransactionHeader
    {
        /// <summary>
        /// Params common to all transactions
        /// </summary>
        TransactionHeader Header { get; set; }

        /// <summary>
        /// Params found in a payment transaction
        /// </summary>
        PaymentTxn.Params PaymentParams { get; set; }

        /// <summary>
        /// Params found in an asset configuration transaction
        /// </summary>
        AssetConfigTxn.Params AssetConfigParams { get; set; }

        /// <summary>
        /// Params found in an asset transfer transaction
        /// </summary>
        AssetTransferTxn.Params AssetTransferParams { get; set; }

        /// <summary>
        /// Params found in an asset freeze transaction
        /// </summary>
        AssetFreezeTxn.Params AssetFreezeParams { get; set; }

        /// <summary>
        /// Params found in an application call transaction
        /// </summary>
        AppCallTxn.Params AppCallParams { get; set; }

        /// <summary>
        /// Params found in a key registration transaction
        /// </summary>
        KeyRegTxn.Params KeyRegParams { get; set; }
    }

    [AlgoApiObject]
    [Serializable]
    public partial struct Transaction
        : IEquatable<Transaction>
        , ITransaction
        , IUntypedTransaction
    {
        /// <summary>
        /// Prefix bytes for signing transaction bytes.
        /// </summary>
        public static readonly byte[] SignaturePrefix = Encoding.UTF8.GetBytes("TX");

        TransactionHeader header;
        PaymentTxn.Params paymentParams;
        AssetConfigTxn.Params assetConfigParams;
        AssetTransferTxn.Params assetTransferParams;
        AssetFreezeTxn.Params assetFreezeParams;
        AppCallTxn.Params appCallParams;
        KeyRegTxn.Params keyRegParams;

        /// <inheritdoc />
        public TransactionHeader Header
        {
            get => header;
            set => header = value;
        }

        /// <inheritdoc />
        [AlgoApiField("payment-transaction", null, readOnly: true)]
        public PaymentTxn.Params PaymentParams
        {
            get => paymentParams;
            set => paymentParams = value;
        }

        /// <inheritdoc />
        [AlgoApiField("asset-config-transaction", null, readOnly: true)]
        public AssetConfigTxn.Params AssetConfigParams
        {
            get => assetConfigParams;
            set => assetConfigParams = value;
        }

        /// <inheritdoc />
        [AlgoApiField("asset-transfer-transaction", null, readOnly: true)]
        public AssetTransferTxn.Params AssetTransferParams
        {
            get => assetTransferParams;
            set => assetTransferParams = value;
        }

        /// <inheritdoc />
        [AlgoApiField("asset-freeze-transaction", null, readOnly: true)]
        public AssetFreezeTxn.Params AssetFreezeParams
        {
            get => assetFreezeParams;
            set => assetFreezeParams = value;
        }

        [Obsolete("Use AppCallParams")]
        public AppCallTxn.Params ApplicationCallParams
        {
            get => appCallParams;
            set => appCallParams = value;
        }

        /// <inheritdoc />
        [AlgoApiField("application-transaction", null, readOnly: true)]
        public AppCallTxn.Params AppCallParams
        {
            get => appCallParams;
            set => appCallParams = value;
        }

        /// <inheritdoc />
        [AlgoApiField("keyreg-transction", null, readOnly: true)]
        public KeyRegTxn.Params KeyRegParams
        {
            get => keyRegParams;
            set => keyRegParams = value;
        }

        /// <inheritdoc />
        [AlgoApiField("signature", null, readOnly: true)]
        public TransactionSignature Signature;

        public bool Equals(Transaction other)
        {
            return Header.Equals(other.Header)
                && Header.TransactionType switch
                {
                    TransactionType.Payment => paymentParams.Equals(other.paymentParams),
                    TransactionType.AssetConfiguration => assetConfigParams.Equals(other.assetConfigParams),
                    TransactionType.ApplicationCall => appCallParams.Equals(other.appCallParams),
                    TransactionType.AssetFreeze => assetFreezeParams.Equals(other.assetFreezeParams),
                    TransactionType.AssetTransfer => assetTransferParams.Equals(other.assetTransferParams),
                    TransactionType.KeyRegistration => keyRegParams.Equals(other.keyRegParams),
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

        /// <inheritdoc />
        public void CopyTo(ref Transaction transaction)
        {
            transaction.Header = Header;
            transaction.paymentParams = paymentParams;
            transaction.keyRegParams = keyRegParams;
            transaction.assetTransferParams = assetTransferParams;
            transaction.assetFreezeParams = assetFreezeParams;
            transaction.assetConfigParams = assetConfigParams;
            transaction.appCallParams = appCallParams;
        }

        /// <inheritdoc />
        public void CopyFrom(Transaction transaction)
        {
            Header = transaction.Header;
            paymentParams = transaction.paymentParams;
            keyRegParams = transaction.keyRegParams;
            assetTransferParams = transaction.assetTransferParams;
            assetFreezeParams = transaction.assetFreezeParams;
            assetConfigParams = transaction.assetConfigParams;
            appCallParams = transaction.appCallParams;
        }

        /// <summary>
        /// Params found in all transactions.
        /// </summary>
        [Obsolete("Use Header")]
        public TransactionHeader HeaderParams
        {
            get => header;
            set => header = value;
        }

        [Obsolete("Use AssetConfigParams")]
        public AssetConfigTxn.Params AssetConfigurationParams
        {
            get => assetConfigParams;
            set => assetConfigParams = value;
        }

        [Obsolete("Use KeyRegParams")]
        public KeyRegTxn.Params KeyRegistrationParams
        {
            get => keyRegParams;
            set => keyRegParams = value;
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
    }
}
