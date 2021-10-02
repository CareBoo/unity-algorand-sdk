using Unity.Collections;

namespace AlgoSdk
{
    public partial struct RawTransaction
    {
        [AlgoApiKey("fee", "fee")]
        public ulong Fee
        {
            get => Header.Fee;
            set => Header.Fee = value;
        }

        [AlgoApiKey("first-valid", "fv")]
        public ulong FirstValidRound
        {
            get => Header.FirstValidRound;
            set => Header.FirstValidRound = value;
        }

        [AlgoApiKey("genesis-hash", "gh")]
        public GenesisHash GenesisHash
        {
            get => Header.GenesisHash;
            set => Header.GenesisHash = value;
        }

        [AlgoApiKey("last-valid", "lv")]
        public ulong LastValidRound
        {
            get => Header.LastValidRound;
            set => Header.LastValidRound = value;
        }

        [AlgoApiKey("sender", "snd")]
        public Address Sender
        {
            get => Header.Sender;
            set => Header.Sender = value;
        }

        [AlgoApiKey("tx-type", "type")]
        public TransactionType TransactionType
        {
            get => Header.TransactionType;
            set => Header.TransactionType = value;
        }

        [AlgoApiKey("genesis-id", "gen")]
        public FixedString32Bytes GenesisId
        {
            get => Header.GenesisId;
            set => Header.GenesisId = value;
        }

        [AlgoApiKey("group", "grp")]
        public Address Group
        {
            get => Header.Group;
            set => Header.Group = value;
        }

        [AlgoApiKey("lease", "lx")]
        public Address Lease
        {
            get => Header.Lease;
            set => Header.Lease = value;
        }

        [AlgoApiKey("note", "note")]
        public byte[] Note
        {
            get => Header.Note;
            set => Header.Note = value;
        }

        [AlgoApiKey("rekey-to", "rekey")]
        public Address RekeyTo
        {
            get => Header.RekeyTo;
            set => Header.RekeyTo = value;
        }

        [AlgoApiKey("id", null)]
        public TransactionId Id
        {
            get => Header.Id;
            set => Header.Id = value;
        }

        [AlgoApiKey("auth-addr", null)]
        public Address AuthAddress
        {
            get => Header.AuthAddress;
            set => Header.AuthAddress = value;
        }

        [AlgoApiKey("close-rewards", null)]
        public ulong CloseRewards
        {
            get => Header.CloseRewards;
            set => Header.CloseRewards = value;
        }

        [AlgoApiKey("closing-amount", null)]
        public ulong ClosingAmount
        {
            get => Header.ClosingAmount;
            set => Header.ClosingAmount = value;
        }

        [AlgoApiKey("confirmed-round", null)]
        public ulong ConfirmedRound
        {
            get => Header.ConfirmedRound;
            set => Header.ConfirmedRound = value;
        }

        [AlgoApiKey("created-application-index", null)]
        public ulong CreatedApplicationIndex
        {
            get => Header.CreatedApplicationIndex;
            set => Header.CreatedApplicationIndex = value;
        }

        [AlgoApiKey("created-asset-index", null)]
        public ulong CreatedAssetIndex
        {
            get => Header.CreatedAssetIndex;
            set => Header.CreatedAssetIndex = value;
        }

        [AlgoApiKey("intra-round-offset", null)]
        public ulong IntraRoundOffset
        {
            get => Header.IntraRoundOffset;
            set => Header.IntraRoundOffset = value;
        }

        [AlgoApiKey("global-state-delta", null)]
        public EvalDeltaKeyValue[] GlobalStateDelta
        {
            get => Header.GlobalStateDelta;
            set => Header.GlobalStateDelta = value;
        }

        [AlgoApiKey("local-state-delta", null)]
        public AccountStateDelta[] LocalStateDelta
        {
            get => Header.LocalStateDelta;
            set => Header.LocalStateDelta = value;
        }

        [AlgoApiKey("receiver-rewards", null)]
        public ulong ReceiverRewards
        {
            get => Header.ReceiverRewards;
            set => Header.ReceiverRewards = value;
        }

        [AlgoApiKey("round-time", null)]
        public ulong RoundTime
        {
            get => Header.RoundTime;
            set => Header.RoundTime = value;
        }

        [AlgoApiKey("sender-rewards", null)]
        public ulong SenderRewards
        {
            get => Header.SenderRewards;
            set => Header.SenderRewards = value;
        }
    }
}
