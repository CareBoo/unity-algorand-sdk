using AlgoSdk.Crypto;
using AlgoSdk.MsgPack;
using Unity.Collections;
using Unity.Jobs;

namespace AlgoSdk
{
    public static partial class Transaction
    {
        public struct Header : ITransaction, INativeDisposable
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

            public void CopyToRawTransaction(ref RawTransaction rawTransaction)
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

            public void CopyFromRawTransaction(in RawTransaction rawTransaction)
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
        }
    }
}
