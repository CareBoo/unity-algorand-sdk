using System;
using Unity.Collections;
using UnityEngine;

namespace AlgoSdk
{
    public interface IAssetConfigTxn : ITransaction
    {
        /// <summary>
        /// For re-configure or destroy transactions, this is the unique asset ID. On asset creation, the ID is set to zero.
        /// </summary>
        AssetIndex ConfigAsset { get; set; }

        /// <summary>
        /// See <see cref="AssetParams"/> for all available fields.
        /// </summary>
        AssetParams AssetParams { get; set; }
    }

    public partial struct Transaction : IAssetConfigTxn
    {
        /// <inheritdoc />
        [AlgoApiField(null, "caid")]
        public AssetIndex ConfigAsset
        {
            get => assetConfigParams.ConfigAsset;
            set => assetConfigParams.ConfigAsset = value;
        }

        /// <inheritdoc />
        [AlgoApiField(null, "apar")]
        public AssetParams AssetParams
        {
            get => assetConfigParams.AssetParams;
            set => assetConfigParams.AssetParams = value;
        }

        /// <summary>
        /// Create an <see cref="AssetConfigTxn"/> that will create an asset.
        /// </summary>
        /// <param name="sender">The address of the account that pays the fee and amount.</param>
        /// <param name="txnParams">See <see cref="TransactionParams"/></param>
        /// <param name="assetParams">See <see cref="AssetParams"/> for all available fields.</param>
        /// <returns>An <see cref="AssetConfigTxn"/> that will create an asset.</returns>
        public static AssetConfigTxn AssetCreate(
            Address sender,
            TransactionParams txnParams,
            AssetParams assetParams
        )
        {
            var txn = new AssetConfigTxn
            {
                header = new TransactionHeader(sender, TransactionType.AssetConfiguration, txnParams),
                AssetParams = assetParams
            };
            txn.Fee = txn.GetSuggestedFee(txnParams);
            return txn;
        }

        /// <summary>
        /// Create an <see cref="AssetConfigTxn"/> that will configure an asset.
        /// </summary>
        /// <param name="sender">The address of the account that pays the fee and amount.</param>
        /// <param name="txnParams">See <see cref="TransactionParams"/></param>
        /// <param name="assetId">The unique asset id.</param>
        /// <param name="assetParams">See <see cref="AssetParams"/> for all available fields.</param>
        /// <returns>An <see cref="AssetConfigTxn"/> that will configure an asset.</returns>
        public static AssetConfigTxn AssetConfigure(
            Address sender,
            TransactionParams txnParams,
            AssetIndex assetId,
            AssetParams assetParams
        )
        {
            var txn = new AssetConfigTxn
            {
                header = new TransactionHeader(sender, TransactionType.AssetConfiguration, txnParams),
                ConfigAsset = assetId,
                AssetParams = assetParams
            };
            txn.Fee = txn.GetSuggestedFee(txnParams);
            return txn;
        }

        /// <summary>
        /// Create an <see cref="AssetConfigTxn"/> that will delete an asset.
        /// </summary>
        /// <param name="sender">The address of the account that pays the fee and amount.</param>
        /// <param name="txnParams">See <see cref="TransactionParams"/></param>
        /// <param name="assetId">The unique asset id.</param>
        /// <returns>An <see cref="AssetConfigTxn"/> that will delete an asset.</returns>
        public static AssetConfigTxn AssetDelete(
            Address sender,
            TransactionParams txnParams,
            AssetIndex assetId
        )
        {
            var txn = new AssetConfigTxn
            {
                header = new TransactionHeader(sender, TransactionType.AssetConfiguration, txnParams),
                ConfigAsset = assetId
            };
            txn.Fee = txn.GetSuggestedFee(txnParams);
            return txn;
        }
    }

    [AlgoApiObject]
    [Serializable]
    public partial struct AssetConfigTxn
        : IAssetConfigTxn
        , IEquatable<AssetConfigTxn>
    {
        [SerializeField]
        internal TransactionHeader header;

        [SerializeField]
        Params @params;

        /// <inheritdoc />
        [AlgoApiField("fee", "fee")]
        public MicroAlgos Fee
        {
            get => header.Fee;
            set => header.Fee = value;
        }

        /// <inheritdoc />
        [AlgoApiField("first-valid", "fv")]
        public ulong FirstValidRound
        {
            get => header.FirstValidRound;
            set => header.FirstValidRound = value;
        }

        /// <inheritdoc />
        [AlgoApiField("genesis-hash", "gh")]
        public GenesisHash GenesisHash
        {
            get => header.GenesisHash;
            set => header.GenesisHash = value;
        }

        /// <inheritdoc />
        [AlgoApiField("last-valid", "lv")]
        public ulong LastValidRound
        {
            get => header.LastValidRound;
            set => header.LastValidRound = value;
        }

        /// <inheritdoc />
        [AlgoApiField("sender", "snd")]
        public Address Sender
        {
            get => header.Sender;
            set => header.Sender = value;
        }

        /// <inheritdoc />
        [AlgoApiField("tx-type", "type")]
        public TransactionType TransactionType
        {
            get => TransactionType.AssetConfiguration;
            internal set => header.TransactionType = TransactionType.AssetConfiguration;
        }

        /// <inheritdoc />
        [AlgoApiField("genesis-id", "gen")]
        public FixedString32Bytes GenesisId
        {
            get => header.GenesisId;
            set => header.GenesisId = value;
        }

        /// <inheritdoc />
        [AlgoApiField("group", "grp")]
        public TransactionId Group
        {
            get => header.Group;
            set => header.Group = value;
        }

        /// <inheritdoc />
        [AlgoApiField("lease", "lx")]
        public TransactionId Lease
        {
            get => header.Lease;
            set => header.Lease = value;
        }

        /// <inheritdoc />
        [AlgoApiField("note", "note")]
        public byte[] Note
        {
            get => header.Note;
            set => header.Note = value;
        }

        /// <inheritdoc />
        [AlgoApiField("rekey-to", "rekey")]
        public Address RekeyTo
        {
            get => header.RekeyTo;
            set => header.RekeyTo = value;
        }

        /// <inheritdoc />
        [AlgoApiField(null, "caid")]
        public AssetIndex ConfigAsset
        {
            get => @params.ConfigAsset;
            set => @params.ConfigAsset = value;
        }

        /// <inheritdoc />
        [AlgoApiField(null, "apar")]
        public AssetParams AssetParams
        {
            get => @params.AssetParams;
            set => @params.AssetParams = value;
        }

        /// <inheritdoc />
        public void CopyTo(ref Transaction transaction)
        {
            transaction.Header = header;
            transaction.AssetConfigParams = @params;
        }

        /// <inheritdoc />
        public void CopyFrom(Transaction transaction)
        {
            header = transaction.Header;
            @params = transaction.AssetConfigParams;
        }

        public bool Equals(AssetConfigTxn other)
        {
            return header.Equals(other.header)
                && @params.Equals(other.@params)
                ;
        }

        [AlgoApiObject]
        [Serializable]
        public partial struct Params
            : IEquatable<Params>
        {
            [AlgoApiField("asset-id", "xaid")]
            [Tooltip("For re-configure or destroy transactions, this is the unique asset ID. On asset creation, the ID is set to zero.")]
            public AssetIndex ConfigAsset;

            [AlgoApiField("params", "params")]
            public AssetParams AssetParams;

            public bool Equals(Params other)
            {
                return ConfigAsset.Equals(other.ConfigAsset)
                    && AssetParams.Equals(other.AssetParams)
                    ;
            }
        }
    }
}
