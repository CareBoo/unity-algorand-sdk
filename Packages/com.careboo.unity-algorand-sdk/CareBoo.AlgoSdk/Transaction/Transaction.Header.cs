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

            public TransactionId Id;
            public Address AuthAddress;
            public ulong CloseRewards;
            public ulong ClosingAmount;
            public ulong ConfirmedRound;
            public ulong CreatedApplicationIndex;
            public ulong CreatedAssetIndex;
            public ulong IntraRoundOffset;
            public EvalDeltaKeyValue[] GlobalStateDelta;
            public AccountStateDelta[] LocalStateDelta;
            public ulong ReceiverRewards;
            public ulong RoundTime;
            public ulong SenderRewards;
            public OnCompletion OnCompletion;


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

                Id = default;
                AuthAddress = default;
                CloseRewards = default;
                ClosingAmount = default;
                ConfirmedRound = default;
                CreatedApplicationIndex = default;
                CreatedAssetIndex = default;
                IntraRoundOffset = default;
                GlobalStateDelta = default;
                LocalStateDelta = default;
                ReceiverRewards = default;
                RoundTime = default;
                SenderRewards = default;
                OnCompletion = default;
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
                if (obj is Header other)
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
