using System;
using AlgoSdk.Crypto;
using Unity.Collections;

namespace AlgoSdk
{
    public partial struct Transaction
    {
        [AlgoApiField(null, "rcv")]
        public Address Receiver
        {
            get => PaymentParams.Receiver;
            set => PaymentParams.Receiver = value;
        }

        [AlgoApiField(null, "amt")]
        public ulong Amount
        {
            get => PaymentParams.Amount;
            set => PaymentParams.Amount = value;
        }

        [AlgoApiField(null, "close")]
        public Address CloseRemainderTo
        {
            get => PaymentParams.CloseRemainderTo;
            set => PaymentParams.CloseRemainderTo = value;
        }

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

            public Optional<ulong> CloseAmount
            {
                get => @params.CloseAmount;
                set => @params.CloseAmount = value;
            }

            public Payment(
                 ulong fee,
                 ulong firstValidRound,
                 Sha512_256_Hash genesisHash,
                 ulong lastValidRound,
                 Address sender,
                 Address receiver,
                 ulong amount
            )
            {
                header = new Header(
                    fee,
                    firstValidRound,
                    genesisHash,
                    lastValidRound,
                    sender,
                    TransactionType.Payment
                );
                @params = new Params(
                     receiver,
                     amount
                );
            }

            public void CopyTo(ref Transaction transaction)
            {
                transaction.HeaderParams = header;
                transaction.PaymentParams = @params;
            }

            public void CopyFrom(Transaction transaction)
            {
                header = transaction.HeaderParams;
                @params = transaction.PaymentParams;
            }

            public bool Equals(Payment other)
            {
                return header.Equals(other.Header)
                    && @params.Equals(other.@params)
                    ;
            }

            [AlgoApiObject]
            public struct Params
                : IEquatable<Params>
            {
                [AlgoApiField("receiver", "rcv")]
                public Address Receiver;

                [AlgoApiField("amount", "amt")]
                public ulong Amount;

                [AlgoApiField("close-remainder-to", "close")]
                public Address CloseRemainderTo;

                [AlgoApiField("close-amount", "close-amount")]
                public Optional<ulong> CloseAmount;

                public Params(
                     Address receiver,
                     ulong amount
                )
                {
                    Receiver = receiver;
                    Amount = amount;
                    CloseRemainderTo = default;
                    CloseAmount = default;
                }

                public bool Equals(Params other)
                {
                    return Receiver.Equals(other.Receiver)
                        && Amount.Equals(other.Amount)
                        && CloseRemainderTo.Equals(other.CloseRemainderTo)
                        && CloseAmount.Equals(other.CloseAmount)
                        ;
                }
            }
        }
    }
}
