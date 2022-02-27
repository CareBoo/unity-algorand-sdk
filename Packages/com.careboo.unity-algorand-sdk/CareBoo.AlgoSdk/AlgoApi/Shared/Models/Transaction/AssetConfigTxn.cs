using System;
using AlgoSdk.Crypto;
using Unity.Collections;
using UnityEngine;

namespace AlgoSdk
{
    public interface IAssetConfigTxn : ITransaction
    {
        /// <summary>
        /// For re-configure or destroy transactions, this is the unique asset ID. On asset creation, the ID is set to zero.
        /// </summary>
        ulong ConfigAsset { get; set; }

        /// <summary>
        /// See <see cref="AssetParams"/> for all available fields.
        /// </summary>
        AssetParams AssetParams { get; set; }
    }

    public partial struct Transaction : IAssetConfigTxn
    {
        [AlgoApiField(null, "caid")]
        public ulong ConfigAsset
        {
            get => AssetConfigurationParams.ConfigAsset;
            set => AssetConfigurationParams.ConfigAsset = value;
        }

        [AlgoApiField(null, "apar")]
        public AssetParams AssetParams
        {
            get => AssetConfigurationParams.AssetParams;
            set => AssetConfigurationParams.AssetParams = value;
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
            ulong assetId,
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
            ulong assetId
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
            get => TransactionType.AssetConfiguration;
            internal set => header.TransactionType = TransactionType.AssetConfiguration;
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

        [AlgoApiField(null, "caid")]
        public ulong ConfigAsset
        {
            get => @params.ConfigAsset;
            set => @params.ConfigAsset = value;
        }

        [AlgoApiField(null, "apar")]
        public AssetParams AssetParams
        {
            get => @params.AssetParams;
            set => @params.AssetParams = value;
        }

        public void CopyTo(ref Transaction transaction)
        {
            transaction.HeaderParams = header;
            transaction.AssetConfigurationParams = @params;
        }

        public void CopyFrom(Transaction transaction)
        {
            header = transaction.HeaderParams;
            @params = transaction.AssetConfigurationParams;
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
            public ulong ConfigAsset;

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
