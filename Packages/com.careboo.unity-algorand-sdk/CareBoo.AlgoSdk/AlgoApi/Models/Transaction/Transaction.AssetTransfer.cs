using System;
using AlgoSdk.Crypto;
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

        public struct AssetTransfer
            : ITransaction
            , IEquatable<AssetTransfer>
        {
            Header header;

            Params @params;

            public Header Header
            {
                get => header;
                set => header = value;
            }

            public ulong Fee
            {
                get => header.Fee;
                set => header.Fee = value;
            }

            public ulong FirstValidRound
            {
                get => header.FirstValidRound;
                set => header.FirstValidRound = value;
            }
            public GenesisHash GenesisHash
            {
                get => header.GenesisHash;
                set => header.GenesisHash = value;
            }
            public ulong LastValidRound
            {
                get => header.LastValidRound;
                set => header.LastValidRound = value;
            }

            public Address Sender
            {
                get => header.Sender;
                set => header.Sender = value;
            }

            public FixedString32Bytes GenesisId
            {
                get => header.GenesisId;
                set => header.GenesisId = value;
            }

            public Address Group
            {
                get => header.Group;
                set => header.Group = value;
            }

            public Address Lease
            {
                get => header.Lease;
                set => header.Lease = value;
            }

            public byte[] Note
            {
                get => header.Note;
                set => header.Note = value;
            }

            public Address RekeyTo
            {
                get => header.RekeyTo;
                set => header.RekeyTo = value;
            }

            public ulong XferAsset
            {
                get => @params.XferAsset;
                set => @params.XferAsset = value;
            }

            public ulong AssetAmount
            {
                get => @params.AssetAmount;
                set => @params.AssetAmount = value;
            }

            public Address AssetSender
            {
                get => @params.AssetSender;
                set => @params.AssetSender = value;
            }

            public Address AssetReceiver
            {
                get => @params.AssetReceiver;
                set => @params.AssetReceiver = value;
            }

            public Address AssetCloseTo
            {
                get => @params.AssetCloseTo;
                set => @params.AssetCloseTo = value;
            }

            public ulong CloseAmount
            {
                get => @params.CloseAmount;
                set => @params.CloseAmount = value;
            }

            public AssetTransfer(
                ulong fee,
                ulong firstValidRound,
                Sha512_256_Hash genesisHash,
                ulong lastValidRound,
                Address sender,
                ulong xferAsset,
                ulong assetAmount,
                Address assetSender,
                Address assetReceiver
            )
            {
                header = new Header(
                    fee,
                    firstValidRound,
                    genesisHash,
                    lastValidRound,
                    sender,
                    TransactionType.AssetTransfer
                );
                @params = new Params(
                    xferAsset,
                    assetAmount,
                    assetSender,
                    assetReceiver
                );
            }

            public void CopyTo(ref Transaction transaction)
            {
                transaction.HeaderParams = header;
                transaction.AssetTransferParams = @params;
            }

            public void CopyFrom(Transaction transaction)
            {
                Header = transaction.HeaderParams;
                @params = transaction.AssetTransferParams;
            }

            public bool Equals(AssetTransfer other)
            {
                return header.Equals(other.header)
                    && @params.Equals(other.@params)
                    ;
            }

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

                public Params(
                    ulong xferAsset,
                    ulong assetAmount,
                    Address assetSender,
                    Address assetReceiver
                )
                {
                    XferAsset = xferAsset;
                    AssetAmount = assetAmount;
                    AssetSender = assetSender;
                    AssetReceiver = assetReceiver;
                    AssetCloseTo = default;
                    CloseAmount = default;
                }

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
}
