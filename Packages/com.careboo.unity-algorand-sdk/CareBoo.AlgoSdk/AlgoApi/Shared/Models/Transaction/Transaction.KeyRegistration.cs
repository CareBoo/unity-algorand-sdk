using System;
using Unity.Collections;

namespace AlgoSdk
{
    public partial struct Transaction
    {
        [AlgoApiField(null, "votekey")]
        public Address VotePk
        {
            get => KeyRegistrationParams.VotePk;
            set => KeyRegistrationParams.VotePk = value;
        }

        [AlgoApiField(null, "selkey")]
        public FixedString128Bytes SelectionPk
        {
            get => KeyRegistrationParams.SelectionPk;
            set => KeyRegistrationParams.SelectionPk = value;
        }

        [AlgoApiField(null, "votefst")]
        public ulong VoteFirst
        {
            get => KeyRegistrationParams.VoteFirst;
            set => KeyRegistrationParams.VoteFirst = value;
        }

        [AlgoApiField(null, "votelst")]
        public ulong VoteLast
        {
            get => KeyRegistrationParams.VoteLast;
            set => KeyRegistrationParams.VoteLast = value;
        }

        [AlgoApiField(null, "votekd")]
        public ulong VoteKeyDilution
        {
            get => KeyRegistrationParams.VoteKeyDilution;
            set => KeyRegistrationParams.VoteKeyDilution = value;
        }

        [AlgoApiField(null, "nonpart")]
        public Optional<bool> NonParticipation
        {
            get => KeyRegistrationParams.NonParticipation;
            set => KeyRegistrationParams.NonParticipation = value;
        }

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

            public FixedString128Bytes SelectionPk
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
                 Address sender,
                 TransactionParams txnParams
            )
            {
                header = new Header(
                    sender,
                    TransactionType.KeyRegistration,
                    txnParams
                );
                @params = new Params();
            }

            public void CopyTo(ref Transaction transaction)
            {
                transaction.HeaderParams = header;
                transaction.KeyRegistrationParams = @params;
            }

            public void CopyFrom(Transaction transaction)
            {
                header = transaction.HeaderParams;
                @params = transaction.KeyRegistrationParams;
            }

            public bool Equals(KeyRegistration other)
            {
                return header.Equals(other.Header)
                    && @params.Equals(other.@params)
                    ;
            }

            [AlgoApiObject]
            public struct Params
                : IEquatable<Params>
            {
                [AlgoApiField("vote-participation-key", "votekey")]
                public Address VotePk;

                [AlgoApiField("selection-participation-key", "selkey")]
                public FixedString128Bytes SelectionPk;

                [AlgoApiField("vote-first-valid", "votefst")]
                public ulong VoteFirst;

                [AlgoApiField("vote-last-valid", "votelst")]
                public ulong VoteLast;

                [AlgoApiField("vote-key-dilution", "votekd")]
                public ulong VoteKeyDilution;

                [AlgoApiField("non-participation", "nonpart")]
                public Optional<bool> NonParticipation;

                public Params(
                    Address votePk,
                    FixedString128Bytes selectionPk,
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
