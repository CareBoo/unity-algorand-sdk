using System;
using Unity.Collections;
using UnityEngine;

namespace Algorand.Unity
{
    public interface IAppCallTxn : ITransaction
    {
        /// <summary>
        ///     ID of the application being configured or empty if creating.
        /// </summary>
        AppIndex ApplicationId { get; set; }

        /// <summary>
        ///     Defines what additional actions occur with the transaction.
        /// </summary>
        OnCompletion OnComplete { get; set; }

        /// <summary>
        ///     Logic executed for every application transaction, except when on-completion is set to "clear". It can read and
        ///     write global state for the application, as well as account-specific local state. Approval programs may reject the
        ///     transaction.
        /// </summary>
        CompiledTeal ApprovalProgram { get; set; }

        /// <summary>
        ///     Logic executed for application transactions with on-completion set to "clear". It can read and write global state
        ///     for the application, as well as account-specific local state. Clear state programs cannot reject the transaction.
        /// </summary>
        CompiledTeal ClearStateProgram { get; set; }

        /// <summary>
        ///     Transaction specific arguments accessed from the application's approval-program and clear-state-program.
        /// </summary>
        CompiledTeal[] AppArguments { get; set; }

        /// <summary>
        ///     List of accounts in addition to the sender that may be accessed from the application's approval-program and
        ///     clear-state-program.
        /// </summary>
        Address[] Accounts { get; set; }

        /// <summary>
        ///     Lists the applications in addition to the application-id whose global states may be accessed by this application's
        ///     approval-program and clear-state-program. The access is read-only.
        /// </summary>
        ulong[] ForeignApps { get; set; }

        /// <summary>
        ///     Lists the assets whose AssetParams may be accessed by this application's approval-program and clear-state-program.
        ///     The access is read-only.
        /// </summary>
        ulong[] ForeignAssets { get; set; }

        /// <summary>
        ///     Holds the maximum number of global state values defined within a <see cref="StateSchema" /> object.
        /// </summary>
        StateSchema GlobalStateSchema { get; set; }

        /// <summary>
        ///     Holds the maximum number of local state values defined within a <see cref="StateSchema" /> object.
        /// </summary>
        StateSchema LocalStateSchema { get; set; }

        /// <summary>
        ///     Number of additional pages allocated to the application's approval and clear state programs. Each ExtraProgramPages
        ///     is 2048 bytes. The sum of <see cref="ApprovalProgram" /> and <see cref="ClearStateProgram" /> may not exceed
        ///     2048*(1+ExtraProgramPages) bytes.
        /// </summary>
        ulong ExtraProgramPages { get; set; }

        /// <summary>
        ///     The boxes that should be made available for the runtime of the program.
        /// </summary>
        BoxRef[] Boxes { get; set; }
    }

    public partial struct Transaction : IAppCallTxn
    {
        /// <inheritdoc />
        [AlgoApiField("apid")]
        public AppIndex ApplicationId
        {
            get => appCallParams.ApplicationId;
            set => appCallParams.ApplicationId = value;
        }

        /// <inheritdoc />
        [AlgoApiField("apan")]
        public OnCompletion OnComplete
        {
            get => appCallParams.OnComplete;
            set => appCallParams.OnComplete = value;
        }

        /// <inheritdoc />
        [AlgoApiField("apap")]
        public CompiledTeal ApprovalProgram
        {
            get => appCallParams.ApprovalProgram;
            set => appCallParams.ApprovalProgram = value;
        }

        /// <inheritdoc />
        [AlgoApiField("apsu")]
        public CompiledTeal ClearStateProgram
        {
            get => appCallParams.ClearStateProgram;
            set => appCallParams.ClearStateProgram = value;
        }

        /// <inheritdoc />
        [AlgoApiField("apaa")]
        public CompiledTeal[] AppArguments
        {
            get => appCallParams.AppArguments;
            set => appCallParams.AppArguments = value;
        }

        /// <inheritdoc />
        [AlgoApiField("apat")]
        public Address[] Accounts
        {
            get => appCallParams.Accounts;
            set => appCallParams.Accounts = value;
        }

        /// <inheritdoc />
        [AlgoApiField("apfa")]
        public ulong[] ForeignApps
        {
            get => appCallParams.ForeignApps;
            set => appCallParams.ForeignApps = value;
        }

        /// <inheritdoc />
        [AlgoApiField("apas")]
        public ulong[] ForeignAssets
        {
            get => appCallParams.ForeignAssets;
            set => appCallParams.ForeignAssets = value;
        }

