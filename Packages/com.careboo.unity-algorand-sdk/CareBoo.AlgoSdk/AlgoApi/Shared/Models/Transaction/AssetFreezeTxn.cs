using System;
using Unity.Collections;

namespace AlgoSdk
{
    public partial struct Transaction
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
    public struct AssetFreezeTxn
        : ITransaction
        , IEquatable<AssetFreezeTxn>
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
        public Address Group
        {
            get => header.Group;
            set => header.Group = value;
        }

        [AlgoApiField("lease", "lx")]
        public Address Lease
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
        public struct Params
            : IEquatable<Params>
        {
            [AlgoApiField("address", "fadd")]
            public Address FreezeAccount;

            [AlgoApiField("asset-id", "faid")]
            public ulong FreezeAsset;

            [AlgoApiField("new-freeze-status", "afrz")]
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
