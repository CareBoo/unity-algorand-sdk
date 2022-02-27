using System;
using UnityEngine;

namespace AlgoSdk
{
    /// <summary>
    /// Stores the global information associated with an application.
    /// </summary>
    [AlgoApiObject]
    [Serializable]
    public partial struct ApplicationParams
        : IEquatable<ApplicationParams>
    {
        /// <summary>
        /// Logic executed for every application transaction, except when on-completion is set to "clear". It can read and write global state for the application, as well as account-specific local state. Approval programs may reject the transaction.
        /// </summary>
        [AlgoApiField("approval-program", null)]
        [Tooltip("Logic executed for every application transaction, except when on-completion is set to \"clear\". It can read and write global state for the application, as well as account-specific local state. Approval programs may reject the transaction.")]
        public CompiledTeal ApprovalProgram;

        /// <summary>
        /// Logic executed for application transactions with on-completion set to "clear". It can read and write global state for the application, as well as account-specific local state. Clear state programs cannot reject the transaction.
        /// </summary>
        [AlgoApiField("clear-state-program", null)]
        [Tooltip("Logic executed for application transactions with on-completion set to \"clear\". It can read and write global state for the application, as well as account-specific local state. Clear state programs cannot reject the transaction.")]
        public CompiledTeal ClearStateProgram;

        /// <summary>
        /// The address that created this application. This is the address where the parameters and global state for this application can be found.
        /// </summary>
        [AlgoApiField("creator", null)]
        [Tooltip("The address that created this application. This is the address where the parameters and global state for this application can be found.")]
        public Address Creator;

        /// <summary>
        /// [epp] the amount of extra program pages available to this app.
        /// </summary>
        [AlgoApiField("extra-program-pages", null)]
        [Tooltip("[epp] the amount of extra program pages available to this app.")]
        public ulong ExtraProgramPages;

        /// <summary>
        /// global state
        /// </summary>
        [AlgoApiField("global-state", null)]
        [Tooltip("global state")]
        public TealKeyValue[] GlobalState;

        /// <summary>
        /// global schema
        /// </summary>
        [AlgoApiField("global-state-schema", null)]
        [Tooltip("global schema")]
        public StateSchema GlobalStateSchema;

        /// <summary>
        /// local schema
        /// </summary>
        [AlgoApiField("local-state-schema", null)]
        [Tooltip("local schema")]
        public StateSchema LocalStateSchema;

        public bool Equals(ApplicationParams other)
        {
            return Creator.Equals(other.Creator)
                && ArrayComparer.Equals(ApprovalProgram, other.ApprovalProgram)
                && ArrayComparer.Equals(ClearStateProgram, other.ClearStateProgram)
                && ExtraProgramPages.Equals(other.ExtraProgramPages)
                && ArrayComparer.Equals(GlobalState, other.GlobalState)
                && GlobalStateSchema.Equals(other.GlobalStateSchema)
                && LocalStateSchema.Equals(other.LocalStateSchema)
                ;
        }
    }
}
