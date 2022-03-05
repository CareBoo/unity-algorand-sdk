using System;
using AlgoSdk.Crypto;
using Unity.Collections;
using UnityEngine;

namespace AlgoSdk
{
    public interface IAssetTransferTxn : ITransaction
    {
        /// <summary>
        /// The unique ID of the asset to be transferred.
        /// </summary>
        ulong XferAsset { get; set; }

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

        /// <summary>
        /// The amount returned from the close out.
        /// </summary>
        ulong CloseAmount { get; set; }
    }

    public partial struct Transaction
    {
        [AlgoApiField(null, "xaid")]
        public ulong XferAsset
        {
            get => AssetTransferParams.XferAsset;
            set => AssetTransferParams.XferAsset = value;
        }

        [AlgoApiField(null, "aamt")]
        public ulong AssetAmount
        {
            get => AssetTransferParams.AssetAmount;
            set => AssetTransferParams.AssetAmount = value;
        }

        [AlgoApiField(null, "asnd")]
        public Address AssetSender
        {
            get => AssetTransferParams.AssetSender;
            set => AssetTransferParams.AssetSender = value;
        }

        [AlgoApiField(null, "arcv")]
        public Address AssetReceiver
        {
            get => AssetTransferParams.AssetReceiver;
            set => AssetTransferParams.AssetReceiver = value;
        }

        [AlgoApiField(null, "aclose")]
        public Address AssetCloseTo
        {
            get => AssetTransferParams.AssetCloseTo;
            set => AssetTransferParams.AssetCloseTo = value;
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
            ulong xferAsset,
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
            ulong xferAsset
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
            ulong xferAsset,
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

        [SerializeField]
        Params @params;

        [AlgoApiField("fee", "fee")]
        public ulong Fee
        {
            get => header.Fee;
            set => header.Fee = value;
        }

        [AlgoApiField("first-valid", "fv")]
        public ulong FirstValidRound
        {
            get => header.FirstValidRound;
            set => header.FirstValidRound = value;
        }

        [AlgoApiField("genesis-hash", "gh")]
        public GenesisHash GenesisHash
        {
            get => header.GenesisHash;
            set => header.GenesisHash = value;
        }

        [AlgoApiField("last-valid", "lv")]
        public ulong LastValidRound
        {
            get => header.LastValidRound;
            set => header.LastValidRound = value;
        }

        [AlgoApiField("sender", "snd")]
        public Address Sender
        {
            get => header.Sender;
            set => header.Sender = value;
        }

        [AlgoApiField("tx-type", "type")]
        public TransactionType TransactionType
        {
            get => TransactionType.AssetTransfer;
            internal set => header.TransactionType = TransactionType.AssetTransfer;
        }

        [AlgoApiField("genesis-id", "gen")]
        public FixedString32Bytes GenesisId
        {
            get => header.GenesisId;
            set => header.GenesisId = value;
        }

        [AlgoApiField("group", "grp")]
        public Sha512_256_Hash Group
        {
            get => header.Group;
            set => header.Group = value;
        }

        [AlgoApiField("lease", "lx")]
        public Sha512_256_Hash Lease
        {
            get => header.Lease;
            set => header.Lease = value;
        }

        [AlgoApiField("note", "note")]
        public byte[] Note
        {
            get => header.Note;
            set => header.Note = value;
        }

        [AlgoApiField("rekey-to", "rekey")]
        public Address RekeyTo
        {
            get => header.RekeyTo;
            set => header.RekeyTo = value;
        }

        [AlgoApiField(null, "xaid")]
        public ulong XferAsset
        {
            get => @params.XferAsset;
            set => @params.XferAsset = value;
        }

        [AlgoApiField(null, "aamt")]
        public ulong AssetAmount
        {
            get => @params.AssetAmount;
            set => @params.AssetAmount = value;
        }

        [AlgoApiField(null, "asnd")]
        public Address AssetSender
        {
            get => @params.AssetSender;
            set => @params.AssetSender = value;
        }

        [AlgoApiField(null, "arcv")]
        public Address AssetReceiver
        {
            get => @params.AssetReceiver;
            set => @params.AssetReceiver = value;
        }

        [AlgoApiField(null, "aclose")]
        public Address AssetCloseTo
        {
            get => @params.AssetCloseTo;
            set => @params.AssetCloseTo = value;
        }

        [AlgoApiField("close-amount", "close-amount", readOnly: true)]
        public ulong CloseAmount
        {
            get => @params.CloseAmount;
            set => @params.CloseAmount = value;
        }

        public void CopyTo(ref Transaction transaction)
        {
            transaction.HeaderParams = header;
            transaction.AssetTransferParams = @params;
        }

        public void CopyFrom(Transaction transaction)
        {
            header = transaction.HeaderParams;
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
            [AlgoApiField("asset-id", "xaid")]
            [Tooltip("The unique ID of the asset to be transferred.")]
            public ulong XferAsset;

            [AlgoApiField("amount", "aamt")]
            [Tooltip("The amount of the asset to be transferred. A zero amount transferred to self allocates that asset in the account's Asset map.")]
            public ulong AssetAmount;

            [AlgoApiField("sender", "asnd")]
            [Tooltip("The sender of the transfer. The regular Sender field should be used and this one set to the zero value for regular transfers between accounts. If this value is nonzero, it indicates a clawback transaction where the sender is the asset's clawback address and the asset sender is the address from which the funds will be withdrawn.")]
            public Address AssetSender;

            [AlgoApiField("receiver", "arcv")]
            [Tooltip("The recipient of the asset transfer.")]
            public Address AssetReceiver;

            [AlgoApiField("close-to", "aclose")]
            [Tooltip("Specify this field to remove the asset holding from the sender account and reduce the account's minimum balance (i.e. opt-out of the asset).")]
            public Address AssetCloseTo;

            [AlgoApiField("close-amount", "close-amount")]
            [NonSerialized]
            public ulong CloseAmount;

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
