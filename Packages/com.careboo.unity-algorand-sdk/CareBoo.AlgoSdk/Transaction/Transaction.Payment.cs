using System;
using AlgoSdk.Crypto;
using AlgoSdk.MsgPack;

namespace AlgoSdk
{
    public static partial class Transaction
    {
        public struct Payment
            : IDisposable
            , ITransaction
        {
            public Header Header;
            public Address Receiver;
            public ulong Amount;
            public Address CloseRemainderTo;

            public Payment(
                in ulong fee,
                in ulong firstValidRound,
                in Sha512_256_Hash genesisHash,
                in ulong lastValidRound,
                in Address sender,
                in Address receiver,
                in ulong amount
            )
            {
                Header = new Header(
                    in fee,
                    in firstValidRound,
                    in genesisHash,
                    in lastValidRound,
                    in sender,
                    TransactionType.Payment
                );
                Receiver = receiver;
                Amount = amount;
                CloseRemainderTo = default;
            }

            public void Dispose()
            {
                Header.Dispose();
            }

            public Header GetHeader() => Header;

            public void CopyToRawTransaction(ref RawTransaction rawTransaction)
            {
                Header.CopyToRawTransaction(ref rawTransaction);
                rawTransaction.Receiver = Receiver;
                rawTransaction.Amount = Amount;
                rawTransaction.CloseRemainderTo = CloseRemainderTo;
            }

            public void CopyFromRawTransaction(in RawTransaction rawTransaction)
            {
                Header.CopyFromRawTransaction(in rawTransaction);
                Receiver = rawTransaction.Receiver;
                Amount = rawTransaction.Amount;
                CloseRemainderTo = rawTransaction.CloseRemainderTo;
            }
        }
    }
}
