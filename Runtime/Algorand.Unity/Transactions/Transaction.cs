using System;
using System.Diagnostics;
using System.Text;

namespace Algorand.Unity
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

    [AlgoApiObject, Serializable]
    public partial struct Transaction
        : IEquatable<Transaction>
        , ITransaction
        , IUntypedTransaction
    {
        /// <summary>
        /// Prefix bytes for signing transaction bytes.
        /// </summary>
        public static readonly byte[] SignaturePrefix = Encoding.UTF8.GetBytes("TX");

        private TransactionHeader header;
        private PaymentTxn.Params paymentParams;
        private AssetConfigTxn.Params assetConfigParams;
        private AssetTransferTxn.Params assetTransferParams;
        private AssetFreezeTxn.Params assetFreezeParams;
        private AppCallTxn.Params appCallParams;
        private KeyRegTxn.Params keyRegParams;

        /// <inheritdoc />
        public TransactionHeader Header
        {
            get => header;
            set => header = value;
        }

        /// <inheritdoc />
        public PaymentTxn.Params PaymentParams
        {
            get => paymentParams;
            set => paymentParams = value;
        }

        /// <inheritdoc />
        public AssetConfigTxn.Params AssetConfigParams
        {
            get => assetConfigParams;
            set => assetConfigParams = value;
        }

        /// <inheritdoc />
        public AssetTransferTxn.Params AssetTransferParams
        {
            get => assetTransferParams;
            set => assetTransferParams = value;
        }

        /// <inheritdoc />
        public AssetFreezeTxn.Params AssetFreezeParams
        {
            get => assetFreezeParams;
            set => assetFreezeParams = value;
        }

        /// <inheritdoc />
        public AppCallTxn.Params AppCallParams
        {
            get => appCallParams;
            set => appCallParams = value;
        }

        /// <inheritdoc />
        public KeyRegTxn.Params KeyRegParams
        {
            get => keyRegParams;
            set => keyRegParams = value;
        }

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
                };
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

        public override string ToString()
        {
            return this.GetId();
        }
    }
}
