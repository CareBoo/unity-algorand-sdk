using System;
using Unity.Collections;
using UnityEngine;

namespace Algorand.Unity
{
    public interface IAssetTransferTxn : ITransaction
    {
        /// <summary>
        /// The unique ID of the asset to be transferred.
        /// </summary>
        AssetIndex XferAsset { get; set; }

        /// <summary>
        /// The amount of the asset to be transferred. A zero amount transferred to self allocates that asset in the account's Asset map.
        /// </summary>
        ulong AssetAmount { get; set; }

        /// <summary>
        /// The sender of the transfer. The regular <see cref="ITransaction.Sender"/> field should be used and this one set to the zero value for regular transfers between accounts. If this value is nonzero, it indicates a clawback transaction where the sender is the asset's clawback address and the asset sender is the address from which the funds will be withdrawn.
        /// </summary>
        Address AssetSender { get; set; }

        /// <summary>
        /// The recipient of the asset transfer.
        /// </summary>
        Address AssetReceiver { get; set; }

        /// <summary>
        /// Specify this field to remove the asset holding from the sender account and reduce the account's minimum balance (i.e. opt-out of the asset).
        /// </summary>
        Address AssetCloseTo { get; set; }
    }

    public partial struct Transaction : IAssetTransferTxn
    {
        /// <inheritdoc />
        [AlgoApiField("xaid")]
        public AssetIndex XferAsset
        {
            get => assetTransferParams.XferAsset;
            set => assetTransferParams.XferAsset = value;
        }

        /// <inheritdoc />
        [AlgoApiField("aamt")]
        public ulong AssetAmount
        {
            get => assetTransferParams.AssetAmount;
            set => assetTransferParams.AssetAmount = value;
        }

        /// <inheritdoc />
        [AlgoApiField("asnd")]
        public Address AssetSender
        {
            get => assetTransferParams.AssetSender;
            set => assetTransferParams.AssetSender = value;
        }

        /// <inheritdoc />
        [AlgoApiField("arcv")]
        public Address AssetReceiver
        {
            get => assetTransferParams.AssetReceiver;
            set => assetTransferParams.AssetReceiver = value;
        }

        /// <inheritdoc />
        [AlgoApiField("aclose")]
        public Address AssetCloseTo
        {
            get => assetTransferParams.AssetCloseTo;
            set => assetTransferParams.AssetCloseTo = value;
        }

        /// <summary>
        /// Create an <see cref="AssetTransferTxn"/> for transferring an asset to another account.
        /// </summary>
        /// <param name="sender">The address of the account that pays the fee and sends the asset amount.</param>
        /// <param name="txnParams">See <see cref="TransactionParams"/></param>
        /// <param name="xferAsset">The unique ID of the asset to be transferred.</param>
        /// <param name="assetAmount">The amount of the asset to be transferred. A zero amount transferred to self allocates that asset in the account's Asset map.</param>
        /// <param name="assetReceiver"></param>
        /// <returns>An <see cref="AssetTransferTxn"/> for transferring an asset to another account.</returns>
        public static AssetTransferTxn AssetTransfer(
            Address sender,
            TransactionParams txnParams,
            AssetIndex xferAsset,
            ulong assetAmount,
            Address assetReceiver
        )
        {
            var txn = new AssetTransferTxn
            {
                header = new TransactionHeader(sender, TransactionType.AssetTransfer, txnParams),
                XferAsset = xferAsset,
                AssetAmount = assetAmount,
                AssetReceiver = assetReceiver
            };
            txn.Fee = txn.GetSuggestedFee(txnParams);
            return txn;
        }

        /// <summary>
        /// Create an <see cref="AssetTransferTxn"/> for opting in to an asset.
        /// </summary>
        /// <param name="sender">The address of the account that pays the fee and amount.</param>
        /// <param name="txnParams">See <see cref="TransactionParams"/></param>
        /// <param name="xferAsset">The unique ID of the asset to opt-in to.</param>
        /// <returns>An <see cref="AssetTransferTxn"/> for opting in to an asset.</returns>
        public static AssetTransferTxn AssetAccept(
            Address sender,
            TransactionParams txnParams,
            AssetIndex xferAsset
        )
        {
            var txn = new AssetTransferTxn
            {
                header = new TransactionHeader(sender, TransactionType.AssetTransfer, txnParams),
                XferAsset = xferAsset,
                AssetReceiver = sender,
            };
            txn.Fee = txn.GetSuggestedFee(txnParams);
            return txn;
        }

        /// <summary>
        /// Creates a form of <see cref="AssetTransferTxn"/> to clawback assets from an account.
        /// </summary>
        /// <param name="sender">The sender of this transaction must be the clawback account specified in the asset configuration.</param>
        /// <param name="txnParams">See <see cref="TransactionParams"/></param>
        /// <param name="xferAsset">The unique ID of the asset to be transferred.</param>
        /// <param name="assetAmount">The amount of the asset to be transferred.</param>
        /// <param name="assetSender">The address from which the funds will be withdrawn.</param>
        /// <param name="assetReceiver">The recipient of the asset transfer.</param>
        /// <param name="assetCloseTo">Specify this field to remove the entire asset holding balance from the AssetSender account. It will not remove the asset holding.</param>
        /// <returns>A form of <see cref="AssetTransferTxn"/> to clawback assets from an account</returns>
        public static AssetTransferTxn AssetClawback(
            Address sender,
            TransactionParams txnParams,
            AssetIndex xferAsset,
            ulong assetAmount,
            Address assetSender,
            Address assetReceiver
        )
        {
            var txn = new AssetTransferTxn
            {
                header = new TransactionHeader(sender, TransactionType.AssetTransfer, txnParams),
                XferAsset = xferAsset,
                AssetAmount = assetAmount,
                AssetSender = assetSender,
                AssetReceiver = assetReceiver,
            };
            txn.Fee = txn.GetSuggestedFee(txnParams);
            return txn;
        }
    }

