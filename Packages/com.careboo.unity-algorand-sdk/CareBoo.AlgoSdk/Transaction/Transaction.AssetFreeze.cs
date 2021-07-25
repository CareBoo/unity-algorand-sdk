using System;
using AlgoSdk.Crypto;

namespace AlgoSdk
{
    public static partial class Transaction
    {
        public struct AssetFreeze
        : IDisposable
        , ITransaction
        {
            public Header Header;

            public AssetFreeze(
                in ulong fee,
                in ulong firstValidRound,
                in Sha512_256_Hash genesisHash,
                in ulong lastValidRound,
                in Address sender
            )
            {
                Header = new Header(
                    in fee,
                    in firstValidRound,
                    in genesisHash,
                    in lastValidRound,
                    in sender,
                    TransactionType.AssetFreeze
                );
            }

            Header.ReadOnly ITransaction.Header => Header.AsReadOnly();

            public void Dispose()
            {
                Header.Dispose();
            }
        }
    }
}
