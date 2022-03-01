using System;
using AlgoSdk.Crypto;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace AlgoSdk
{
    public partial struct Transaction
    {
        [AlgoApiField("fee", "fee")]
        public ulong Fee
        {
            get => HeaderParams.Fee;
            set => HeaderParams.Fee = value;
        }

        [AlgoApiField("first-valid", "fv")]
        public ulong FirstValidRound
        {
            get => HeaderParams.FirstValidRound;
            set => HeaderParams.FirstValidRound = value;
        }

        [AlgoApiField("genesis-hash", "gh")]
        public GenesisHash GenesisHash
        {
            get => HeaderParams.GenesisHash;
            set => HeaderParams.GenesisHash = value;
        }

        [AlgoApiField("last-valid", "lv")]
        public ulong LastValidRound
        {
            get => HeaderParams.LastValidRound;
            set => HeaderParams.LastValidRound = value;
        }

        [AlgoApiField("sender", "snd")]
        public Address Sender
        {
            get => HeaderParams.Sender;
            set => HeaderParams.Sender = value;
        }

        [AlgoApiField("tx-type", "type")]
        public TransactionType TransactionType
        {
            get => HeaderParams.TransactionType;
            set => HeaderParams.TransactionType = value;
        }

        [AlgoApiField("genesis-id", "gen")]
        public FixedString32Bytes GenesisId
        {
            get => HeaderParams.GenesisId;
            set => HeaderParams.GenesisId = value;
        }

        [AlgoApiField("group", "grp")]
        public Sha512_256_Hash Group
        {
            get => HeaderParams.Group;
            set => HeaderParams.Group = value;
        }

        [AlgoApiField("lease", "lx")]
        public Sha512_256_Hash Lease
        {
            get => HeaderParams.Lease;
            set => HeaderParams.Lease = value;
        }

        [AlgoApiField("note", "note")]
        public byte[] Note
        {
            get => HeaderParams.Note;
            set => HeaderParams.Note = value;
        }

        [AlgoApiField("rekey-to", "rekey")]
        public Address RekeyTo
        {
            get => HeaderParams.RekeyTo;
            set => HeaderParams.RekeyTo = value;
        }

        [AlgoApiField("id", "id", readOnly: true)]
        public TransactionId Id
        {
            get => HeaderParams.Id;
            set => HeaderParams.Id = value;
        }

        [AlgoApiField("auth-addr", "sgnr", readOnly: true)]
        public Address AuthAddress
        {
            get => HeaderParams.AuthAddress;
            set => HeaderParams.AuthAddress = value;
        }

        [AlgoApiField("close-rewards", "rc", readOnly: true)]
        public ulong CloseRewards
        {
            get => HeaderParams.CloseRewards;
            set => HeaderParams.CloseRewards = value;
        }

        [AlgoApiField("closing-amount", "ca", readOnly: true)]
        public ulong ClosingAmount
        {
            get => HeaderParams.ClosingAmount;
            set => HeaderParams.ClosingAmount = value;
        }

        [AlgoApiField("confirmed-round", null, readOnly: true)]
        public ulong ConfirmedRound
        {
            get => HeaderParams.ConfirmedRound;
            set => HeaderParams.ConfirmedRound = value;
        }

        [AlgoApiField("created-application-index", null, readOnly: true)]
        public ulong CreatedApplicationIndex
        {
            get => HeaderParams.CreatedApplicationIndex;
            set => HeaderParams.CreatedApplicationIndex = value;
        }

        [AlgoApiField("created-asset-index", null, readOnly: true)]
        public ulong CreatedAssetIndex
        {
            get => HeaderParams.CreatedAssetIndex;
            set => HeaderParams.CreatedAssetIndex = value;
        }

        [AlgoApiField("intra-round-offset", null, readOnly: true)]
        public ulong IntraRoundOffset
        {
            get => HeaderParams.IntraRoundOffset;
            set => HeaderParams.IntraRoundOffset = value;
        }

        [AlgoApiField("global-state-delta", "gd", readOnly: true)]
        public EvalDeltaKeyValue[] GlobalStateDelta
        {
            get => HeaderParams.GlobalStateDelta;
            set => HeaderParams.GlobalStateDelta = value;
        }

        [AlgoApiField("local-state-delta", "ld", readOnly: true)]
        public AccountStateDelta[] LocalStateDelta
        {
            get => HeaderParams.LocalStateDelta;
            set => HeaderParams.LocalStateDelta = value;
        }

        [AlgoApiField("receiver-rewards", "rr", readOnly: true)]
        public ulong ReceiverRewards
        {
            get => HeaderParams.ReceiverRewards;
            set => HeaderParams.ReceiverRewards = value;
        }

        [AlgoApiField("round-time", null, readOnly: true)]
        public ulong RoundTime
        {
            get => HeaderParams.RoundTime;
            set => HeaderParams.RoundTime = value;
        }

        [AlgoApiField("sender-rewards", "rs", readOnly: true)]
        public ulong SenderRewards
        {
            get => HeaderParams.SenderRewards;
            set => HeaderParams.SenderRewards = value;
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
    {
        [Tooltip("Paid by the sender to the FeeSink to prevent denial-of-service. The minimum fee on Algorand is currently 1000 microAlgos.")]
        public ulong Fee;
        [Tooltip("The first round for when the transaction is valid. If the transaction is sent prior to this round it will be rejected by the network.")]
        public ulong FirstValidRound;
        [Tooltip("The hash of the genesis block of the network for which the transaction is valid.")]
        public GenesisHash GenesisHash;
        [Tooltip("The ending round for which the transaction is valid. After this round, the transaction will be rejected by the network.")]
        public ulong LastValidRound;
        [Tooltip("The address of the account that pays the fee and amount.")]
        public Address Sender;
        [Tooltip("Specifies the type of transaction. This value is automatically generated using any of the developer tools.")]
        public TransactionType TransactionType;

        [Tooltip("The human-readable string that identifies the network for the transaction. The genesis ID is found in the genesis block.")]
        public FixedString32Bytes GenesisId;
        [Tooltip("The group specifies that the transaction is part of a group and, if so, specifies the hash of the transaction group.")]
        public Sha512_256_Hash Group;
        [Tooltip("A lease enforces mutual exclusion of transactions. If this field is nonzero, then once the transaction is confirmed, it acquires the lease identified by the (Sender, Lease) pair of the transaction until the LastValid round passes. While this transaction possesses the lease, no other transaction specifying this lease can be confirmed. A lease is often used in the context of Algorand Smart Contracts to prevent replay attacks.")]
        public Sha512_256_Hash Lease;
        [Tooltip("Any data up to 1000 bytes.")]
        public byte[] Note;
        [Tooltip("Specifies the authorized address. This address will be used to authorize all future transactions.")]
        public Address RekeyTo;

        [NonSerialized]
        public TransactionId Id;
        [NonSerialized]
        public Address AuthAddress;
        [NonSerialized]
        public ulong CloseRewards;
        [NonSerialized]
        public ulong ClosingAmount;
        [NonSerialized]
        public ulong ConfirmedRound;
        [NonSerialized]
        public ulong CreatedApplicationIndex;
        [NonSerialized]
        public ulong CreatedAssetIndex;
        [NonSerialized]
        public ulong IntraRoundOffset;
        [NonSerialized]
        public EvalDeltaKeyValue[] GlobalStateDelta;
        [NonSerialized]
        public AccountStateDelta[] LocalStateDelta;
        [NonSerialized]
        public ulong ReceiverRewards;
        [NonSerialized]
        public ulong RoundTime;
        [NonSerialized]
        public ulong SenderRewards;
        [NonSerialized]
        public OnCompletion OnCompletion;


        public TransactionHeader(
            Address sender,
            TransactionType transactionType,
            TransactionParams transactionParams
        )
        {
            Fee = math.max(transactionParams.Fee, transactionParams.MinFee);
            FirstValidRound = transactionParams.FirstValidRound;
            GenesisHash = transactionParams.GenesisHash;
            LastValidRound = transactionParams.LastValidRound;
            Sender = sender;
            TransactionType = transactionType;

            GenesisId = transactionParams.GenesisId;
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

        public bool Equals(TransactionHeader other)
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
