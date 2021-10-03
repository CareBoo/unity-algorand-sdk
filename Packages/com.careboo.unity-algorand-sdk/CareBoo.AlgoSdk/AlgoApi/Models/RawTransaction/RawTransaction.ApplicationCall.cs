namespace AlgoSdk
{
    public partial struct RawTransaction
    {
        [AlgoApiField(null, "apid")]
        public ulong ApplicationId
        {
            get => ApplicationCallParams.ApplicationId;
            set => ApplicationCallParams.ApplicationId = value;
        }

        [AlgoApiField(null, "apan")]
        public ulong OnComplete
        {
            get => ApplicationCallParams.OnComplete;
            set => ApplicationCallParams.OnComplete = value;
        }

        [AlgoApiField(null, "apat")]
        public Address[] Accounts
        {
            get => ApplicationCallParams.Accounts;
            set => ApplicationCallParams.Accounts = value;
        }

        [AlgoApiField(null, "apap")]
        public byte[] ApprovalProgram
        {
            get => ApplicationCallParams.ApprovalProgram;
            set => ApplicationCallParams.ApprovalProgram = value;
        }

        [AlgoApiField(null, "apaa")]
        public byte[] AppArguments
        {
            get => ApplicationCallParams.AppArguments;
            set => ApplicationCallParams.AppArguments = value;
        }

        [AlgoApiField(null, "apsu")]
        public byte[] ClearStateProgram
        {
            get => ApplicationCallParams.ClearStateProgram;
            set => ApplicationCallParams.ClearStateProgram = value;
        }

        [AlgoApiField(null, "apfa")]
        public Address[] ForeignApps
        {
            get => ApplicationCallParams.ForeignApps;
            set => ApplicationCallParams.ForeignApps = value;
        }

        [AlgoApiField(null, "apas")]
        public Address[] ForeignAssets
        {
            get => ApplicationCallParams.ForeignAssets;
            set => ApplicationCallParams.ForeignAssets = value;
        }

        [AlgoApiField(null, "apgs")]
        public Optional<StateSchema> GlobalStateSchema
        {
            get => ApplicationCallParams.GlobalStateSchema;
            set => ApplicationCallParams.GlobalStateSchema = value;
        }

        [AlgoApiField(null, "apls")]
        public Optional<StateSchema> LocalStateSchema
        {
            get => ApplicationCallParams.LocalStateSchema;
            set => ApplicationCallParams.LocalStateSchema = value;
        }

        [AlgoApiField(null, "apep")]
        public Optional<ulong> ExtraProgramPages
        {
            get => ApplicationCallParams.ExtraProgramPages;
            set => ApplicationCallParams.ExtraProgramPages = value;
        }
    }
}
