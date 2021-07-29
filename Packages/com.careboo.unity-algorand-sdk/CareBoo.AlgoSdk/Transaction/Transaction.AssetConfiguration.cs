using System;
using AlgoSdk.Crypto;

namespace AlgoSdk
{
    public static partial class Transaction
    {
        public struct AssetConfiguration
        : IDisposable
        , ITransaction
        {
            public Header Header;
            public ulong ConfigAsset;
            public AssetParams AssetParams;

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
                Header = new Header(
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

            Header.ReadOnly ITransaction.Header => Header.AsReadOnly();

            public void Dispose()
            {
                Header.Dispose();
            }
        }
    }
}
