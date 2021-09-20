using System;
using AlgoSdk.Crypto;
using Unity.Collections;

namespace AlgoSdk
{
    public static partial class Transaction
    {
        public struct Header
            : IEquatable<Header>
        {
            public ulong Fee;
            public ulong FirstValidRound;
            public GenesisHash GenesisHash;
            public ulong LastValidRound;
            public Address Sender;
            public TransactionType TransactionType;
            public FixedString32Bytes GenesisId;
            public Address Group;
            public Address Lease;
            public byte[] Note;
            public Address RekeyTo;

            public Header(
                ulong fee,
                ulong firstValidRound,
                Sha512_256_Hash genesisHash,
                ulong lastValidRound,
                Address sender,
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

            public override bool Equals(object obj)
            {
                if (obj is RawTransaction other)
                    return Equals(other);
                return false;
            }

            public override int GetHashCode()
            {
                unchecked
                {
                    int hash = 17;
                    hash = hash * 31 + Fee.GetHashCode();
                    hash = hash * 31 + FirstValidRound.GetHashCode();
                    return hash;
                }
            }
        }
    }
}
