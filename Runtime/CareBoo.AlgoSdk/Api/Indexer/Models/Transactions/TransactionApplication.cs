using System;
using UnityEngine;

namespace AlgoSdk
{
    /// <summary>
    /// Contains information regarding a completed <see cref="AppCallTxn"/>.
    /// </summary>
    [AlgoApiObject, Serializable]
    public partial struct TransactionApplication
        : IEquatable<TransactionApplication>
    {
        [SerializeField, Tooltip("List of accounts in addition to the sender that may be accessed from the application's approval-program and clear-state-program.")]
        string[] accounts;

        [SerializeField, Tooltip("Transaction specific arguments accessed from the application's approval-program and clear-state-program.")]
        string[] applicationArgs;

        [SerializeField, Tooltip("Id of the application being configured or empty if creating.")]
        ulong applicationId;

        [SerializeField, Tooltip("Logic executed for every application transaction, except when on-completion is set to \"clear\". It can read and write global state for the application, as well as account-specific local state. Approval programs may reject the transaction.")]
        byte[] approvalProgram;

        [SerializeField, Tooltip("Logic executed for application transactions with on-completion set to \"clear\". It can read and write global state for the application, as well as account-specific local state. Clear state programs cannot reject the transaction.")]
        byte[] clearStateProgram;

        [SerializeField, Tooltip("Specifies the additional app program len requested in pages.")]
        ulong extraProgramPages;

        [SerializeField, Tooltip("Lists the applications in addition to the application-id whose global states may be accessed by this application's approval-program and clear-state-program. The access is read-only.")]
        ulong[] foreignApps;

        [SerializeField, Tooltip("Lists the assets whose parameters may be accessed by this application's ApprovalProgram and ClearStateProgram. The access is read-only.")]
        ulong[] foreignAssets;

        [SerializeField, Tooltip("Represents a global-state schema. These schemas determine how much storage may be used in a global-state for an application. The more space used, the larger minimum balance must be maintained in the account holding the data.")]
        StateSchema globalStateSchema;

        [SerializeField, Tooltip("Represents a local-state schema. These schemas determine how much storage may be used in a local-state for an application. The more space used, the larger minimum balance must be maintained in the account holding the data.")]
        StateSchema localStateSchema;

        [SerializeField, Tooltip("Defines what additional actions occur with the transaction.")]
        OnCompletion onCompletion;

        /// <summary>
        /// List of accounts in addition to the sender that may be accessed from the application's approval-program and clear-state-program.
        /// </summary>
        [AlgoApiField("accounts", null)]
        public string[] Accounts
        {
            get => accounts;
            set => accounts = value;
        }

        /// <summary>
        /// Transaction specific arguments accessed from the application's approval-program and clear-state-program.
        /// </summary>
        [AlgoApiField("application-args", null)]
        public string[] ApplicationArgs
        {
            get => applicationArgs;
            set => applicationArgs = value;
        }

        /// <summary>
        /// Id of the application being configured or empty if creating.
        /// </summary>
        [AlgoApiField("application-id", null)]
        public ulong ApplicationId
        {
            get => applicationId;
            set => applicationId = value;
        }

        /// <summary>
        /// Logic executed for every application transaction, except when on-completion is set to "clear". It can read and write global state for the application, as well as account-specific local state. Approval programs may reject the transaction.
        /// </summary>
        [AlgoApiField("approval-program", null)]
        public byte[] ApprovalProgram
        {
            get => approvalProgram;
            set => approvalProgram = value;
        }

        /// <summary>
        /// Logic executed for application transactions with on-completion set to "clear". It can read and write global state for the application, as well as account-specific local state. Clear state programs cannot reject the transaction.
        /// </summary>
        [AlgoApiField("clear-state-program", null)]
        public byte[] ClearStateProgram
        {
            get => clearStateProgram;
            set => clearStateProgram = value;
        }

        /// <summary>
        /// Specifies the additional app program len requested in pages.
        /// </summary>
        [AlgoApiField("extra-program-pages", null)]
        public ulong ExtraProgramPages
        {
            get => extraProgramPages;
            set => extraProgramPages = value;
        }

        /// <summary>
        /// Lists the applications in addition to the application-id whose global states may be accessed by this application's approval-program and clear-state-program. The access is read-only.
        /// </summary>
        [AlgoApiField("foreign-apps", null)]
        public ulong[] ForeignApps
        {
            get => foreignApps;
            set => foreignApps = value;
        }

        /// <summary>
        /// Lists the assets whose parameters may be accessed by this application's ApprovalProgram and ClearStateProgram. The access is read-only.
        /// </summary>
        [AlgoApiField("foreign-assets", null)]
        public ulong[] ForeignAssets
        {
            get => foreignAssets;
            set => foreignAssets = value;
        }

        /// <summary>
        /// Represents a global-state schema. These schemas determine how much storage may be used in a global-state for an application. The more space used, the larger minimum balance must be maintained in the account holding the data.
        /// </summary>
        [AlgoApiField("global-state-schema", null)]
        public StateSchema GlobalStateSchema
        {
            get => globalStateSchema;
            set => globalStateSchema = value;
        }

        /// <summary>
        /// Represents a local-state schema. These schemas determine how much storage may be used in a local-state for an application. The more space used, the larger minimum balance must be maintained in the account holding the data.
        /// </summary>
        [AlgoApiField("local-state-schema", null)]
        public StateSchema LocalStateSchema
        {
            get => localStateSchema;
            set => localStateSchema = value;
        }

        /// <summary>
        /// Defines what additional actions occur with the transaction.
        /// </summary>
        [AlgoApiField("on-completion", null)]
        public OnCompletion OnCompletion
        {
            get => onCompletion;
            set => onCompletion = value;
        }

        public bool Equals(TransactionApplication other)
        {
            return ApplicationId.Equals(other.ApplicationId)
                && ArrayComparer.Equals(ApplicationArgs, other.ApplicationArgs)
                && ArrayComparer.Equals(Accounts, other.Accounts)
                && ArrayComparer.Equals(ForeignApps, other.ForeignApps)
                && ArrayComparer.Equals(ForeignAssets, other.ForeignAssets)
                && OnCompletion.Equals(other.OnCompletion)
                ;
        }
    }
}
