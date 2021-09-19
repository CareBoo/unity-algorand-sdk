using Unity.Collections;

namespace AlgoSdk
{
    public partial struct RawTransaction
    {
        [AlgoApiKey("fee")]
        public ulong Fee
        {
            get => Header.Fee;
            set => Header.Fee = value;
        }

        [AlgoApiKey("fv")]
        public ulong FirstValidRound
        {
            get => Header.FirstValidRound;
            set => Header.FirstValidRound = value;
        }

        [AlgoApiKey("gh")]
        public GenesisHash GenesisHash
        {
            get => Header.GenesisHash;
            set => Header.GenesisHash = value;
        }

        [AlgoApiKey("lv")]
        public ulong LastValidRound
        {
            get => Header.LastValidRound;
            set => Header.LastValidRound = value;
        }

        [AlgoApiKey("snd")]
        public Address Sender
        {
            get => Header.Sender;
            set => Header.Sender = value;
        }

        [AlgoApiKey("type")]
        public TransactionType TransactionType
        {
            get => Header.TransactionType;
            set => Header.TransactionType = value;
        }

        [AlgoApiKey("gen")]
        public FixedString32Bytes GenesisId
        {
            get => Header.GenesisId;
            set => Header.GenesisId = value;
        }

        [AlgoApiKey("grp")]
        public Address Group
        {
            get => Header.Group;
            set => Header.Group = value;
        }

        [AlgoApiKey("lx")]
        public Address Lease
        {
            get => Header.Lease;
            set => Header.Lease = value;
        }

        [AlgoApiKey("note")]
        public byte[] Note
        {
            get => Header.Note;
            set => Header.Note = value;
        }

        [AlgoApiKey("rekey")]
        public Address RekeyTo
        {
            get => Header.RekeyTo;
            set => Header.RekeyTo = value;
        }
    }
}
