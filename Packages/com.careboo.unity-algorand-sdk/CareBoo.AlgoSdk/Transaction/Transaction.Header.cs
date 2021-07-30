using System;
using AlgoSdk.Crypto;
using MessagePack;
using Unity.Collections;

namespace AlgoSdk
{
    public static partial class Transaction
    {
        public struct Header
        : IDisposable
        {
            public ulong Fee;
            public ulong FirstValidRound;
            public Sha512_256_Hash GenesisHash;
            public ulong LastValidRound;
            public Address Sender;
            public readonly TransactionType TransactionType;
            public NativeText GenesisId;
            public NativeReference<Address> Group;
            public NativeReference<Address> Lease;
            public NativeText Note;
            public NativeReference<Address> RekeyTo;

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
                TransactionType = transactionType;

                GenesisId = default;
                Group = default;
                Lease = default;
                Note = default;
                RekeyTo = default;
            }

            public void Dispose()
            {
                if (GenesisId.IsCreated)
                    GenesisId.Dispose();
                if (Group.IsCreated)
                    Group.Dispose();
                if (Lease.IsCreated)
                    Lease.Dispose();
                if (Note.IsCreated)
                    Note.Dispose();
                if (RekeyTo.IsCreated)
                    RekeyTo.Dispose();
            }

            public readonly ref struct ReadOnly
            {
                readonly Header header;

                internal ReadOnly(in Header header)
                {
                    this.header = header;
                }

                public ulong Fee => header.Fee;
                public ulong FirstValidRound => header.FirstValidRound;
                public Sha512_256_Hash GenesisHash => header.GenesisHash;
                public ulong LastValidRound => header.LastValidRound;
                public Address Sender => header.Sender;
                public TransactionType TransactionType => header.TransactionType;

                public NativeText GenesisId => header.GenesisId;
                public NativeReference<Address>.ReadOnly Group => header.Group.AsReadOnly();
                public NativeReference<Address>.ReadOnly Lease => header.Lease.AsReadOnly();
                public NativeText Note => header.Note;
                public NativeReference<Address>.ReadOnly RekeyTo => header.RekeyTo.AsReadOnly();
            }

            public ReadOnly AsReadOnly()
            {
                return new ReadOnly(in this);
            }
        }
    }
}
