using System;
using AlgoSdk.Crypto;
using Unity.Collections;

namespace AlgoSdk
{
    public partial struct Transaction
    {
        [AlgoApiField(null, "votekey")]
        public Ed25519.PublicKey VoteParticipationKey
        {
            get => KeyRegistrationParams.VoteParticipationKey;
            set => KeyRegistrationParams.VoteParticipationKey = value;
        }

        [AlgoApiField(null, "selkey")]
        public VrfPubKey SelectionParticipationKey
        {
            get => KeyRegistrationParams.SelectionParticipationKey;
            set => KeyRegistrationParams.SelectionParticipationKey = value;
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

        public static KeyRegTxn RegisterAccountOnline(
            Address account,
            TransactionParams txnParams,
            AccountParticipation accountParticipation
        )
        {
            return new KeyRegTxn(account, txnParams, accountParticipation);
        }

        public static KeyRegTxn RegisterAccountOffline(
            Address account,
            TransactionParams txnParams
        )
        {
            return new KeyRegTxn(account, txnParams);
        }
    }

    public struct KeyRegTxn
           : ITransaction
           , IEquatable<KeyRegTxn>
    {
        TransactionHeader header;

        Params @params;

        public TransactionHeader Header
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

        public Ed25519.PublicKey VoteParticipationKey
        {
            get => @params.VoteParticipationKey;
            set => @params.VoteParticipationKey = value;
        }

        public VrfPubKey SelectionParticipationKey
        {
            get => @params.SelectionParticipationKey;
            set => @params.SelectionParticipationKey = value;
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

        public KeyRegTxn(
             Address sender,
             TransactionParams txnParams
        )
        {
            header = new TransactionHeader(
                sender,
                TransactionType.KeyRegistration,
                txnParams
            );
            @params = new Params();
        }

        public KeyRegTxn(
            Address sender,
            TransactionParams txnParams,
            AccountParticipation accountParticipation
        )
        {
            header = new TransactionHeader(
                sender,
                TransactionType.KeyRegistration,
                txnParams
            );
            @params = new Params(accountParticipation);
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

        public bool Equals(KeyRegTxn other)
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
            public Ed25519.PublicKey VoteParticipationKey
            {
                get => AccountParticipation.VoteParticipationKey;
                set => AccountParticipation.VoteParticipationKey = value;
            }

            [AlgoApiField("selection-participation-key", "selkey")]
            public VrfPubKey SelectionParticipationKey
            {
                get => AccountParticipation.SelectionParticipationKey;
                set => AccountParticipation.SelectionParticipationKey = value;
            }

            [AlgoApiField("vote-first-valid", "votefst")]
            public ulong VoteFirst
            {
                get => AccountParticipation.VoteFirst;
                set => AccountParticipation.VoteFirst = value;
            }

            [AlgoApiField("vote-last-valid", "votelst")]
            public ulong VoteLast
            {
                get => AccountParticipation.VoteLast;
                set => AccountParticipation.VoteLast = value;
            }

            [AlgoApiField("vote-key-dilution", "votekd")]
            public ulong VoteKeyDilution
            {
                get => AccountParticipation.VoteKeyDilution;
                set => AccountParticipation.VoteKeyDilution = value;
            }

            [AlgoApiField("non-participation", "nonpart")]
            public Optional<bool> NonParticipation;

            public AccountParticipation AccountParticipation;

            public Params(
                AccountParticipation accountParticipation
            )
            {
                AccountParticipation = accountParticipation;
                NonParticipation = default;
            }

            public bool Equals(Params other)
            {
                return SelectionParticipationKey.Equals(other.SelectionParticipationKey);
            }
        }
    }
}
