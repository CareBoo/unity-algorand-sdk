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

        [AlgoApiField(null, "apap")]
        public byte[] ApprovalProgram
        {
            get => ApplicationCallParams.ApprovalProgram;
            set => ApplicationCallParams.ApprovalProgram = value;
        }

        [AlgoApiField(null, "apsu")]
        public byte[] ClearStateProgram
        {
            get => ApplicationCallParams.ClearStateProgram;
            set => ApplicationCallParams.ClearStateProgram = value;
        }

        [AlgoApiField(null, "apaa")]
        public byte[] AppArguments
        {
            get => ApplicationCallParams.AppArguments;
            set => ApplicationCallParams.AppArguments = value;
        }

        [AlgoApiField(null, "apat")]
        public Address[] Accounts
        {
            get => ApplicationCallParams.Accounts;
            set => ApplicationCallParams.Accounts = value;
        }

        [AlgoApiField(null, "apfa")]
        public ulong[] ForeignApps
        {
            get => ApplicationCallParams.ForeignApps;
            set => ApplicationCallParams.ForeignApps = value;
        }

        [AlgoApiField(null, "apas")]
        public ulong[] ForeignAssets
        {
            get => ApplicationCallParams.ForeignAssets;
            set => ApplicationCallParams.ForeignAssets = value;
        }

        [AlgoApiField(null, "apgs")]
        public StateSchema GlobalStateSchema
        {
            get => ApplicationCallParams.GlobalStateSchema;
            set => ApplicationCallParams.GlobalStateSchema = value;
        }

        [AlgoApiField(null, "apls")]
        public StateSchema LocalStateSchema
        {
            get => ApplicationCallParams.LocalStateSchema;
            set => ApplicationCallParams.LocalStateSchema = value;
        }

        [AlgoApiField(null, "apep")]
        public ulong ExtraProgramPages
        {
            get => ApplicationCallParams.ExtraProgramPages;
            set => ApplicationCallParams.ExtraProgramPages = value;
        }

        public static AppCallTxn AppCreate(
            Address sender,
            TransactionParams txnParams,
            byte[] approvalProgram,
            byte[] clearStateProgram,
            StateSchema globalStateSchema = default,
            StateSchema localStateSchema = default,
            ulong extraProgramPages = default
        )
        {
            var txn = new AppCallTxn
            {
                header = new TransactionHeader(sender, TransactionType.ApplicationCall, txnParams),
                OnComplete = OnCompletion.NoOp,
                ApprovalProgram = approvalProgram,
                ClearStateProgram = clearStateProgram,
                GlobalStateSchema = globalStateSchema,
                LocalStateSchema = localStateSchema,
                ExtraProgramPages = extraProgramPages
            };
            txn.Fee = txn.GetSuggestedFee(txnParams);
            return txn;
        }

        public static AppCallTxn AppCloseOut(
            Address sender,
            TransactionParams txnParams,
            ulong applicationId,
            byte[] appArguments = default,
            Address[] accounts = default,
            ulong[] foreignApps = default,
            ulong[] foreignAssets = default
        )
        {
            var txn = new AppCallTxn
            {
                header = new TransactionHeader(sender, TransactionType.ApplicationCall, txnParams),
                ApplicationId = applicationId,
                OnComplete = OnCompletion.CloseOut,
                AppArguments = appArguments,
                Accounts = accounts,
                ForeignApps = foreignApps,
                ForeignAssets = foreignAssets,
            };
            txn.Fee = txn.GetSuggestedFee(txnParams);
            return txn;
        }

        public static AppCallTxn AppClearState(
            Address sender,
            TransactionParams txnParams,
            ulong applicationId,
            byte[] appArguments = default,
            Address[] accounts = default,
            ulong[] foreignApps = default,
            ulong[] foreignAssets = default
        )
        {
            var txn = new AppCallTxn
            {
                header = new TransactionHeader(sender, TransactionType.ApplicationCall, txnParams),
                ApplicationId = applicationId,
                OnComplete = OnCompletion.Clear,
                AppArguments = appArguments,
                Accounts = accounts,
                ForeignApps = foreignApps,
                ForeignAssets = foreignAssets,
            };
            txn.Fee = txn.GetSuggestedFee(txnParams);
            return txn;
        }

        public static AppCallTxn AppCall(
            Address sender,
            TransactionParams txnParams,
            ulong applicationId,
            OnCompletion onComplete = OnCompletion.NoOp,
            byte[] appArguments = default,
            Address[] accounts = default,
            ulong[] foreignApps = default,
            ulong[] foreignAssets = default
        )
        {
            var txn = new AppCallTxn
            {
                header = new TransactionHeader(sender, TransactionType.ApplicationCall, txnParams),
                ApplicationId = applicationId,
                OnComplete = onComplete,
                AppArguments = appArguments,
                Accounts = accounts,
                ForeignApps = foreignApps,
                ForeignAssets = foreignAssets,
            };
            txn.Fee = txn.GetSuggestedFee(txnParams);
            return txn;
        }

        public static AppCallTxn AppOptIn(
            Address sender,
            TransactionParams txnParams,
            ulong applicationId,
            byte[] appArguments = default,
            Address[] accounts = default,
            ulong[] foreignApps = default,
            ulong[] foreignAssets = default
        )
        {
            var txn = new AppCallTxn
            {
                header = new TransactionHeader(sender, TransactionType.ApplicationCall, txnParams),
                ApplicationId = applicationId,
                OnComplete = OnCompletion.OptIn,
                AppArguments = appArguments,
                Accounts = accounts,
                ForeignApps = foreignApps,
                ForeignAssets = foreignAssets,
            };
            txn.Fee = txn.GetSuggestedFee(txnParams);
            return txn;
        }

        public static AppCallTxn AppUpdateTxn(
            Address sender,
            TransactionParams txnParams,
            ulong applicationId,
            byte[] approvalProgram = default,
            byte[] clearStateProgram = default,
            ulong extraProgramPages = default,
            byte[] appArguments = default,
            Address[] accounts = default,
            ulong[] foreignApps = default,
            ulong[] foreignAssets = default
        )
        {
            var txn = new AppCallTxn
            {
                header = new TransactionHeader(sender, TransactionType.ApplicationCall, txnParams),
                ApplicationId = applicationId,
                OnComplete = OnCompletion.Update,
                ApprovalProgram = approvalProgram,
                ClearStateProgram = clearStateProgram,
                ExtraProgramPages = extraProgramPages,
                AppArguments = appArguments,
                Accounts = accounts,
                ForeignApps = foreignApps,
                ForeignAssets = foreignAssets
            };
            txn.Fee = txn.GetSuggestedFee(txnParams);
            return txn;
        }

        public static AppCallTxn AppDelete(
            Address sender,
            TransactionParams txnParams,
            ulong applicationId,
            byte[] appArguments = default,
            Address[] accounts = default,
            ulong[] foreignApps = default,
            ulong[] foreignAssets = default
        )
        {
            var txn = new AppCallTxn
            {
                header = new TransactionHeader(sender, TransactionType.ApplicationCall, txnParams),
                ApplicationId = applicationId,
                OnComplete = OnCompletion.Delete,
                AppArguments = appArguments,
                Accounts = accounts,
                ForeignApps = foreignApps,
                ForeignAssets = foreignAssets
            };
            txn.Fee = txn.GetSuggestedFee(txnParams);
            return txn;
        }
    }

    [AlgoApiObject]
    public struct AppCallTxn
          : ITransaction
          , IEquatable<AppCallTxn>
    {
        internal TransactionHeader header;

        Params @params;

        [AlgoApiField("fee", "fee")]
        public ulong Fee
        {
            get => header.Fee;
            set => header.Fee = value;
        }

        [AlgoApiField("first-valid", "fv")]
        public ulong FirstValidRound
        {
            get => header.FirstValidRound;
            set => header.FirstValidRound = value;
        }

        [AlgoApiField("genesis-hash", "gh")]
        public GenesisHash GenesisHash
        {
            get => header.GenesisHash;
            set => header.GenesisHash = value;
        }

        [AlgoApiField("last-valid", "lv")]
        public ulong LastValidRound
        {
            get => header.LastValidRound;
            set => header.LastValidRound = value;
        }

        [AlgoApiField("sender", "snd")]
        public Address Sender
        {
            get => header.Sender;
            set => header.Sender = value;
        }

        [AlgoApiField("tx-type", "type")]
        public TransactionType TransactionType
        {
            get => TransactionType.ApplicationCall;
            internal set => header.TransactionType = TransactionType.ApplicationCall;
        }

        [AlgoApiField("genesis-id", "gen")]
        public FixedString32Bytes GenesisId
        {
            get => header.GenesisId;
            set => header.GenesisId = value;
        }

        [AlgoApiField("group", "grp")]
        public Sha512_256_Hash Group
        {
            get => header.Group;
            set => header.Group = value;
        }

        [AlgoApiField("lease", "lx")]
        public Sha512_256_Hash Lease
        {
            get => header.Lease;
            set => header.Lease = value;
        }

        [AlgoApiField("note", "note")]
        public byte[] Note
        {
            get => header.Note;
            set => header.Note = value;
        }

        [AlgoApiField("rekey-to", "rekey")]
        public Address RekeyTo
        {
            get => header.RekeyTo;
            set => header.RekeyTo = value;
        }

        [AlgoApiField(null, "apid")]
        public ulong ApplicationId
        {
            get => @params.ApplicationId;
            set => @params.ApplicationId = value;
        }

        [AlgoApiField("on-completion", "apan")]
        public OnCompletion OnComplete
        {
            get => @params.OnComplete;
            set => @params.OnComplete = value;
        }

        [AlgoApiField(null, "apap")]
        public byte[] ApprovalProgram
        {
            get => @params.ApprovalProgram;
            set => @params.ApprovalProgram = value;
        }

        [AlgoApiField(null, "apsu")]
        public byte[] ClearStateProgram
        {
            get => @params.ClearStateProgram;
            set => @params.ClearStateProgram = value;
        }

        [AlgoApiField(null, "apaa")]
        public byte[] AppArguments
        {
            get => @params.AppArguments;
            set => @params.AppArguments = value;
        }

        [AlgoApiField(null, "apat")]
        public Address[] Accounts
        {
            get => @params.Accounts;
            set => @params.Accounts = value;
        }

        [AlgoApiField(null, "apfa")]
        public ulong[] ForeignApps
        {
            get => @params.ForeignApps;
            set => @params.ForeignApps = value;
        }

        [AlgoApiField(null, "apas")]
        public ulong[] ForeignAssets
        {
            get => @params.ForeignAssets;
            set => @params.ForeignAssets = value;
        }

        [AlgoApiField(null, "apgs")]
        public StateSchema GlobalStateSchema
        {
            get => @params.GlobalStateSchema;
            set => @params.GlobalStateSchema = value;
        }

        [AlgoApiField(null, "apls")]
        public StateSchema LocalStateSchema
        {
            get => @params.LocalStateSchema;
            set => @params.LocalStateSchema = value;
        }

        [AlgoApiField(null, "apep")]
        public ulong ExtraProgramPages
        {
            get => @params.ExtraProgramPages;
            set => @params.ExtraProgramPages = value;
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

        public bool Equals(AppCallTxn other)
        {
            return header.Equals(other.header)
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

            [AlgoApiField("approval-program", "apap")]
            public byte[] ApprovalProgram;

            [AlgoApiField("clear-state-program", "apsu")]
            public byte[] ClearStateProgram;

            [AlgoApiField("application-args", "apaa")]
            public byte[] AppArguments;

            [AlgoApiField("accounts", "apat")]
            public Address[] Accounts;

            [AlgoApiField("foreign-apps", "apfa")]
            public ulong[] ForeignApps;

            [AlgoApiField("foreign-assets", "apas")]
            public ulong[] ForeignAssets;

            [AlgoApiField("global-state-schema", "global-state-schema")]
            public StateSchema GlobalStateSchema;

            [AlgoApiField("local-state-schema", "local-state-schema")]
            public StateSchema LocalStateSchema;

            [AlgoApiField("extra-program-pages", "epp")]
            public ulong ExtraProgramPages;

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
