using System;
using AlgoSdk.Crypto;
using Unity.Collections;
using UnityEngine;

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
        /// <inheritdoc />
        [AlgoApiField(null, "votekey")]
        public Ed25519.PublicKey VoteParticipationKey
        {
            get => keyRegParams.VoteParticipationKey;
            set => keyRegParams.VoteParticipationKey = value;
        }

        /// <inheritdoc />
        [AlgoApiField(null, "selkey")]
        public VrfPubKey SelectionParticipationKey
        {
            get => keyRegParams.SelectionParticipationKey;
            set => keyRegParams.SelectionParticipationKey = value;
        }

        /// <inheritdoc />
        [AlgoApiField(null, "votefst")]
        public ulong VoteFirst
        {
            get => keyRegParams.VoteFirst;
            set => keyRegParams.VoteFirst = value;
        }

        /// <inheritdoc />
        [AlgoApiField(null, "votelst")]
        public ulong VoteLast
        {
            get => keyRegParams.VoteLast;
            set => keyRegParams.VoteLast = value;
        }

        /// <inheritdoc />
        [AlgoApiField(null, "votekd")]
        public ulong VoteKeyDilution
        {
            get => keyRegParams.VoteKeyDilution;
            set => keyRegParams.VoteKeyDilution = value;
        }

        /// <inheritdoc />
        [AlgoApiField(null, "nonpart")]
        public Optional<bool> NonParticipation
        {
            get => keyRegParams.NonParticipation;
            set => keyRegParams.NonParticipation = value;
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
    [Serializable]
    public partial struct KeyRegTxn
           : IKeyRegTxn
           , IEquatable<KeyRegTxn>
    {
        [SerializeField]
        internal TransactionHeader header;

        [SerializeField]
        Params @params;

        /// <inheritdoc />
        public AccountParticipation AccountParticipation
        {
            get => @params.AccountParticipation;
            set => @params.AccountParticipation = value;
        }

        /// <inheritdoc />
        [AlgoApiField("fee", "fee")]
        public MicroAlgos Fee
        {
            get => header.Fee;
            set => header.Fee = value;
        }

        /// <inheritdoc />
        [AlgoApiField("first-valid", "fv")]
        public ulong FirstValidRound
        {
            get => header.FirstValidRound;
            set => header.FirstValidRound = value;
        }

        /// <inheritdoc />
        [AlgoApiField("genesis-hash", "gh")]
        public GenesisHash GenesisHash
        {
            get => header.GenesisHash;
            set => header.GenesisHash = value;
        }

        /// <inheritdoc />
        [AlgoApiField("last-valid", "lv")]
        public ulong LastValidRound
        {
            get => header.LastValidRound;
            set => header.LastValidRound = value;
        }

        /// <inheritdoc />
        [AlgoApiField("sender", "snd")]
        public Address Sender
        {
            get => header.Sender;
            set => header.Sender = value;
        }

        /// <inheritdoc />
        [AlgoApiField("tx-type", "type")]
        public TransactionType TransactionType
        {
            get => TransactionType.KeyRegistration;
            internal set => header.TransactionType = TransactionType.KeyRegistration;
        }

        /// <inheritdoc />
        [AlgoApiField("genesis-id", "gen")]
        public FixedString32Bytes GenesisId
        {
            get => header.GenesisId;
            set => header.GenesisId = value;
        }

        /// <inheritdoc />
        [AlgoApiField("group", "grp")]
        public TransactionId Group
        {
            get => header.Group;
            set => header.Group = value;
        }

        /// <inheritdoc />
        [AlgoApiField("lease", "lx")]
        public TransactionId Lease
        {
            get => header.Lease;
            set => header.Lease = value;
        }

        /// <inheritdoc />
        [AlgoApiField("note", "note")]
        public byte[] Note
        {
            get => header.Note;
            set => header.Note = value;
        }

        /// <inheritdoc />
        [AlgoApiField("rekey-to", "rekey")]
        public Address RekeyTo
        {
            get => header.RekeyTo;
            set => header.RekeyTo = value;
        }

        /// <inheritdoc />
        [AlgoApiField(null, "votekey")]
        public Ed25519.PublicKey VoteParticipationKey
        {
            get => @params.VoteParticipationKey;
            set => @params.VoteParticipationKey = value;
        }

        /// <inheritdoc />
        [AlgoApiField(null, "selkey")]
        public VrfPubKey SelectionParticipationKey
        {
            get => @params.SelectionParticipationKey;
            set => @params.SelectionParticipationKey = value;
        }

        /// <inheritdoc />
        [AlgoApiField(null, "votefst")]
        public ulong VoteFirst
        {
            get => @params.VoteFirst;
            set => @params.VoteFirst = value;
        }

        /// <inheritdoc />
        [AlgoApiField(null, "votelst")]
        public ulong VoteLast
        {
            get => @params.VoteLast;
            set => @params.VoteLast = value;
        }

        /// <inheritdoc />
        [AlgoApiField(null, "votekd")]
        public ulong VoteKeyDilution
        {
            get => @params.VoteKeyDilution;
            set => @params.VoteKeyDilution = value;
        }

        /// <inheritdoc />
        [AlgoApiField(null, "nonpart")]
        public Optional<bool> NonParticipation
        {
            get => @params.NonParticipation;
            set => @params.NonParticipation = value;
        }

        /// <inheritdoc />
        public void CopyTo(ref Transaction transaction)
        {
            transaction.Header = header;
            transaction.KeyRegParams = @params;
        }

        /// <inheritdoc />
        public void CopyFrom(Transaction transaction)
        {
            header = transaction.Header;
            @params = transaction.KeyRegParams;
        }

        public bool Equals(KeyRegTxn other)
        {
            return header.Equals(other.header)
                && @params.Equals(other.@params)
                ;
        }

        [AlgoApiObject]
        [Serializable]
        public partial struct Params
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
            [Tooltip("All new Algorand accounts are participating by default. This means that they earn rewards. Mark an account nonparticipating by setting this value to <c>true</c> and this account will no longer earn rewards. It is unlikely that you will ever need to do this and exists mainly for economic-related functions on the network.")]
            public Optional<bool> NonParticipation;

            public AccountParticipation AccountParticipation;

            public bool Equals(Params other)
            {
                return SelectionParticipationKey.Equals(other.SelectionParticipationKey);
            }
        }
    }
}
