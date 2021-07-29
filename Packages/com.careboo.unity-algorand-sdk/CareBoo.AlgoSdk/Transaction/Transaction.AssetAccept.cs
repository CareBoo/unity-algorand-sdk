using System;
using AlgoSdk.Crypto;
using Unity.Collections;

namespace AlgoSdk
{
    public static partial class Transaction
    {
        public struct AssetAccept
        : IDisposable
        , ITransaction
        {
            public Header Header;
            public ulong TransferAsset;
            public Address AssetSender;
            public Address AssetReceiver;

            NativeReference<Address> assetCloseTo;

            public AssetAccept(
                in ulong fee,
                in ulong firstValidRound,
                in Sha512_256_Hash genesisHash,
                in ulong lastValidRound,
                in Address sender,
                in ulong transferAsset,
                in Address assetSender,
                in Address assetReceiver
            )
            {
                Header = new Header(
                    in fee,
                    in firstValidRound,
                    in genesisHash,
                    in lastValidRound,
                    in sender,
                    TransactionType.AssetTransfer
                );
                TransferAsset = transferAsset;
                AssetSender = assetSender;
                AssetReceiver = assetReceiver;
                assetCloseTo = default;
            }

            public NativeReference<Address> AssetCloseTo => assetCloseTo;

            public void SetAssetCloseTo(ref NativeReference<Address> value)
            {
                if (assetCloseTo.IsCreated)
                    assetCloseTo.Dispose();
                assetCloseTo = value;
            }

            Header.ReadOnly ITransaction.Header => Header.AsReadOnly();

            public void Dispose()
            {
                Header.Dispose();
                if (assetCloseTo.IsCreated)
                    assetCloseTo.Dispose();
            }
        }
    }
}
