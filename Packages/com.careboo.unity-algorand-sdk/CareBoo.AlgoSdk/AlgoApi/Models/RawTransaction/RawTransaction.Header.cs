using Unity.Collections;

namespace AlgoSdk
{
    public partial struct RawTransaction
    {
        [AlgoApiKey(null, "fee")]
        public ulong Fee
        {
            get => Header.Fee;
            set => Header.Fee = value;
        }

        [AlgoApiKey(null, "fv")]
        public ulong FirstValidRound
        {
            get => Header.FirstValidRound;
            set => Header.FirstValidRound = value;
        }

        [AlgoApiKey(null, "gh")]
        public GenesisHash GenesisHash
        {
            get => Header.GenesisHash;
            set => Header.GenesisHash = value;
        }

        [AlgoApiKey(null, "lv")]
        public ulong LastValidRound
        {
            get => Header.LastValidRound;
            set => Header.LastValidRound = value;
        }

        [AlgoApiKey(null, "snd")]
        public Address Sender
        {
            get => Header.Sender;
            set => Header.Sender = value;
        }

        [AlgoApiKey(null, "type")]
        public TransactionType TransactionType
        {
            get => Header.TransactionType;
            set => Header.TransactionType = value;
        }

        [AlgoApiKey(null, "gen")]
        public FixedString32Bytes GenesisId
        {
            get => Header.GenesisId;
            set => Header.GenesisId = value;
        }

        [AlgoApiKey(null, "grp")]
        public Address Group
        {
            get => Header.Group;
            set => Header.Group = value;
        }

        [AlgoApiKey(null, "lx")]
        public Address Lease
        {
            get => Header.Lease;
            set => Header.Lease = value;
        }

        [AlgoApiKey(null, "note")]
        public byte[] Note
        {
            get => Header.Note;
            set => Header.Note = value;
        }

        [AlgoApiKey(null, "rekey")]
        public Address RekeyTo
        {
            get => Header.RekeyTo;
            set => Header.RekeyTo = value;
        }
    }
}
