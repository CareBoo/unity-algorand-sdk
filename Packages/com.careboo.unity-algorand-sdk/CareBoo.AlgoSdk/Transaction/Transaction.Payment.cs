using System;
using AlgoSdk.Crypto;
using Unity.Collections;

namespace AlgoSdk
{
    public static partial class Transaction
    {
        public struct Payment
            : ITransaction
            , IEquatable<Payment>
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

            public Address Receiver
            {
                get => @params.Receiver;
                set => @params.Receiver = value;
            }

            public ulong Amount
            {
                get => @params.Amount;
                set => @params.Amount = value;
            }

            public Address CloseRemainderTo
            {
                get => @params.CloseRemainderTo;
                set => @params.CloseRemainderTo = value;
            }

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
                header = new Header(
                    in fee,
                    in firstValidRound,
                    in genesisHash,
                    in lastValidRound,
                    in sender,
                    TransactionType.Payment
                );
                @params = new Params(
                    in receiver,
                    in amount
                );
            }

            public void CopyTo(ref RawTransaction rawTransaction)
            {
                Header.CopyTo(ref rawTransaction);
                rawTransaction.PaymentParams = @params;
            }

            public void CopyFrom(in RawTransaction rawTransaction)
            {
                Header = rawTransaction.Header;
                @params = rawTransaction.PaymentParams;
            }

            public bool Equals(Payment other)
            {
                return header.Equals(other.Header)
                    && @params.Equals(other.@params)
                    ;
            }

            public struct Params
                : IEquatable<Params>
            {
                public Address Receiver;
                public ulong Amount;
                public Address CloseRemainderTo;

                public Params(
                    in Address receiver,
                    in ulong amount
                )
                {
                    Receiver = receiver;
                    Amount = amount;
                    CloseRemainderTo = default;
                }

                public bool Equals(Params other)
                {
                    return Receiver.Equals(other.Receiver)
                        && Amount.Equals(other.Amount)
                        && CloseRemainderTo.Equals(other.CloseRemainderTo)
                        ;
                }
            }
        }
    }
}
