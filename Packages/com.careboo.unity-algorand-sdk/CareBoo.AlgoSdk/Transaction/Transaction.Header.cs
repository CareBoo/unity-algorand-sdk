using System;
using AlgoSdk.Crypto;
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

            NativeText genesisId;
            NativeReference<Address> group;
            NativeReference<Address> lease;
            NativeText note;
            NativeReference<Address> rekeyTo;

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

                genesisId = default;
                group = default;
                lease = default;
                note = default;
                rekeyTo = default;
            }

            public NativeText GenesisId => genesisId;

            public void SetGenesisId(ref NativeText value)
            {
                if (genesisId.IsCreated)
                    genesisId.Dispose();
                genesisId = value;
            }

            public NativeReference<Address>.ReadOnly Group => group.AsReadOnly();

            public void SetGroup(ref NativeReference<Address> value)
            {
                if (group.IsCreated)
                    group.Dispose();
                group = value;
            }

            public NativeReference<Address>.ReadOnly Lease => lease.AsReadOnly();

            public void SetLease(ref NativeReference<Address> value)
            {
                if (lease.IsCreated)
                    lease.Dispose();
                lease = value;
            }

            public NativeText Note => note;

            public void SetNote(NativeText value)
            {
                if (note.IsCreated)
                    note.Dispose();
                note = value;
            }

            public NativeReference<Address>.ReadOnly RekeyTo => rekeyTo.AsReadOnly();

            public void SetRekeyTo(ref NativeReference<Address> value)
            {
                if (rekeyTo.IsCreated)
                    rekeyTo.Dispose();
                rekeyTo = value;
            }

            public void Dispose()
            {
                if (genesisId.IsCreated)
                    genesisId.Dispose();
                if (group.IsCreated)
                    group.Dispose();
                if (lease.IsCreated)
                    lease.Dispose();
                if (note.IsCreated)
                    note.Dispose();
                if (rekeyTo.IsCreated)
                    rekeyTo.Dispose();
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
                public NativeReference<Address>.ReadOnly Group => header.Group;
                public NativeReference<Address>.ReadOnly Lease => header.Lease;
                public NativeText Note => header.Note;
                public NativeReference<Address>.ReadOnly RekeyTo => header.RekeyTo;
            }

            public ReadOnly AsReadOnly()
            {
                return new ReadOnly(in this);
            }
        }
    }
}
