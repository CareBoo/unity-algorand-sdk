using System;
using AlgoSdk.Crypto;
using Unity.Collections;

namespace AlgoSdk
{
    public static partial class Transaction
    {
        public struct Header
            : ITransaction
            , IEquatable<Header>
        {
            public ulong Fee;
            public ulong FirstValidRound;
            public GenesisHash GenesisHash;
            public ulong LastValidRound;
            public Address Sender;
            TransactionType transactionType;
            public FixedString32Bytes GenesisId;
            public Address Group;
            public Address Lease;
            public byte[] Note;
            public Address RekeyTo;

            public TransactionType TransactionType => transactionType;

            Header ITransaction.Header => this;

            public Header(
                in ulong fee,
                in ulong firstValidRound,
                in Sha512_256_Hash genesisHash,
                in ulong lastValidRound,
                in Address sender,
                TransactionType transactionType
            )
            {
                Fee = fee;
                FirstValidRound = firstValidRound;
                GenesisHash = genesisHash;
                LastValidRound = lastValidRound;
                Sender = sender;
                this.transactionType = transactionType;

                GenesisId = default;
                Group = default;
                Lease = default;
                Note = default;
                RekeyTo = default;
            }

            public Header GetHeader()
            {
                return this;
            }

            public void CopyTo(ref RawTransaction rawTransaction)
            {
                rawTransaction.Fee = Fee;
                rawTransaction.FirstValidRound = FirstValidRound;
                rawTransaction.GenesisHash = GenesisHash;
                rawTransaction.LastValidRound = LastValidRound;
                rawTransaction.Sender = Sender;
                rawTransaction.TransactionType = TransactionType;
                rawTransaction.GenesisId = GenesisId;
                rawTransaction.Group = Group;
                rawTransaction.Lease = Lease;
                rawTransaction.Note = Note;
                rawTransaction.RekeyTo = RekeyTo;
            }

            public void CopyFrom(in RawTransaction rawTransaction)
            {
                Fee = rawTransaction.Fee;
                FirstValidRound = rawTransaction.FirstValidRound;
                GenesisHash = rawTransaction.GenesisHash;
                LastValidRound = rawTransaction.LastValidRound;
                Sender = rawTransaction.Sender;
                transactionType = rawTransaction.TransactionType;
                GenesisId = rawTransaction.GenesisId;
                Group = rawTransaction.Group;
                Lease = rawTransaction.Lease;
                Note = rawTransaction.Note;
                RekeyTo = rawTransaction.RekeyTo;
            }

            public bool Equals(Header other)
            {
                return Fee == other.Fee
                    && FirstValidRound == other.FirstValidRound
                    && GenesisHash.Equals(other.GenesisHash)
                    && LastValidRound == other.LastValidRound
                    && Sender == other.Sender
                    && TransactionType == other.TransactionType
                    && GenesisId == other.GenesisId
                    && Group == other.Group
                    && Lease == other.Lease
                    && string.Equals(Note, other.Note)
                    && RekeyTo == other.RekeyTo
                    ;
            }
        }
    }
}
