using System;
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

        public struct AssetConfiguration
            : ITransaction
            , IEquatable<AssetConfiguration>
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

            public ulong ConfigAsset
            {
                get => @params.ConfigAsset;
                set => @params.ConfigAsset = value;
            }

            public AssetParams AssetParams
            {
                get => @params.AssetParams;
                set => @params.AssetParams = value;
            }

            public AssetConfiguration(
                Address sender,
                TransactionParams txnParams,
                ulong configAsset,
                AssetParams assetParams
            )
            {
                header = new Header(
                    sender,
                    TransactionType.AssetConfiguration,
                    txnParams
                );
                @params = new Params(
                    configAsset,
                    assetParams
                );
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

            public bool Equals(AssetConfiguration other)
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

                public Params(
                    ulong configAsset,
                    AssetParams assetParams
                )
                {
                    ConfigAsset = configAsset;
                    AssetParams = assetParams;
                }

                public bool Equals(Params other)
                {
                    return ConfigAsset.Equals(other.ConfigAsset)
                        && AssetParams.Equals(other.AssetParams)
                        ;
                }
            }
        }
    }
}
