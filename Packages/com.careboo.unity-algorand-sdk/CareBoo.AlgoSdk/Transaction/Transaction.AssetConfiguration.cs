using System;
using AlgoSdk.Crypto;
using MessagePack;
using Unity.Collections;

namespace AlgoSdk
{
    public static partial class Transaction
    {
        [MessagePackObject]
        public struct AssetConfiguration
        : IDisposable
        , ITransaction
        {
            Header header;

            [Key("caid")]
            public ulong ConfigAsset;

            [Key("apar")]
            public AssetParams AssetParams;

            [Key("fee")]
            public ulong Fee { get => header.Fee; set => header.Fee = value; }

            [Key("fv")]
            public ulong FirstValidRound { get => header.FirstValidRound; set => header.FirstValidRound = value; }

            [Key("gh")]
            public Sha512_256_Hash GenesisHash { get => header.GenesisHash; set => header.GenesisHash = value; }

            [Key("lv")]
            public ulong LastValidRound { get => header.LastValidRound; set => header.LastValidRound = value; }

            [Key("snd")]
            public Address Sender { get => header.Sender; set => header.Sender = value; }

            [Key("type")]
            public TransactionType TransactionType => header.TransactionType;

            [Key("gen")]
            public NativeText GenesisId { get => header.GenesisId; set => header.GenesisId = value; }

            [Key("grp")]
            public NativeReference<Address> Group { get => header.Group; set => header.Group = value; }

            [Key("lx")]
            public NativeReference<Address> Lease { get => header.Lease; set => header.Lease = value; }

            [Key("note")]
            public NativeText Note { get => header.Note; set => header.Note = value; }

            [Key("rekey")]
            public NativeReference<Address> RekeyTo { get => header.RekeyTo; set => header.RekeyTo = value; }

            public AssetConfiguration(
                in ulong fee,
                in ulong firstValidRound,
                in Sha512_256_Hash genesisHash,
                in ulong lastValidRound,
                in Address sender,
                in ulong configAsset,
                in AssetParams assetParams
            )
            {
                header = new Header(
                    in fee,
                    in firstValidRound,
                    in genesisHash,
                    in lastValidRound,
                    in sender,
                    TransactionType.AssetConfiguration
                );

                ConfigAsset = configAsset;
                AssetParams = assetParams;
            }

            public void Dispose()
            {
                header.Dispose();
            }
        }
    }
}