        /// <inheritdoc />
        [AlgoApiField("apgs")]
        public StateSchema GlobalStateSchema
        {
            get => appCallParams.GlobalStateSchema;
            set => appCallParams.GlobalStateSchema = value;
        }

        /// <inheritdoc />
        [AlgoApiField("apls")]
        public StateSchema LocalStateSchema
        {
            get => appCallParams.LocalStateSchema;
            set => appCallParams.LocalStateSchema = value;
        }

        /// <inheritdoc />
        [AlgoApiField("apep")]
        public ulong ExtraProgramPages
        {
            get => appCallParams.ExtraProgramPages;
            set => appCallParams.ExtraProgramPages = value;
        }

        /// <inheritdoc />
        [AlgoApiField("apbx")]
        public BoxRef[] Boxes
        {
            get => appCallParams.Boxes;
            set => appCallParams.Boxes = value;
        }

        /// <summary>
        ///     Create an <see cref="AppCallTxn" /> with params to create apps.
        /// </summary>
        /// <param name="sender">The address of the account that pays the fee and amount.</param>
        /// <param name="txnParams">See <see cref="TransactionParams" /></param>
        /// <param name="approvalProgram">
        ///     Logic executed for every application transaction, except when on-completion is set to
        ///     "clear". It can read and write global state for the application, as well as account-specific local state. Approval
        ///     programs may reject the transaction.
        /// </param>
        /// <param name="clearStateProgram">
        ///     Logic executed for application transactions with on-completion set to "clear". It can
        ///     read and write global state for the application, as well as account-specific local state. Clear state programs
        ///     cannot reject the transaction.
        /// </param>
        /// <param name="globalStateSchema">
        ///     Holds the maximum number of global state values defined within a
        ///     <see cref="StateSchema" /> object.
        /// </param>
        /// <param name="localStateSchema">
        ///     Holds the maximum number of local state values defined within a
        ///     <see cref="StateSchema" /> object.
        /// </param>
        /// <param name="extraProgramPages">
        ///     Number of additional pages allocated to the application's approval and clear state
        ///     programs. Each ExtraProgramPages is 2048 bytes. The sum of ApprovalProgram and ClearStateProgram may not exceed
        ///     2048*(1+ExtraProgramPages) bytes.
        /// </param>
        /// <returns>An <see cref="AppCallTxn" /> with params set to create apps</returns>
        public static AppCallTxn AppCreate(
            Address sender,
            TransactionParams txnParams,
            CompiledTeal approvalProgram,
            CompiledTeal clearStateProgram,
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

        /// <summary>
        ///     Create an <see cref="AppCallTxn" /> with params to close out state with your account.
        /// </summary>
        /// <param name="sender">The address of the account that pays the fee and amount.</param>
        /// <param name="txnParams">See <see cref="TransactionParams" /></param>
        /// <param name="applicationId">ID of the application being configured.</param>
        /// <param name="appArguments">
        ///     Transaction specific arguments accessed from the application's approval-program and
        ///     clear-state-program.
        /// </param>
        /// <param name="accounts">
        ///     List of accounts in addition to the sender that may be accessed from the application's
        ///     approval-program and clear-state-program.
        /// </param>
        /// <param name="foreignApps">
        ///     Lists the applications in addition to the application-id whose global states may be accessed
        ///     by this application's approval-program and clear-state-program. The access is read-only.
        /// </param>
        /// <param name="foreignAssets">
        ///     Lists the assets whose AssetParams may be accessed by this application's approval-program
        ///     and clear-state-program. The access is read-only.
        /// </param>
        /// <param name="boxRefs">The boxes that should be made available for the runtime of the program.</param>
        /// <returns>An <see cref="AppCallTxn" /> with params set to close out state with your account.</returns>
        public static AppCallTxn AppCloseOut(
            Address sender,
            TransactionParams txnParams,
            AppIndex applicationId,
            CompiledTeal[] appArguments = default,
            Address[] accounts = default,
            ulong[] foreignApps = default,
            ulong[] foreignAssets = default,
            BoxRef[] boxRefs = default
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
                Boxes = boxRefs
            };
            txn.Fee = txn.GetSuggestedFee(txnParams);
            return txn;
        }

