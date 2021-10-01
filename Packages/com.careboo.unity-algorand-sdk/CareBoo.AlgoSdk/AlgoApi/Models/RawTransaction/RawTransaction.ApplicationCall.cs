namespace AlgoSdk
{
    public partial struct RawTransaction
    {
        [AlgoApiKey("application-id", "apid")]
        public ulong ApplicationId
        {
            get => ApplicationCallParams.ApplicationId;
            set => ApplicationCallParams.ApplicationId = value;
        }

        [AlgoApiKey("on-completion", "apan")]
        public ulong OnComplete
        {
            get => ApplicationCallParams.OnComplete;
            set => ApplicationCallParams.OnComplete = value;
        }

        [AlgoApiKey("accounts", "apat")]
        public Address[] Accounts
        {
            get => ApplicationCallParams.Accounts;
            set => ApplicationCallParams.Accounts = value;
        }

        [AlgoApiKey("approval-program", "apap")]
        public byte[] ApprovalProgram
        {
            get => ApplicationCallParams.ApprovalProgram;
            set => ApplicationCallParams.ApprovalProgram = value;
        }

        [AlgoApiKey("application-args", "apaa")]
        public byte[] AppArguments
        {
            get => ApplicationCallParams.AppArguments;
            set => ApplicationCallParams.AppArguments = value;
        }

        [AlgoApiKey("clear-state-program", "apsu")]
        public byte[] ClearStateProgram
        {
            get => ApplicationCallParams.ClearStateProgram;
            set => ApplicationCallParams.ClearStateProgram = value;
        }

        [AlgoApiKey("foreign-apps", "apfa")]
        public Address[] ForeignApps
        {
            get => ApplicationCallParams.ForeignApps;
            set => ApplicationCallParams.ForeignApps = value;
        }

        [AlgoApiKey("foreign-assets", "apas")]
        public Address[] ForeignAssets
        {
            get => ApplicationCallParams.ForeignAssets;
            set => ApplicationCallParams.ForeignAssets = value;
        }

        [AlgoApiKey("global-state-schema", "apgs")]
        public Optional<ApplicationStateSchema> GlobalStateSchema
        {
            get => ApplicationCallParams.GlobalStateSchema;
            set => ApplicationCallParams.GlobalStateSchema = value;
        }

        [AlgoApiKey("local-state-schema", "apls")]
        public Optional<ApplicationStateSchema> LocalStateSchema
        {
            get => ApplicationCallParams.LocalStateSchema;
            set => ApplicationCallParams.LocalStateSchema = value;
        }

        [AlgoApiKey("extra-program-pages", "apep")]
        public Optional<ulong> ExtraProgramPages
        {
            get => ApplicationCallParams.ExtraProgramPages;
            set => ApplicationCallParams.ExtraProgramPages = value;
        }
    }
}
