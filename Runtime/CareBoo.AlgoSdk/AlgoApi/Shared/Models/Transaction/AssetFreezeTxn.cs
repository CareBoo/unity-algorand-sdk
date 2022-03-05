using System;
using AlgoSdk.Crypto;
using Unity.Collections;
using UnityEngine;

namespace AlgoSdk
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
        ulong FreezeAsset { get; set; }

        /// <summary>
        /// True to freeze the asset.
        /// </summary>
        Optional<bool> AssetFrozen { get; set; }
    }

    public partial struct Transaction : IAssetFreezeTxn
    {
        [AlgoApiField(null, "fadd")]
        public Address FreezeAccount
        {
            get => AssetFreezeParams.FreezeAccount;
            set => AssetFreezeParams.FreezeAccount = value;
        }

        [AlgoApiField(null, "faid")]
        public ulong FreezeAsset
        {
            get => AssetFreezeParams.FreezeAsset;
            set => AssetFreezeParams.FreezeAsset = value;
        }

        [AlgoApiField(null, "afrz")]
        public Optional<bool> AssetFrozen
        {
            get => AssetFreezeParams.AssetFrozen;
            set => AssetFreezeParams.AssetFrozen = value;
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
            ulong freezeAsset,
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
            get => TransactionType.AssetFreeze;
            internal set => header.TransactionType = TransactionType.AssetFreeze;
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
        [AlgoApiField(null, "fadd")]
        public Address FreezeAccount
        {
            get => @params.FreezeAccount;
            set => @params.FreezeAccount = value;
        }

        [AlgoApiField(null, "faid")]
        public ulong FreezeAsset
        {
            get => @params.FreezeAsset;
            set => @params.FreezeAsset = value;
        }

        [AlgoApiField(null, "afrz")]
        public Optional<bool> AssetFrozen
        {
            get => @params.AssetFrozen;
            set => @params.AssetFrozen = value;
        }

        public void CopyTo(ref Transaction transaction)
        {
            transaction.HeaderParams = header;
            transaction.AssetFreezeParams = @params;
        }

        public void CopyFrom(Transaction transaction)
        {
            header = transaction.HeaderParams;
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
            [AlgoApiField("address", "fadd")]
            [Tooltip("The address of the account whose asset is being frozen or unfrozen.")]
            public Address FreezeAccount;

            [AlgoApiField("asset-id", "faid")]
            [Tooltip("The asset ID being frozen or unfrozen.")]
            public ulong FreezeAsset;

            [AlgoApiField("new-freeze-status", "afrz")]
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
