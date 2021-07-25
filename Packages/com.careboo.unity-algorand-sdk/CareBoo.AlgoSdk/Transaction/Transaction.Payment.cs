using System;
using AlgoSdk.Crypto;
using Unity.Collections;

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
            NativeReference<Address> closeRemainderTo;

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
                closeRemainderTo = default;
            }

            public NativeReference<Address>.ReadOnly CloseRemainderTo => closeRemainderTo.AsReadOnly();

            public void SetCloseRemainderTo(ref NativeReference<Address> value)
            {
                if (closeRemainderTo.IsCreated)
                    closeRemainderTo.Dispose();
                closeRemainderTo = value;
            }

            Header.ReadOnly ITransaction.Header => Header.AsReadOnly();

            public void Dispose()
            {
                Header.Dispose();
                if (closeRemainderTo.IsCreated)
                    closeRemainderTo.Dispose();
            }
        }
    }
}
