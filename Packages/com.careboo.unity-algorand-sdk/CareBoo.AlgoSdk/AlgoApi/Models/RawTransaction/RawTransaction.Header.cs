using Unity.Collections;

namespace AlgoSdk
{
    public partial struct RawTransaction
    {
        [AlgoApiField("fee", "fee")]
        public ulong Fee
        {
            get => Header.Fee;
            set => Header.Fee = value;
        }

        [AlgoApiField("first-valid", "fv")]
        public ulong FirstValidRound
        {
            get => Header.FirstValidRound;
            set => Header.FirstValidRound = value;
        }

        [AlgoApiField("genesis-hash", "gh")]
        public GenesisHash GenesisHash
        {
            get => Header.GenesisHash;
            set => Header.GenesisHash = value;
        }

        [AlgoApiField("last-valid", "lv")]
        public ulong LastValidRound
        {
            get => Header.LastValidRound;
            set => Header.LastValidRound = value;
        }

        [AlgoApiField("sender", "snd")]
        public Address Sender
        {
            get => Header.Sender;
            set => Header.Sender = value;
        }

        [AlgoApiField("tx-type", "type")]
        public TransactionType TransactionType
        {
            get => Header.TransactionType;
            set => Header.TransactionType = value;
        }

        [AlgoApiField("genesis-id", "gen")]
        public FixedString32Bytes GenesisId
        {
            get => Header.GenesisId;
            set => Header.GenesisId = value;
        }

        [AlgoApiField("group", "grp")]
        public Address Group
        {
            get => Header.Group;
            set => Header.Group = value;
        }

        [AlgoApiField("lease", "lx")]
        public Address Lease
        {
            get => Header.Lease;
            set => Header.Lease = value;
        }

        [AlgoApiField("note", "note")]
        public byte[] Note
        {
            get => Header.Note;
            set => Header.Note = value;
        }

        [AlgoApiField("rekey-to", "rekey")]
        public Address RekeyTo
        {
            get => Header.RekeyTo;
            set => Header.RekeyTo = value;
        }

        [AlgoApiField("id", "id", readOnly: true)]
        public TransactionId Id
        {
            get => Header.Id;
            set => Header.Id = value;
        }

        [AlgoApiField("auth-addr", "sgnr", readOnly: true)]
        public Address AuthAddress
        {
            get => Header.AuthAddress;
            set => Header.AuthAddress = value;
        }

        [AlgoApiField("close-rewards", "rc", readOnly: true)]
        public ulong CloseRewards
        {
            get => Header.CloseRewards;
            set => Header.CloseRewards = value;
        }

        [AlgoApiField("closing-amount", "ca", readOnly: true)]
        public ulong ClosingAmount
        {
            get => Header.ClosingAmount;
            set => Header.ClosingAmount = value;
        }

        [AlgoApiField("confirmed-round", null, readOnly: true)]
        public ulong ConfirmedRound
        {
            get => Header.ConfirmedRound;
            set => Header.ConfirmedRound = value;
        }

        [AlgoApiField("created-application-index", null, readOnly: true)]
        public ulong CreatedApplicationIndex
        {
            get => Header.CreatedApplicationIndex;
            set => Header.CreatedApplicationIndex = value;
        }

        [AlgoApiField("created-asset-index", null, readOnly: true)]
        public ulong CreatedAssetIndex
        {
            get => Header.CreatedAssetIndex;
            set => Header.CreatedAssetIndex = value;
        }

        [AlgoApiField("intra-round-offset", null, readOnly: true)]
        public ulong IntraRoundOffset
        {
            get => Header.IntraRoundOffset;
            set => Header.IntraRoundOffset = value;
        }

        [AlgoApiField("global-state-delta", "gd", readOnly: true)]
        public EvalDeltaKeyValue[] GlobalStateDelta
        {
            get => Header.GlobalStateDelta;
            set => Header.GlobalStateDelta = value;
        }

        [AlgoApiField("local-state-delta", "ld", readOnly: true)]
        public AccountStateDelta[] LocalStateDelta
        {
            get => Header.LocalStateDelta;
            set => Header.LocalStateDelta = value;
        }

        [AlgoApiField("receiver-rewards", "rr", readOnly: true)]
        public ulong ReceiverRewards
        {
            get => Header.ReceiverRewards;
            set => Header.ReceiverRewards = value;
        }

        [AlgoApiField("round-time", null, readOnly: true)]
        public ulong RoundTime
        {
            get => Header.RoundTime;
            set => Header.RoundTime = value;
        }

        [AlgoApiField("sender-rewards", "rs", readOnly: true)]
        public ulong SenderRewards
        {
            get => Header.SenderRewards;
            set => Header.SenderRewards = value;
        }
    }
}
