using System;
using AlgoSdk.Crypto;
using Unity.Collections;

namespace AlgoSdk
{
    public partial struct Transaction
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
    public struct AssetConfigTxn
        : ITransaction
        , IEquatable<AssetConfigTxn>
    {
        internal TransactionHeader header;

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
        public struct Params
            : IEquatable<Params>
        {
            [AlgoApiField("asset-id", "xaid")]
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
