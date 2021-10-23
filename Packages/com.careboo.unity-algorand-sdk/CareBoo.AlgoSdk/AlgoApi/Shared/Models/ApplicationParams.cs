using System;

namespace AlgoSdk
{
    /// <summary>
    /// Stores the global information associated with an application.
    /// </summary>
    [AlgoApiObject]
    public struct ApplicationParams
        : IEquatable<ApplicationParams>
    {
        /// <summary>
        /// Logic executed for every application transaction, except when on-completion is set to "clear". It can read and write global state for the application, as well as account-specific local state. Approval programs may reject the transaction.
        /// </summary>
        [AlgoApiField("approval-program", null)]
        public byte[] ApprovalProgram;

        /// <summary>
        /// Logic executed for application transactions with on-completion set to "clear". It can read and write global state for the application, as well as account-specific local state. Clear state programs cannot reject the transaction.
        /// </summary>
        [AlgoApiField("clear-state-program", null)]
        public byte[] ClearStateProgram;

        /// <summary>
        /// The address that created this application. This is the address where the parameters and global state for this application can be found.
        /// </summary>
        [AlgoApiField("creator", null)]
        public Address Creator;

        /// <summary>
        /// [epp] the amount of extra program pages available to this app.
        /// </summary>
        [AlgoApiField("extra-program-pages", null)]
        public ulong ExtraProgramPages;

        /// <summary>
        /// [\gs] global state
        /// </summary>
        [AlgoApiField("global-state", null)]
        public TealKeyValue[] GlobalState;

        /// <summary>
        /// [\lsch] global schema
        /// </summary>
        [AlgoApiField("global-state-schema", null)]
        public StateSchema GlobalStateSchema;

        /// <summary>
        /// [\lsch] local schema
        /// </summary>
        [AlgoApiField("local-state-schema", null)]
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
