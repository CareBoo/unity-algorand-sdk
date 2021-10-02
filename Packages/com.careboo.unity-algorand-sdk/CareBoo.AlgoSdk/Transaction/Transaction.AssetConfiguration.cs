using System;
using AlgoSdk.Crypto;
using Unity.Collections;

namespace AlgoSdk
{
    public static partial class Transaction
    {
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
                ulong fee,
                ulong firstValidRound,
                Sha512_256_Hash genesisHash,
                ulong lastValidRound,
                Address sender,
                ulong configAsset,
                AssetParams assetParams
            )
            {
                header = new Header(
                    fee,
                    firstValidRound,
                    genesisHash,
                    lastValidRound,
                    sender,
                    TransactionType.AssetConfiguration
                );
                @params = new Params(
                    configAsset,
                    assetParams
                );
            }

            public void CopyTo(ref RawTransaction rawTransaction)
            {
                rawTransaction.Header = header;
                rawTransaction.AssetConfigurationParams = @params;
            }

            public void CopyFrom(RawTransaction rawTransaction)
            {
                header = rawTransaction.Header;
                @params = rawTransaction.AssetConfigurationParams;
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
