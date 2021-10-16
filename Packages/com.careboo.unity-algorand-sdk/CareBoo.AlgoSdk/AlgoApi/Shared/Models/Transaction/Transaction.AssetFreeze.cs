using System;
using AlgoSdk.Crypto;
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

        public struct AssetFreeze
            : ITransaction
            , IEquatable<AssetFreeze>
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

            public Address FreezeAccount
            {
                get => @params.FreezeAccount;
                set => @params.FreezeAccount = value;
            }

            public ulong FreezeAsset
            {
                get => @params.FreezeAsset;
                set => @params.FreezeAsset = value;
            }

            public Optional<bool> AssetFrozen
            {
                get => @params.AssetFrozen;
                set => @params.AssetFrozen = value;
            }

            public AssetFreeze(
                Address sender,
                TransactionParams txnParams,
                Address freezeAccount,
                ulong freezeAsset,
                bool assetFrozen
            )
            {
                header = new Header(
                    sender,
                    TransactionType.AssetFreeze,
                    txnParams
                );
                @params = new Params(
                    freezeAccount,
                    freezeAsset,
                    assetFrozen
                );
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

            public bool Equals(AssetFreeze other)
            {
                return header.Equals(other.Header)
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

                public Params(
                    Address freezeAccount,
                    ulong freezeAsset,
                    bool assetFrozen
                )
                {
                    FreezeAccount = freezeAccount;
                    FreezeAsset = freezeAsset;
                    AssetFrozen = assetFrozen;
                }

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
}
