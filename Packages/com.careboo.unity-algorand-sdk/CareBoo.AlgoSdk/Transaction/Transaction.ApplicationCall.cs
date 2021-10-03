using System;
using AlgoSdk.Crypto;
using Unity.Collections;

namespace AlgoSdk
{
    public static partial class Transaction
    {
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
            public ulong OnComplete
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
            public Optional<ApplicationStateSchema> GlobalStateSchema
            {
                get => @params.GlobalStateSchema;
                set => @params.GlobalStateSchema = value;
            }
            public Optional<ApplicationStateSchema> LocalStateSchema
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
                ulong fee,
                ulong firstValidRound,
                Sha512_256_Hash genesisHash,
                ulong lastValidRound,
                Address sender,
                Address freezeAccount,
                ulong freezeAsset,
                bool assetFrozen,
                ulong appId,
                ulong onComplete
            )
            {
                header = new Header(
                    fee,
                    firstValidRound,
                    genesisHash,
                    lastValidRound,
                    sender,
                    TransactionType.ApplicationCall
                );
                @params = new Params(
                    appId,
                    onComplete
                );
            }

            public void CopyTo(ref RawTransaction rawTransaction)
            {
                rawTransaction.Header = header;
                rawTransaction.ApplicationCallParams = @params;
            }

            public void CopyFrom(RawTransaction rawTransaction)
            {
                header = rawTransaction.Header;
                @params = rawTransaction.ApplicationCallParams;
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

                [AlgoApiField("on-completion", "on-completion")]
                public ulong OnComplete;

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
                public Optional<ApplicationStateSchema> GlobalStateSchema;

                [AlgoApiField("local-state-schema", "local-state-schema")]
                public Optional<ApplicationStateSchema> LocalStateSchema;

                [AlgoApiField("extra-program-pages", "epp")]
                public Optional<ulong> ExtraProgramPages;

                public Params(
                    ulong appId,
                    ulong onComplete
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
