using System;
using AlgoSdk.Crypto;
using Unity.Collections;

namespace AlgoSdk
{
    public interface IKeyRegTxn : ITransaction
    {
        /// <summary>
        /// The root participation public key.
        /// </summary>
        Ed25519.PublicKey VoteParticipationKey { get; set; }

        /// <summary>
        /// The VRF public key.
        /// </summary>
        VrfPubKey SelectionParticipationKey { get; set; }

        /// <summary>
        /// The first round that the participation key is valid. Not to be confused with the <see cref="ITransaction.FirstValidRound"/> of the keyreg transaction.
        /// </summary>
        ulong VoteFirst { get; set; }

        /// <summary>
        /// The last round that the participation key is valid. Not to be confused with the <see cref="ITransaction.LastValidRound"/> of the keyreg transaction.
        /// </summary>
        ulong VoteLast { get; set; }

        /// <summary>
        /// This is the dilution for the 2-level participation key.
        /// </summary>
        ulong VoteKeyDilution { get; set; }

        /// <summary>
        /// All new Algorand accounts are participating by default. This means that they earn rewards. Mark an account nonparticipating by setting this value to <c>true</c> and this account will no longer earn rewards. It is unlikely that you will ever need to do this and exists mainly for economic-related functions on the network.
        /// </summary>
        Optional<bool> NonParticipation { get; set; }
    }

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

        /// <summary>
        /// Register account online for participation.
        /// </summary>
        /// <param name="account">Account to register online. This is the sender of the transaction.</param>
        /// <param name="txnParams">See <see cref="TransactionParams"/></param>
        /// <param name="accountParticipation">See <see cref="AccountParticipation"/></param>
        /// <returns>A <see cref="KeyRegTxn"/>.</returns>
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

        /// <summary>
        /// Register account offline for participation.
        /// </summary>
        /// <param name="account">Account to register offline. This is the sender of the transaction.</param>
        /// <param name="txnParams">See <see cref="TransactionParams"/></param>
        /// <returns>A <see cref="KeyRegTxn"/>.</returns>
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
           : IKeyRegTxn
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
