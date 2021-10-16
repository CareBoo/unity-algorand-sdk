using System;
using Unity.Collections;

namespace AlgoSdk
{
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

        public static AssetTransferTxn AssetTransfer(
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
                AssetReceiver = assetReceiver
            };
            txn.Fee = txn.GetSuggestedFee(txnParams);
            return txn;
        }

        public static AssetTransferTxn AssetAccept(
            Address sender,
            TransactionParams txnParams,
            ulong xferAsset
        )
        {
            var txn = new AssetTransferTxn
            {
                header = new TransactionHeader(sender, TransactionType.AssetTransfer, txnParams),
                XferAsset = xferAsset
            };
            txn.Fee = txn.GetSuggestedFee(txnParams);
            return txn;
        }

        public static AssetTransferTxn AssetClawback(
            Address clawbackAccount,
            TransactionParams txnParams,
            ulong xferAsset,
            ulong assetAmount,
            Address assetSender,
            Address assetReceiver
        )
        {
            var txn = new AssetTransferTxn
            {
                header = new TransactionHeader(clawbackAccount, TransactionType.AssetTransfer, txnParams),
                XferAsset = xferAsset,
                AssetAmount = assetAmount,
                AssetSender = assetSender,
                AssetReceiver = assetReceiver
            };
            txn.Fee = txn.GetSuggestedFee(txnParams);
            return txn;
        }
    }

    [AlgoApiObject]
    public struct AssetTransferTxn
        : ITransaction
        , IEquatable<AssetTransferTxn>
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
        public struct Params
            : IEquatable<Params>
        {
            [AlgoApiField("asset-id", "xaid")]
            public ulong XferAsset;

            [AlgoApiField("amount", "aamt")]
            public ulong AssetAmount;

            [AlgoApiField("sender", "asnd")]
            public Address AssetSender;

            [AlgoApiField("receiver", "arcv")]
            public Address AssetReceiver;

            [AlgoApiField("close-to", "aclose")]
            public Address AssetCloseTo;

            [AlgoApiField("close-amount", "close-amount")]
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
