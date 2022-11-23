using System;
using Unity.Collections;
using UnityEngine;

namespace Algorand.Unity
{
    public interface IAssetFreezeTxn : ITransaction
    {
        /// <summary>
        /// The address of the account whose asset is being frozen or unfrozen.
        /// </summary>
        Address FreezeAccount { get; set; }

        /// <summary>
        /// The asset ID being frozen or unfrozen.
        /// </summary>
        AssetIndex FreezeAsset { get; set; }

        /// <summary>
        /// True to freeze the asset.
        /// </summary>
        Optional<bool> AssetFrozen { get; set; }
    }

    public partial struct Transaction : IAssetFreezeTxn
    {
        /// <inheritdoc />
        [AlgoApiField("fadd")]
        public Address FreezeAccount
        {
            get => assetFreezeParams.FreezeAccount;
            set => assetFreezeParams.FreezeAccount = value;
        }

        /// <inheritdoc />
        [AlgoApiField("faid")]
        public AssetIndex FreezeAsset
        {
            get => assetFreezeParams.FreezeAsset;
            set => assetFreezeParams.FreezeAsset = value;
        }

        /// <inheritdoc />
        [AlgoApiField("afrz")]
        public Optional<bool> AssetFrozen
        {
            get => assetFreezeParams.AssetFrozen;
            set => assetFreezeParams.AssetFrozen = value;
        }

        /// <summary>
        /// Create an <see cref="AssetFreezeTxn"/> which is used to freeze or unfreeze an asset from transfers.
        /// </summary>
        /// <param name="sender">The address of the account that pays the fee and amount.</param>
        /// <param name="txnParams">See <see cref="TransactionParams"/></param>
        /// <param name="freezeAccount">The address of the account whose asset is being frozen or unfrozen.</param>
        /// <param name="freezeAsset">The asset ID being frozen or unfrozen.</param>
        /// <param name="assetFrozen">True to freeze the asset.</param>
        /// <returns>An <see cref="AssetFreezeTxn"/>.</returns>
        public static AssetFreezeTxn AssetFreeze(
            Address sender,
            TransactionParams txnParams,
            Address freezeAccount,
            AssetIndex freezeAsset,
            bool assetFrozen
        )
        {
            var txn = new AssetFreezeTxn
            {
                header = new TransactionHeader(sender, TransactionType.AssetFreeze, txnParams),
                FreezeAccount = freezeAccount,
                FreezeAsset = freezeAsset,
                AssetFrozen = assetFrozen
            };
            txn.Fee = txn.GetSuggestedFee(txnParams);
            return txn;
        }
    }

    [AlgoApiObject]
    [Serializable]
    public partial struct AssetFreezeTxn
        : IAssetFreezeTxn
        , IEquatable<AssetFreezeTxn>
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
            get => TransactionType.AssetFreeze;
            internal set => header.TransactionType = TransactionType.AssetFreeze;
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
        [AlgoApiField("fadd")]
        public Address FreezeAccount
        {
            get => @params.FreezeAccount;
            set => @params.FreezeAccount = value;
        }

        /// <inheritdoc />
        [AlgoApiField("faid")]
        public AssetIndex FreezeAsset
        {
            get => @params.FreezeAsset;
            set => @params.FreezeAsset = value;
        }

        /// <inheritdoc />
        [AlgoApiField("afrz")]
        public Optional<bool> AssetFrozen
        {
            get => @params.AssetFrozen;
            set => @params.AssetFrozen = value;
        }

        /// <inheritdoc />
        public void CopyTo(ref Transaction transaction)
        {
            transaction.Header = header;
            transaction.AssetFreezeParams = @params;
        }

        /// <inheritdoc />
        public void CopyFrom(Transaction transaction)
        {
            header = transaction.Header;
            @params = transaction.AssetFreezeParams;
        }

        public bool Equals(AssetFreezeTxn other)
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
            [AlgoApiField("fadd")]
            [Tooltip("The address of the account whose asset is being frozen or unfrozen.")]
            public Address FreezeAccount;

            [AlgoApiField("faid")]
            [Tooltip("The asset ID being frozen or unfrozen.")]
            public AssetIndex FreezeAsset;

            [AlgoApiField("afrz")]
            [Tooltip("True to freeze the asset.")]
            public Optional<bool> AssetFrozen;

            public bool Equals(Params other)
            {
                return FreezeAccount.Equals(other.FreezeAccount)
                    && FreezeAsset.Equals(other.FreezeAccount)
                    && AssetFrozen.Equals(other.AssetFrozen)
                    ;
            }
        }
    }
}
