using System;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace Algorand.Unity
{
    public interface ITransactionHeader
    {
        /// <summary>
        /// Paid by the sender to the FeeSink to prevent denial-of-service. The minimum fee on Algorand is currently 1000 microAlgos.
        /// </summary>
        MicroAlgos Fee { get; set; }

        /// <summary>
        /// The first round for when the transaction is valid. If the transaction is sent prior to this round it will be rejected by the network.
        /// </summary>
        ulong FirstValidRound { get; set; }

        /// <summary>
        /// The hash of the genesis block of the network for which the transaction is valid.
        /// </summary>
        GenesisHash GenesisHash { get; set; }

        /// <summary>
        /// The ending round for which the transaction is valid. After this round, the transaction will be rejected by the network.
        /// </summary>
        ulong LastValidRound { get; set; }

        /// <summary>
        /// The address of the account that pays the fee and amount.
        /// </summary>
        Address Sender { get; set; }

        /// <summary>
        /// Specifies the type of transaction. This value is automatically generated using any of the developer tools.
        /// </summary>
        TransactionType TransactionType { get; }

        /// <summary>
        /// The human-readable string that identifies the network for the transaction. The genesis ID is found in the genesis block.
        /// </summary>
        FixedString32Bytes GenesisId { get; set; }

        /// <summary>
        /// The group specifies that the transaction is part of a group and, if so, specifies the hash of the transaction group. See <see cref="TransactionGroup"/>.
        /// </summary>
        TransactionId Group { get; set; }

        /// <summary>
        /// A lease enforces mutual exclusion of transactions. If this field is nonzero, then once the transaction is confirmed, it acquires the lease identified by the (Sender, Lease) pair of the transaction until the LastValid round passes. While this transaction possesses the lease, no other transaction specifying this lease can be confirmed. A lease is often used in the context of Algorand Smart Contracts to prevent replay attacks.
        /// </summary>
        TransactionId Lease { get; set; }

        /// <summary>
        /// Any data up to 1000 bytes.
        /// </summary>
        byte[] Note { get; set; }

        /// <summary>
        /// Specifies the authorized address. This address will be used to authorize all future transactions.
        /// </summary>
        Address RekeyTo { get; set; }
    }

    public partial struct Transaction : ITransactionHeader
    {
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
            get => header.TransactionType;
            set => header.TransactionType = value;
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
    }

    /// <summary>
    /// Contains parameters used in all transaction types.
    /// </summary>
    /// <remarks>
    /// For the most part, this is used internally in the sdk and shouldn't be used directly.
    /// </remarks>
    [Serializable]
    public struct TransactionHeader
        : IEquatable<TransactionHeader>
        , ITransactionHeader
    {
        [SerializeField, Tooltip("Paid by the sender to the FeeSink to prevent denial-of-service. The minimum fee on Algorand is currently 1000 microAlgos.")]
        private MicroAlgos fee;

        [SerializeField, Tooltip("The first round for when the transaction is valid. If the transaction is sent prior to this round it will be rejected by the network.")]
        private ulong firstValidRound;

        [SerializeField, Tooltip("The hash of the genesis block of the network for which the transaction is valid.")]
        private GenesisHash genesisHash;

        [SerializeField, Tooltip("The ending round for which the transaction is valid. After this round, the transaction will be rejected by the network.")]
        private ulong lastValidRound;

        [SerializeField, Tooltip("The address of the account that pays the fee and amount.")]
        private Address sender;

        [SerializeField, Tooltip("Specifies the type of transaction. This value is automatically generated using any of the developer tools.")]
        private TransactionType transactionType;

        [SerializeField, Tooltip("The human-readable string that identifies the network for the transaction. The genesis ID is found in the genesis block.")]
        private FixedString32Bytes genesisId;

        [SerializeField, Tooltip("The group specifies that the transaction is part of a group and, if so, specifies the hash of the transaction group.")]
        private TransactionId group;

        [SerializeField, Tooltip("A lease enforces mutual exclusion of transactions. If this field is nonzero, then once the transaction is confirmed, it acquires the lease identified by the (Sender, Lease) pair of the transaction until the LastValid round passes. While this transaction possesses the lease, no other transaction specifying this lease can be confirmed. A lease is often used in the context of Algorand Smart Contracts to prevent replay attacks.")]
        private TransactionId lease;

        [SerializeField, Tooltip("Any data up to 1000 bytes.")]
        private byte[] note;

        [SerializeField, Tooltip("Specifies the authorized address. This address will be used to authorize all future transactions.")]
        private Address rekeyTo;

        /// <inheritdoc />
        public MicroAlgos Fee
        {
            get => fee;
            set => fee = value;
        }

        /// <inheritdoc />
        public ulong FirstValidRound
        {
            get => firstValidRound;
            set => firstValidRound = value;
        }

        /// <inheritdoc />
        public GenesisHash GenesisHash
        {
            get => genesisHash;
            set => genesisHash = value;
        }

        /// <inheritdoc />
        public ulong LastValidRound
        {
            get => lastValidRound;
            set => lastValidRound = value;
        }

        /// <inheritdoc />
        public Address Sender
        {
            get => sender;
            set => sender = value;
        }

        /// <inheritdoc />
        public TransactionType TransactionType
        {
            get => transactionType;
            set => transactionType = value;
        }

        /// <inheritdoc />
        public FixedString32Bytes GenesisId
        {
            get => genesisId;
            set => genesisId = value;
        }

        /// <inheritdoc />
        public TransactionId Group
        {
            get => group;
            set => group = value;
        }

        /// <inheritdoc />
        public TransactionId Lease
        {
            get => lease;
            set => lease = value;
        }

        /// <inheritdoc />
        public byte[] Note
        {
            get => note;
            set => note = value;
        }

        /// <inheritdoc />
        public Address RekeyTo
        {
            get => rekeyTo;
            set => rekeyTo = value;
        }

        public TransactionHeader(
            Address sender,
            TransactionType transactionType,
            TransactionParams transactionParams
        )
        {
            fee = math.max(transactionParams.Fee, transactionParams.MinFee);
            firstValidRound = transactionParams.FirstValidRound;
            genesisHash = transactionParams.GenesisHash;
            lastValidRound = transactionParams.LastValidRound;
            this.sender = sender;
            this.transactionType = transactionType;

            genesisId = transactionParams.GenesisId;
            group = default;
            lease = default;
            note = default;
            rekeyTo = default;
        }

        public bool Equals(TransactionHeader other)
        {
            return Fee == other.Fee
                && FirstValidRound == other.FirstValidRound
                && GenesisHash.Equals(other.GenesisHash)
                && LastValidRound == other.LastValidRound
                && Sender == other.Sender
                && TransactionType == other.TransactionType
                && GenesisId == other.GenesisId
                && Group.Equals(other.Group)
                && Lease.Equals(other.Lease)
                && string.Equals(Note, other.Note)
                && RekeyTo == other.RekeyTo
                ;
        }

        public override bool Equals(object obj)
        {
            if (obj is TransactionHeader other)
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
