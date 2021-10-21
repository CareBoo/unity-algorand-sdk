using System;
using AlgoSdk.Crypto;
using Unity.Collections;
using Unity.Mathematics;

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
        public Sha512_256_Hash Id
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
        public ApplicationStateDelta[] LocalStateDelta
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

    public struct TransactionHeader
        : IEquatable<TransactionHeader>
    {
        public ulong Fee;
        public ulong FirstValidRound;
        public GenesisHash GenesisHash;
        public ulong LastValidRound;
        public Address Sender;
        public TransactionType TransactionType;

        public FixedString32Bytes GenesisId;
        public Sha512_256_Hash Group;
        public Sha512_256_Hash Lease;
        public byte[] Note;
        public Address RekeyTo;

        public Sha512_256_Hash Id;
        public Address AuthAddress;
        public ulong CloseRewards;
        public ulong ClosingAmount;
        public ulong ConfirmedRound;
        public ulong CreatedApplicationIndex;
        public ulong CreatedAssetIndex;
        public ulong IntraRoundOffset;
        public EvalDeltaKeyValue[] GlobalStateDelta;
        public ApplicationStateDelta[] LocalStateDelta;
        public ulong ReceiverRewards;
        public ulong RoundTime;
        public ulong SenderRewards;
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
