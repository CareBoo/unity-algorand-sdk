using System;
using AlgoSdk.Crypto;
using Unity.Collections;

namespace AlgoSdk
{
    public partial struct Transaction
    {
        [AlgoApiField(null, "apid")]
        public ulong ApplicationId
        {
            get => ApplicationCallParams.ApplicationId;
            set => ApplicationCallParams.ApplicationId = value;
        }

        [AlgoApiField("on-completion", "apan")]
        public OnCompletion OnComplete
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

        public struct ApplicationCall
            : ITransaction
            , IEquatable<ApplicationCall>
        {
            Header header;

            Params @params;

            public Header Header
            {
                get => header;
                set => header = value;
            }

            public ulong Fee
            {
                get => header.Fee;
                set => header.Fee = value;
            }

            public ulong FirstValidRound
            {
                get => header.FirstValidRound;
                set => header.FirstValidRound = value;
            }
            public GenesisHash GenesisHash
            {
                get => header.GenesisHash;
                set => header.GenesisHash = value;
            }
            public ulong LastValidRound
            {
                get => header.LastValidRound;
                set => header.LastValidRound = value;
            }

            public Address Sender
            {
                get => header.Sender;
                set => header.Sender = value;
            }

            public FixedString32Bytes GenesisId
            {
                get => header.GenesisId;
                set => header.GenesisId = value;
            }

            public Address Group
            {
                get => header.Group;
                set => header.Group = value;
            }

            public Address Lease
            {
                get => header.Lease;
                set => header.Lease = value;
            }

            public byte[] Note
            {
                get => header.Note;
                set => header.Note = value;
            }

            public Address RekeyTo
            {
                get => header.RekeyTo;
                set => header.RekeyTo = value;
            }

            public ulong ApplicationId
            {
                get => @params.ApplicationId;
                set => @params.ApplicationId = value;
            }

            public OnCompletion OnComplete
            {
                get => @params.OnComplete;
                set => @params.OnComplete = value;
            }

            public Address[] Accounts
            {
                get => @params.Accounts;
                set => @params.Accounts = value;
            }

            public byte[] ApprovalProgram
            {
                get => @params.ApprovalProgram;
                set => @params.ApprovalProgram = value;
            }

            public byte[] AppArguments
            {
                get => @params.AppArguments;
                set => @params.AppArguments = value;
            }

            public byte[] ClearStateProgram
            {
                get => @params.ClearStateProgram;
                set => @params.ClearStateProgram = value;
            }

            public Address[] ForeignApps
            {
                get => @params.ForeignApps;
                set => @params.ForeignApps = value;
            }

            public Address[] ForeignAssets
            {
                get => @params.ForeignAssets;
                set => @params.ForeignAssets = value;
            }

            public Optional<StateSchema> GlobalStateSchema
            {
                get => @params.GlobalStateSchema;
                set => @params.GlobalStateSchema = value;
            }

            public Optional<StateSchema> LocalStateSchema
            {
                get => @params.LocalStateSchema;
                set => @params.LocalStateSchema = value;
            }

            public Optional<ulong> ExtraProgramPages
            {
                get => @params.ExtraProgramPages;
                set => @params.ExtraProgramPages = value;
            }

            public ApplicationCall(
                Address sender,
                TransactionParams txnParams,
                Address freezeAccount,
                ulong freezeAsset,
                bool assetFrozen,
                ulong appId,
                OnCompletion onComplete
            )
            {
                header = new Header(
                    sender,
                    TransactionType.ApplicationCall,
                    txnParams
                );
                @params = new Params(
                    appId,
                    onComplete
                );
            }

            public void CopyTo(ref Transaction transaction)
            {
                transaction.HeaderParams = header;
                transaction.ApplicationCallParams = @params;
            }

            public void CopyFrom(Transaction transaction)
            {
                header = transaction.HeaderParams;
                @params = transaction.ApplicationCallParams;
            }

            public bool Equals(ApplicationCall other)
            {
                return header.Equals(other.Header)
                    && @params.Equals(other.@params)
                    ;
            }

            [AlgoApiObject]
            public struct Params
                : IEquatable<Params>
            {
                [AlgoApiField("application-id", "apid")]
                public ulong ApplicationId;

                [AlgoApiField("on-completion", "apan")]
                public OnCompletion OnComplete;

                [AlgoApiField("accounts", "apat")]
                public Address[] Accounts;

                [AlgoApiField("approval-program", "apap")]
                public byte[] ApprovalProgram;

                [AlgoApiField("application-args", "apaa")]
                public byte[] AppArguments;

                [AlgoApiField("clear-state-program", "apsu")]
                public byte[] ClearStateProgram;

                [AlgoApiField("foreign-apps", "apfa")]
                public Address[] ForeignApps;

                [AlgoApiField("foreign-assets", "apas")]
                public Address[] ForeignAssets;

                [AlgoApiField("global-state-schema", "global-state-schema")]
                public Optional<StateSchema> GlobalStateSchema;

                [AlgoApiField("local-state-schema", "local-state-schema")]
                public Optional<StateSchema> LocalStateSchema;

                [AlgoApiField("extra-program-pages", "epp")]
                public Optional<ulong> ExtraProgramPages;

                public Params(
                    ulong appId,
                    OnCompletion onComplete
                )
                {
                    ApplicationId = appId;
                    OnComplete = onComplete;
                    Accounts = default;
                    ApprovalProgram = default;
                    AppArguments = default;
                    ClearStateProgram = default;
                    ForeignApps = default;
                    ForeignAssets = default;
                    GlobalStateSchema = default;
                    LocalStateSchema = default;
                    ExtraProgramPages = default;
                }

                public bool Equals(Params other)
                {
                    return ApplicationId.Equals(other.ApplicationId)
                        && OnComplete.Equals(other.OnComplete)
                        && ArrayComparer.Equals(Accounts, other.Accounts)
                        && ArrayComparer.Equals(ApprovalProgram, other.ApprovalProgram)
                        && ArrayComparer.Equals(AppArguments, other.AppArguments)
                        && ArrayComparer.Equals(ClearStateProgram, other.ClearStateProgram)
                        && ArrayComparer.Equals(ForeignApps, other.ForeignApps)
                        && ArrayComparer.Equals(ForeignAssets, other.ForeignAssets)
                        && GlobalStateSchema.Equals(other.GlobalStateSchema)
                        && LocalStateSchema.Equals(other.LocalStateSchema)
                        && ExtraProgramPages.Equals(other.ExtraProgramPages)
                        ;
                }
            }
        }
    }
}