        /// <summary>
        ///     Create an <see cref="AppCallTxn" /> with params to clear state with your account.
        /// </summary>
        /// <param name="sender">The address of the account that pays the fee and amount.</param>
        /// <param name="txnParams">See <see cref="TransactionParams" /></param>
        /// <param name="applicationId">ID of the application being configured.</param>
        /// <param name="appArguments">
        ///     Transaction specific arguments accessed from the application's approval-program and
        ///     clear-state-program.
        /// </param>
        /// <param name="accounts">
        ///     List of accounts in addition to the sender that may be accessed from the application's
        ///     approval-program and clear-state-program.
        /// </param>
        /// <param name="foreignApps">
        ///     Lists the applications in addition to the application-id whose global states may be accessed
        ///     by this application's approval-program and clear-state-program. The access is read-only.
        /// </param>
        /// <param name="foreignAssets">
        ///     Lists the assets whose AssetParams may be accessed by this application's approval-program
        ///     and clear-state-program. The access is read-only.
        /// </param>
        /// <param name="boxRefs">The boxes that should be made available for the runtime of the program.</param>
        /// <returns>An <see cref="AppCallTxn" /> with params to clear app state with your account.</returns>
        public static AppCallTxn AppClearState(
            Address sender,
            TransactionParams txnParams,
            AppIndex applicationId,
            CompiledTeal[] appArguments = default,
            Address[] accounts = default,
            ulong[] foreignApps = default,
            ulong[] foreignAssets = default,
            BoxRef[] boxRefs = default
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
                Boxes = boxRefs
            };
            txn.Fee = txn.GetSuggestedFee(txnParams);
            return txn;
        }

