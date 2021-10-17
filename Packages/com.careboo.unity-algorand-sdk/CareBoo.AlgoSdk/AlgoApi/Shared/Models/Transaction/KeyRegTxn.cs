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

        public static KeyRegTxn KeyRegOnline(
            Address account,
            TransactionParams txnParams,
            AccountParticipation accountParticipation
        )
        {
            var txn = new KeyRegTxn
            {
                header = new TransactionHeader(account, TransactionType.KeyRegistration, txnParams),
                AccountParticipation = accountParticipation
            };
            txn.Fee = txn.GetSuggestedFee(txnParams);
            return txn;
        }

        public static KeyRegTxn KeyRegOffline(
            Address account,
            TransactionParams txnParams
        )
        {
            var txn = new KeyRegTxn
            {
                header = new TransactionHeader(account, TransactionType.KeyRegistration, txnParams)
            };
            txn.Fee = txn.GetSuggestedFee(txnParams);
            return txn;
        }
    }

    [AlgoApiObject]
    public struct KeyRegTxn
           : ITransaction
           , IEquatable<KeyRegTxn>
    {
        internal TransactionHeader header;

        Params @params;

        public AccountParticipation AccountParticipation
        {
            get => @params.AccountParticipation;
            set => @params.AccountParticipation = value;
        }

        [AlgoApiField("fee", "fee")]
        public ulong Fee
        {
            get => header.Fee;
            set => header.Fee = value;
        }

        [AlgoApiField("first-valid", "fv")]
        public ulong FirstValidRound
        {
            get => header.FirstValidRound;
            set => header.FirstValidRound = value;
        }

        [AlgoApiField("genesis-hash", "gh")]
        public GenesisHash GenesisHash
        {
            get => header.GenesisHash;
            set => header.GenesisHash = value;
        }

        [AlgoApiField("last-valid", "lv")]
        public ulong LastValidRound
        {
            get => header.LastValidRound;
            set => header.LastValidRound = value;
        }

        [AlgoApiField("sender", "snd")]
        public Address Sender
        {
            get => header.Sender;
            set => header.Sender = value;
        }

        [AlgoApiField("tx-type", "type")]
        public TransactionType TransactionType
        {
            get => TransactionType.KeyRegistration;
            internal set => header.TransactionType = TransactionType.KeyRegistration;
        }

        [AlgoApiField("genesis-id", "gen")]
        public FixedString32Bytes GenesisId
        {
            get => header.GenesisId;
            set => header.GenesisId = value;
        }

        [AlgoApiField("group", "grp")]
        public Sha512_256_Hash Group
        {
            get => header.Group;
            set => header.Group = value;
        }

        [AlgoApiField("lease", "lx")]
        public Sha512_256_Hash Lease
        {
            get => header.Lease;
            set => header.Lease = value;
        }

        [AlgoApiField("note", "note")]
        public byte[] Note
        {
            get => header.Note;
            set => header.Note = value;
        }

        [AlgoApiField("rekey-to", "rekey")]
        public Address RekeyTo
        {
            get => header.RekeyTo;
            set => header.RekeyTo = value;
        }

        [AlgoApiField(null, "votekey")]
        public Ed25519.PublicKey VoteParticipationKey
        {
            get => @params.VoteParticipationKey;
            set => @params.VoteParticipationKey = value;
        }

        [AlgoApiField(null, "selkey")]
        public VrfPubKey SelectionParticipationKey
        {
            get => @params.SelectionParticipationKey;
            set => @params.SelectionParticipationKey = value;
        }

        [AlgoApiField(null, "votefst")]
        public ulong VoteFirst
        {
            get => @params.VoteFirst;
            set => @params.VoteFirst = value;
        }

        [AlgoApiField(null, "votelst")]
        public ulong VoteLast
        {
            get => @params.VoteLast;
            set => @params.VoteLast = value;
        }

        [AlgoApiField(null, "votekd")]
        public ulong VoteKeyDilution
        {
            get => @params.VoteKeyDilution;
            set => @params.VoteKeyDilution = value;
        }

        [AlgoApiField(null, "nonpart")]
        public Optional<bool> NonParticipation
        {
            get => @params.NonParticipation;
            set => @params.NonParticipation = value;
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
            return header.Equals(other.header)
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

            public bool Equals(Params other)
            {
                return SelectionParticipationKey.Equals(other.SelectionParticipationKey);
            }
        }
    }
}
