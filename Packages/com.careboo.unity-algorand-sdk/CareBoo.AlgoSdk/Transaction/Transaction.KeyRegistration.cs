using System;
using AlgoSdk.Crypto;
using Unity.Collections;

namespace AlgoSdk
{
    public static partial class Transaction
    {
        public struct KeyRegistration
            : ITransaction
            , IEquatable<KeyRegistration>
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

            public Address VotePk
            {
                get => @params.VotePk;
                set => @params.VotePk = value;
            }

            public VrfPubkey SelectionPk
            {
                get => @params.SelectionPk;
                set => @params.SelectionPk = value;
            }

            public ulong VoteFirst
            {
                get => @params.VoteFirst;
                set => @params.VoteFirst = value;
            }

            public ulong VoteLast
            {
                get => @params.VoteLast;
                set => @params.VoteLast = value;
            }

            public ulong VoteKeyDilution
            {
                get => @params.VoteKeyDilution;
                set => @params.VoteKeyDilution = value;
            }

            public Optional<bool> NonParticipation
            {
                get => @params.NonParticipation;
                set => @params.NonParticipation = value;
            }

            public KeyRegistration(
                 ulong fee,
                 ulong firstValidRound,
                 Sha512_256_Hash genesisHash,
                 ulong lastValidRound,
                 Address sender
            )
            {
                header = new Header(
                    fee,
                    firstValidRound,
                    genesisHash,
                    lastValidRound,
                    sender,
                    TransactionType.KeyRegistration
                );
                @params = new Params(
                );
            }

            public void CopyTo(ref RawTransaction rawTransaction)
            {
                rawTransaction.Header = header;
                rawTransaction.KeyRegistrationParams = @params;
            }

            public void CopyFrom(RawTransaction rawTransaction)
            {
                header = rawTransaction.Header;
                @params = rawTransaction.KeyRegistrationParams;
            }

            public bool Equals(KeyRegistration other)
            {
                return header.Equals(other.Header)
                    && @params.Equals(other.@params)
                    ;
            }

            public struct Params
                : IEquatable<Params>
            {
                public Address VotePk;
                public VrfPubkey SelectionPk;
                public ulong VoteFirst;
                public ulong VoteLast;
                public ulong VoteKeyDilution;
                public Optional<bool> NonParticipation;

                public Params(
                    Address votePk,
                    VrfPubkey selectionPk,
                    ulong voteFirst,
                    ulong voteLast,
                    ulong voteKeyDilution
                )
                {
                    VotePk = votePk;
                    SelectionPk = selectionPk;
                    VoteFirst = voteFirst;
                    VoteLast = voteLast;
                    VoteKeyDilution = voteKeyDilution;
                    NonParticipation = default;
                }

                public bool Equals(Params other)
                {
                    return VotePk.Equals(other.VotePk)
                        && SelectionPk.Equals(other.SelectionPk)
                        && VoteFirst.Equals(other.VoteFirst)
                        && VoteLast.Equals(other.VoteLast)
                        && VoteKeyDilution.Equals(other.VoteKeyDilution)
                        && NonParticipation.Equals(other.NonParticipation)
                        ;
                }
            }
        }
    }
}
