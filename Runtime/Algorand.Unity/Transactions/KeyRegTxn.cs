using System;
using Unity.Collections;
using UnityEngine;

namespace Algorand.Unity
{
    public interface IKeyRegTxn : ITransaction
    {
        /// <summary>
        /// The root participation public key.
        /// </summary>
        ParticipationPublicKey VoteParticipationKey { get; set; }

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
        [AlgoApiField("votekey")]
        public ParticipationPublicKey VoteParticipationKey
        {
            get => keyRegParams.VoteParticipationKey;
            set => keyRegParams.VoteParticipationKey = value;
        }

        /// <inheritdoc />
        [AlgoApiField("selkey")]
        public VrfPubKey SelectionParticipationKey
        {
            get => keyRegParams.SelectionParticipationKey;
            set => keyRegParams.SelectionParticipationKey = value;
        }

        /// <inheritdoc />
        [AlgoApiField("votefst")]
        public ulong VoteFirst
        {
            get => keyRegParams.VoteFirst;
            set => keyRegParams.VoteFirst = value;
        }

        /// <inheritdoc />
        [AlgoApiField("votelst")]
        public ulong VoteLast
        {
            get => keyRegParams.VoteLast;
            set => keyRegParams.VoteLast = value;
        }

        /// <inheritdoc />
        [AlgoApiField("votekd")]
        public ulong VoteKeyDilution
        {
            get => keyRegParams.VoteKeyDilution;
            set => keyRegParams.VoteKeyDilution = value;
        }

        /// <inheritdoc />
        [AlgoApiField("nonpart")]
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

        [SerializeField] private Params @params;

        /// <inheritdoc />
        public AccountParticipation AccountParticipation
        {
            get => @params.AccountParticipation;
            set => @params.AccountParticipation = value;
        }

        /// <inheritdoc />
        [AlgoApiField("fee")]
        public MicroAlgos Fee
        {
            get => header.Fee;
            set => header.Fee = value;
        }

        /// <inheritdoc />
        [AlgoApiField("fv")]
        public ulong FirstValidRound
        {
            get => header.FirstValidRound;
            set => header.FirstValidRound = value;
        }

        /// <inheritdoc />
        [AlgoApiField("gh")]
        public GenesisHash GenesisHash
        {
            get => header.GenesisHash;
            set => header.GenesisHash = value;
        }

        /// <inheritdoc />
        [AlgoApiField("lv")]
        public ulong LastValidRound
        {
            get => header.LastValidRound;
            set => header.LastValidRound = value;
        }

        /// <inheritdoc />
        [AlgoApiField("snd")]
        public Address Sender
        {
            get => header.Sender;
            set => header.Sender = value;
        }

        /// <inheritdoc />
        [AlgoApiField("type")]
        public TransactionType TransactionType
        {
            get => TransactionType.KeyRegistration;
            internal set => header.TransactionType = TransactionType.KeyRegistration;
        }

        /// <inheritdoc />
        [AlgoApiField("gen")]
        public FixedString32Bytes GenesisId
        {
            get => header.GenesisId;
            set => header.GenesisId = value;
        }

        /// <inheritdoc />
        [AlgoApiField("grp")]
        public TransactionId Group
        {
            get => header.Group;
            set => header.Group = value;
        }

        /// <inheritdoc />
        [AlgoApiField("lx")]
        public TransactionId Lease
        {
            get => header.Lease;
            set => header.Lease = value;
        }

        /// <inheritdoc />
        [AlgoApiField("note")]
        public byte[] Note
        {
            get => header.Note;
            set => header.Note = value;
        }

        /// <inheritdoc />
        [AlgoApiField("rekey")]
        public Address RekeyTo
        {
            get => header.RekeyTo;
            set => header.RekeyTo = value;
        }

        /// <inheritdoc />
        [AlgoApiField("votekey")]
        public ParticipationPublicKey VoteParticipationKey
        {
            get => @params.VoteParticipationKey;
            set => @params.VoteParticipationKey = value;
        }

        /// <inheritdoc />
        [AlgoApiField("selkey")]
        public VrfPubKey SelectionParticipationKey
        {
            get => @params.SelectionParticipationKey;
            set => @params.SelectionParticipationKey = value;
        }

        /// <inheritdoc />
        [AlgoApiField("votefst")]
        public ulong VoteFirst
        {
            get => @params.VoteFirst;
            set => @params.VoteFirst = value;
        }

        /// <inheritdoc />
        [AlgoApiField("votelst")]
        public ulong VoteLast
        {
            get => @params.VoteLast;
            set => @params.VoteLast = value;
        }

        /// <inheritdoc />
        [AlgoApiField("votekd")]
        public ulong VoteKeyDilution
        {
            get => @params.VoteKeyDilution;
            set => @params.VoteKeyDilution = value;
        }

        /// <inheritdoc />
        [AlgoApiField("nonpart")]
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
            [AlgoApiField("votekey")]
            public ParticipationPublicKey VoteParticipationKey
            {
                get => AccountParticipation.VoteParticipationKey;
                set => AccountParticipation.VoteParticipationKey = value;
            }

            [AlgoApiField("selkey")]
            public VrfPubKey SelectionParticipationKey
            {
                get => AccountParticipation.SelectionParticipationKey;
                set => AccountParticipation.SelectionParticipationKey = value;
            }

            [AlgoApiField("votefst")]
            public ulong VoteFirst
            {
                get => AccountParticipation.VoteFirst;
                set => AccountParticipation.VoteFirst = value;
            }

            [AlgoApiField("votelst")]
            public ulong VoteLast
            {
                get => AccountParticipation.VoteLast;
                set => AccountParticipation.VoteLast = value;
            }

            [AlgoApiField("votekd")]
            public ulong VoteKeyDilution
            {
                get => AccountParticipation.VoteKeyDilution;
                set => AccountParticipation.VoteKeyDilution = value;
            }

            [AlgoApiField("nonpart")]
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