        /// <summary>
        ///     Create an <see cref="AppCallTxn" /> used to call an application.
        /// </summary>
        /// <param name="sender">The address of the account that pays the fee and amount.</param>
        /// <param name="txnParams">See <see cref="TransactionParams" /></param>
        /// <param name="applicationId">ID of the application being configured.</param>
        /// <param name="onComplete">Defines what additional actions occur with the transaction.</param>
        /// <param name="appArguments">
        ///     Transaction specific arguments accessed from the application's approval-program and
        ///     clear-state-program.
        /// </param>
        /// <param name="accounts">
        ///     List of accounts in addition to the sender that may be accessed from the application's
        ///     approval-program and clear-state-program.
        /// </param>
        /// <param name="foreignApps">
        ///     Lists the applications in addition to the application-id whose global states may be accessed
        ///     by this application's approval-program and clear-state-program. The access is read-only.
        /// </param>
        /// <param name="foreignAssets">
        ///     Lists the assets whose AssetParams may be accessed by this application's approval-program
        ///     and clear-state-program. The access is read-only.
        /// </param>
        /// <param name="boxRefs">The boxes that should be made available for the runtime of the program.</param>
        /// <returns>An <see cref="AppCallTxn" /> used to call an application.</returns>
        public static AppCallTxn AppCall(
            Address sender,
            TransactionParams txnParams,
            AppIndex applicationId,
            OnCompletion onComplete = OnCompletion.NoOp,
            CompiledTeal[] appArguments = default,
            Address[] accounts = default,
            ulong[] foreignApps = default,
            ulong[] foreignAssets = default,
            BoxRef[] boxRefs = default
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
                Boxes = boxRefs
            };
            txn.Fee = txn.GetSuggestedFee(txnParams);
            return txn;
        }

        /// <summary>
        ///     Create an <see cref="AppCallTxn" /> used to opt in to an application.
        /// </summary>
        /// <param name="sender">The address of the account that pays the fee and amount.</param>
        /// <param name="txnParams">See <see cref="TransactionParams" /></param>
        /// <param name="applicationId">ID of the application being configured.</param>
        /// <param name="appArguments">
        ///     Transaction specific arguments accessed from the application's approval-program and
        ///     clear-state-program.
        /// </param>
        /// <param name="accounts">
        ///     List of accounts in addition to the sender that may be accessed from the application's
        ///     approval-program and clear-state-program.
        /// </param>
        /// <param name="foreignApps">
        ///     Lists the applications in addition to the application-id whose global states may be accessed
        ///     by this application's approval-program and clear-state-program. The access is read-only.
        /// </param>
        /// <param name="foreignAssets">
        ///     Lists the assets whose AssetParams may be accessed by this application's approval-program
        ///     and clear-state-program. The access is read-only.
        /// </param>
        /// <param name="boxRefs">The boxes that should be made available for the runtime of the program.</param>
        /// <returns>An <see cref="AppCallTxn" /> with params to opt in to an application.</returns>
        public static AppCallTxn AppOptIn(
            Address sender,
            TransactionParams txnParams,
            AppIndex applicationId,
            CompiledTeal[] appArguments = default,
            Address[] accounts = default,
            ulong[] foreignApps = default,
            ulong[] foreignAssets = default,
            BoxRef[] boxRefs = default
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
                Boxes = boxRefs
            };
            txn.Fee = txn.GetSuggestedFee(txnParams);
            return txn;
        }

        /// <summary>
        ///     Create an <see cref="AppCallTxn" /> used to update an application.
        /// </summary>
        /// <param name="sender">The address of the account that pays the fee and amount.</param>
        /// <param name="txnParams">See <see cref="TransactionParams" /></param>
        /// <param name="applicationId">ID of the application being configured.</param>
        /// <param name="approvalProgram">
        ///     Logic executed for every application transaction, except when on-completion is set to
        ///     "clear". It can read and write global state for the application, as well as account-specific local state. Approval
        ///     programs may reject the transaction.
        /// </param>
        /// <param name="clearStateProgram">
        ///     Logic executed for application transactions with on-completion set to "clear". It can
        ///     read and write global state for the application, as well as account-specific local state. Clear state programs
        ///     cannot reject the transaction.
        /// </param>
        /// <param name="extraProgramPages">
        ///     Number of additional pages allocated to the application's approval and clear state
        ///     programs. Each ExtraProgramPages is 2048 bytes. The sum of ApprovalProgram and ClearStateProgram may not exceed
        ///     2048*(1+ExtraProgramPages) bytes.
        /// </param>
        /// <param name="appArguments">
        ///     Transaction specific arguments accessed from the application's approval-program and
        ///     clear-state-program.
        /// </param>
        /// <param name="accounts">
        ///     List of accounts in addition to the sender that may be accessed from the application's
        ///     approval-program and clear-state-program.
        /// </param>
        /// <param name="foreignApps">
        ///     Lists the applications in addition to the application-id whose global states may be accessed
        ///     by this application's approval-program and clear-state-program. The access is read-only.
        /// </param>
        /// <param name="foreignAssets">
        ///     Lists the assets whose AssetParams may be accessed by this application's approval-program
        ///     and clear-state-program. The access is read-only.
        /// </param>
        /// <param name="boxRefs">The boxes that should be made available for the runtime of the program.</param>
        /// <returns>An <see cref="AppCallTxn" /> with params to update an application.</returns>
        public static AppCallTxn AppUpdateTxn(
            Address sender,
            TransactionParams txnParams,
            AppIndex applicationId,
            CompiledTeal approvalProgram = default,
            CompiledTeal clearStateProgram = default,
            ulong extraProgramPages = default,
            CompiledTeal[] appArguments = default,
            Address[] accounts = default,
            ulong[] foreignApps = default,
            ulong[] foreignAssets = default,
            BoxRef[] boxRefs = default
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
                ForeignAssets = foreignAssets,
                Boxes = boxRefs
            };
            txn.Fee = txn.GetSuggestedFee(txnParams);
            return txn;
        }

        /// <summary>
        ///     Create an <see cref="AppCallTxn" /> used to delete an application.
        /// </summary>
        /// <param name="sender">The address of the account that pays the fee and amount.</param>
        /// <param name="txnParams">See <see cref="TransactionParams" /></param>
        /// <param name="applicationId">ID of the application being configured.</param>
        /// <param name="appArguments">
        ///     Transaction specific arguments accessed from the application's approval-program and
        ///     clear-state-program.
        /// </param>
        /// <param name="accounts">
        ///     List of accounts in addition to the sender that may be accessed from the application's
        ///     approval-program and clear-state-program.
        /// </param>
        /// <param name="foreignApps">
        ///     Lists the applications in addition to the application-id whose global states may be accessed
        ///     by this application's approval-program and clear-state-program. The access is read-only.
        /// </param>
        /// <param name="foreignAssets">
        ///     Lists the assets whose AssetParams may be accessed by this application's approval-program
        ///     and clear-state-program. The access is read-only.
        /// </param>
        /// <param name="boxRefs">The boxes that should be made available for the runtime of the program.</param>
        /// <returns>An <see cref="AppCallTxn" /> with params to delete an application.</returns>
        public static AppCallTxn AppDelete(
            Address sender,
            TransactionParams txnParams,
            AppIndex applicationId,
            CompiledTeal[] appArguments = default,
            Address[] accounts = default,
            ulong[] foreignApps = default,
            ulong[] foreignAssets = default,
            BoxRef[] boxRefs = default
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
                ForeignAssets = foreignAssets,
                Boxes = boxRefs
            };
            txn.Fee = txn.GetSuggestedFee(txnParams);
            return txn;
        }
    }

    [AlgoApiObject]
    [Serializable]
    public partial struct AppCallTxn
        : IAppCallTxn
            , IEquatable<AppCallTxn>
    {
        public const int MaxNumAppArguments = 16;

        [SerializeField]
        internal TransactionHeader header;

        [SerializeField]
        private Params @params;

        /// <inheritdoc />
        [AlgoApiField("fee")]
        public MicroAlgos Fee
        {
            get => header.Fee;
            set => header.Fee = value;
        }

        /// <inheritdoc />
        [AlgoApiField("fv")]
        public ulong FirstValidRound
        {
            get => header.FirstValidRound;
            set => header.FirstValidRound = value;
        }

        /// <inheritdoc />
        [AlgoApiField("gh")]
        public GenesisHash GenesisHash
        {
            get => header.GenesisHash;
            set => header.GenesisHash = value;
        }

        /// <inheritdoc />
        [AlgoApiField("lv")]
        public ulong LastValidRound
        {
            get => header.LastValidRound;
            set => header.LastValidRound = value;
        }

        /// <inheritdoc />
        [AlgoApiField("snd")]
        public Address Sender
        {
            get => header.Sender;
            set => header.Sender = value;
        }

        /// <inheritdoc />
        [AlgoApiField("type")]
        public TransactionType TransactionType
        {
            get => TransactionType.ApplicationCall;
            internal set => header.TransactionType = TransactionType.ApplicationCall;
        }

        /// <inheritdoc />
        [AlgoApiField("gen")]
        public FixedString32Bytes GenesisId
        {
            get => header.GenesisId;
            set => header.GenesisId = value;
        }

        /// <inheritdoc />
        [AlgoApiField("grp")]
        public TransactionId Group
        {
            get => header.Group;
            set => header.Group = value;
        }

        /// <inheritdoc />
        [AlgoApiField("lx")]
        public TransactionId Lease
        {
            get => header.Lease;
            set => header.Lease = value;
        }

        /// <inheritdoc />
        [AlgoApiField("note")]
        public byte[] Note
        {
            get => header.Note;
            set => header.Note = value;
        }

        /// <inheritdoc />
        [AlgoApiField("rekey")]
        public Address RekeyTo
        {
            get => header.RekeyTo;
            set => header.RekeyTo = value;
        }

        /// <inheritdoc />
        [AlgoApiField("apid")]
        public AppIndex ApplicationId
        {
            get => @params.ApplicationId;
            set => @params.ApplicationId = value;
        }

        /// <inheritdoc />
        [AlgoApiField("apan")]
        public OnCompletion OnComplete
        {
            get => @params.OnComplete;
            set => @params.OnComplete = value;
        }

        /// <inheritdoc />
        [AlgoApiField("apap")]
        public CompiledTeal ApprovalProgram
        {
            get => @params.ApprovalProgram;
            set => @params.ApprovalProgram = value;
        }

        /// <inheritdoc />
        [AlgoApiField("apsu")]
        public CompiledTeal ClearStateProgram
        {
            get => @params.ClearStateProgram;
            set => @params.ClearStateProgram = value;
        }

        /// <inheritdoc />
        [AlgoApiField("apaa")]
        public CompiledTeal[] AppArguments
        {
            get => @params.AppArguments;
            set => @params.AppArguments = value;
        }

        /// <inheritdoc />
        [AlgoApiField("apat")]
        public Address[] Accounts
        {
            get => @params.Accounts;
            set => @params.Accounts = value;
        }

        /// <inheritdoc />
        [AlgoApiField("apfa")]
        public ulong[] ForeignApps
        {
            get => @params.ForeignApps;
            set => @params.ForeignApps = value;
        }

        /// <inheritdoc />
        [AlgoApiField("apas")]
        public ulong[] ForeignAssets
        {
            get => @params.ForeignAssets;
            set => @params.ForeignAssets = value;
        }

        /// <inheritdoc />
        [AlgoApiField("apgs")]
        public StateSchema GlobalStateSchema
        {
            get => @params.GlobalStateSchema;
            set => @params.GlobalStateSchema = value;
        }

        /// <inheritdoc />
        [AlgoApiField("apls")]
        public StateSchema LocalStateSchema
        {
            get => @params.LocalStateSchema;
            set => @params.LocalStateSchema = value;
        }

        /// <inheritdoc />
        [AlgoApiField("apep")]
        public ulong ExtraProgramPages
        {
            get => @params.ExtraProgramPages;
            set => @params.ExtraProgramPages = value;
        }

        /// <inheritdoc />
        [AlgoApiField("apbx")]
        public BoxRef[] Boxes
        {
            get => @params.Boxes;
            set => @params.Boxes = value;
        }

        /// <inheritdoc />
        public void CopyTo(ref Transaction transaction)
        {
            transaction.Header = header;
            transaction.AppCallParams = @params;
        }

        /// <inheritdoc />
        public void CopyFrom(Transaction transaction)
        {
            header = transaction.Header;
            @params = transaction.AppCallParams;
        }

        public bool Equals(AppCallTxn other)
        {
            return header.Equals(other.header)
                   && @params.Equals(other.@params)
                ;
        }

        [AlgoApiObject]
        [Serializable]
        public partial struct Params
            : IEquatable<Params>
        {
            [AlgoApiField("apid")]
            [Tooltip("ID of the application being configured or empty if creating")]
            public AppIndex ApplicationId;

            [AlgoApiField("apan")]
            [Tooltip("Defines what additional actions occur with the transaction.")]
            public OnCompletion OnComplete;

            [AlgoApiField("apap")]
            [Tooltip(
                "Logic executed for every application transaction, except when on-completion is set to \"clear\". It can read and write global state for the application, as well as account-specific local state. Approval programs may reject the transaction.")]
            public CompiledTeal ApprovalProgram;

            [AlgoApiField("apsu")]
            [Tooltip(
                "Logic executed for application transactions with on-completion set to \"clear\". It can read and write global state for the application, as well as account-specific local state. Clear state programs cannot reject the transaction.")]
            public CompiledTeal ClearStateProgram;

            [AlgoApiField("apaa")]
            [Tooltip(
                "Transaction specific arguments accessed from the application's approval-program and clear-state-program.")]
            public CompiledTeal[] AppArguments;

            [AlgoApiField("apat")]
            [Tooltip(
                "List of accounts in addition to the sender that may be accessed from the application's approval-program and clear-state-program.")]
            public Address[] Accounts;

            [AlgoApiField("apfa")]
            [Tooltip(
                "Lists the applications in addition to the application-id whose global states may be accessed by this application's approval-program and clear-state-program. The access is read-only.")]
            public ulong[] ForeignApps;

            [AlgoApiField("apas")]
            [Tooltip(
                "Lists the assets whose AssetParams may be accessed by this application's approval-program and clear-state-program. The access is read-only.")]
            public ulong[] ForeignAssets;

            [AlgoApiField("global-state-schema")]
            [Tooltip("Holds the maximum number of global state values")]
            public StateSchema GlobalStateSchema;

            [AlgoApiField("local-state-schema")]
            [Tooltip("Holds the maximum number of local state values")]
            public StateSchema LocalStateSchema;

            [AlgoApiField("epp")]
            [Tooltip(
                "Number of additional pages allocated to the application's approval and clear state programs. Each ExtraProgramPages is 2048 bytes.")]
            public ulong ExtraProgramPages;

            [AlgoApiField("apbx")]
            [Tooltip("The boxes that should be made available for the runtime of the program.")]
            public BoxRef[] Boxes;

            public bool Equals(Params other)
            {
                return ApplicationId.Equals(other.ApplicationId)
                       && OnComplete == other.OnComplete
                       && ArrayComparer.Equals(Accounts, other.Accounts)
                       && ApprovalProgram.Equals(other.ApprovalProgram)
                       && ArrayComparer.Equals(AppArguments, other.AppArguments)
                       && ClearStateProgram.Equals(other.ClearStateProgram)
                       && ArrayComparer.Equals(ForeignApps, other.ForeignApps)
                       && ArrayComparer.Equals(ForeignAssets, other.ForeignAssets)
                       && GlobalStateSchema.Equals(other.GlobalStateSchema)
                       && LocalStateSchema.Equals(other.LocalStateSchema)
                       && ExtraProgramPages.Equals(other.ExtraProgramPages)
                       && ArrayComparer.Equals(Boxes, other.Boxes)
                    ;
            }
        }
    }
}