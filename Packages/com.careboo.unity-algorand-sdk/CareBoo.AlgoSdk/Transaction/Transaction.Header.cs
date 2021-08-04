using System;
using AlgoSdk.Crypto;
using AlgoSdk.MsgPack;
using Unity.Collections;
using Unity.Jobs;

namespace AlgoSdk
{
    public static partial class Transaction
    {
        public struct Header
            : ITransaction
            , INativeDisposable
            , IEquatable<Header>
        {
            public ulong Fee;
            public ulong FirstValidRound;
            public Sha512_256_Hash GenesisHash;
            public ulong LastValidRound;
            public Address Sender;
            TransactionType transactionType;
            public FixedString32 GenesisId;
            public Address Group;
            public Address Lease;
            public NativeText Note;
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

            public JobHandle Dispose(JobHandle inputDeps)
            {
                return Note.Dispose(inputDeps);
            }

            public void Dispose()
            {
                Note.Dispose();
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
                if (GenesisId.Length > 0)
                    rawTransaction.GenesisId = GenesisId;
                if (Group != default)
                    rawTransaction.Group = Group;
                if (Lease != default)
                    rawTransaction.Lease = Lease;
                if (Note.IsCreated)
                    rawTransaction.Note = Note;
                if (RekeyTo != default)
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
                if (rawTransaction.GenesisId.IsCreated)
                    GenesisId = rawTransaction.GenesisId;
                if (rawTransaction.Group.IsCreated)
                    Group = rawTransaction.Group;
                if (rawTransaction.Lease.IsCreated)
                    Lease = rawTransaction.Lease;
                if (rawTransaction.Note.IsCreated)
                    Note = rawTransaction.Note;
                if (rawTransaction.RekeyTo.IsCreated)
                    RekeyTo = rawTransaction.RekeyTo;
            }

            public bool Equals(Header other)
            {
                return Fee == other.Fee
                    && FirstValidRound == other.FirstValidRound
                    && GenesisHash == other.GenesisHash
                    && LastValidRound == other.LastValidRound
                    && Sender == other.Sender
                    && TransactionType == other.TransactionType
                    && GenesisId == other.GenesisId
                    && Group == other.Group
                    && Lease == other.Lease
                    && Note.IsCreated == other.Note.IsCreated
                    && (!Note.IsCreated || Note.Equals(other.Note))
                    && RekeyTo == other.RekeyTo
                    ;
            }
        }
    }
}