    [AlgoApiObject]
    [Serializable]
    public partial struct AssetTransferTxn
        : IAssetTransferTxn
        , IEquatable<AssetTransferTxn>
    {
        [SerializeField]
        internal TransactionHeader header;

        [SerializeField] private Params @params;

        /// <inheritdoc />
        [AlgoApiField("fee")]
        public MicroAlgos Fee
        {
            get => header.Fee;
            set => header.Fee = value;
        }

        /// <inheritdoc />
        [AlgoApiField("fv")]
        public ulong FirstValidRound
        {
            get => header.FirstValidRound;
            set => header.FirstValidRound = value;
        }

        /// <inheritdoc />
        [AlgoApiField("gh")]
        public GenesisHash GenesisHash
        {
            get => header.GenesisHash;
            set => header.GenesisHash = value;
        }

        /// <inheritdoc />
        [AlgoApiField("lv")]
        public ulong LastValidRound
        {
            get => header.LastValidRound;
            set => header.LastValidRound = value;
        }

        /// <inheritdoc />
        [AlgoApiField("snd")]
        public Address Sender
        {
            get => header.Sender;
            set => header.Sender = value;
        }

        /// <inheritdoc />
        [AlgoApiField("type")]
        public TransactionType TransactionType
        {
            get => TransactionType.AssetTransfer;
            internal set => header.TransactionType = TransactionType.AssetTransfer;
        }

        /// <inheritdoc />
        [AlgoApiField("gen")]
        public FixedString32Bytes GenesisId
        {
            get => header.GenesisId;
            set => header.GenesisId = value;
        }

        /// <inheritdoc />
        [AlgoApiField("grp")]
        public TransactionId Group
        {
            get => header.Group;
            set => header.Group = value;
        }

        /// <inheritdoc />
        [AlgoApiField("lx")]
        public TransactionId Lease
        {
            get => header.Lease;
            set => header.Lease = value;
        }

        /// <inheritdoc />
        [AlgoApiField("note")]
        public byte[] Note
        {
            get => header.Note;
            set => header.Note = value;
        }

        /// <inheritdoc />
        [AlgoApiField("rekey")]
        public Address RekeyTo
        {
            get => header.RekeyTo;
            set => header.RekeyTo = value;
        }

        /// <inheritdoc />
        [AlgoApiField("xaid")]
        public AssetIndex XferAsset
        {
            get => @params.XferAsset;
            set => @params.XferAsset = value;
        }

        /// <inheritdoc />
        [AlgoApiField("aamt")]
        public ulong AssetAmount
        {
            get => @params.AssetAmount;
            set => @params.AssetAmount = value;
        }

        /// <inheritdoc />
        [AlgoApiField("asnd")]
        public Address AssetSender
        {
            get => @params.AssetSender;
            set => @params.AssetSender = value;
        }

        /// <inheritdoc />
        [AlgoApiField("arcv")]
        public Address AssetReceiver
        {
            get => @params.AssetReceiver;
            set => @params.AssetReceiver = value;
        }

        /// <inheritdoc />
        [AlgoApiField("aclose")]
        public Address AssetCloseTo
        {
            get => @params.AssetCloseTo;
            set => @params.AssetCloseTo = value;
        }

        /// <inheritdoc />
        public void CopyTo(ref Transaction transaction)
        {
            transaction.Header = header;
            transaction.AssetTransferParams = @params;
        }

        /// <inheritdoc />
        public void CopyFrom(Transaction transaction)
        {
            header = transaction.Header;
            @params = transaction.AssetTransferParams;
        }

        public bool Equals(AssetTransferTxn other)
        {
            return header.Equals(other.header)
                && @params.Equals(other.@params)
                ;
        }

        [AlgoApiObject]
        public partial struct Params
            : IEquatable<Params>
        {
            [AlgoApiField("xaid")]
            [Tooltip("The unique ID of the asset to be transferred.")]
            public AssetIndex XferAsset;

            [AlgoApiField("aamt")]
            [Tooltip("The amount of the asset to be transferred. A zero amount transferred to self allocates that asset in the account's Asset map.")]
            public ulong AssetAmount;

            [AlgoApiField("asnd")]
            [Tooltip("The sender of the transfer. The regular Sender field should be used and this one set to the zero value for regular transfers between accounts. If this value is nonzero, it indicates a clawback transaction where the sender is the asset's clawback address and the asset sender is the address from which the funds will be withdrawn.")]
            public Address AssetSender;

            [AlgoApiField("arcv")]
            [Tooltip("The recipient of the asset transfer.")]
            public Address AssetReceiver;

            [AlgoApiField("aclose")]
            [Tooltip("Specify this field to remove the asset holding from the sender account and reduce the account's minimum balance (i.e. opt-out of the asset).")]
            public Address AssetCloseTo;

            public bool Equals(Params other)
            {
                return XferAsset.Equals(other.XferAsset)
                    && AssetAmount.Equals(other.AssetAmount)
                    && AssetSender.Equals(other.AssetSender)
                    && AssetReceiver.Equals(other.AssetReceiver)
                    && AssetCloseTo.Equals(other.AssetCloseTo)
                    ;
            }
        }
    }
}
