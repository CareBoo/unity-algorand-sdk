using AlgoSdk.Formatters;

namespace AlgoSdk
{
    /// <summary>
    /// [apan] defines the what additional actions occur with the transaction.
    /// </summary>
    [AlgoApiFormatter(typeof(OnCompletionFormatter))]
    public enum OnCompletion : byte
    {
        /// <summary>
        /// Generic application calls to execute the <see cref="AppCallTxn.ApprovalProgram"/>
        /// </summary>
        NoOp,

        /// <summary>
        /// Accounts use this transaction to opt into the smart contract to participate (local storage usage).
        /// </summary>
        OptIn,

        /// <summary>
        /// Accounts use this transaction to close out their participation in the contract. This call can fail based on the TEAL logic, preventing the account from removing the contract from its balance record.
        /// </summary>
        CloseOut,

        /// <summary>
        /// Similar to CloseOut, but the transaction will always clear a contract from the account’s balance record whether the program succeeds or fails.
        /// </summary>
        Clear,

        /// <summary>
        /// Transaction to update TEAL Programs for a contract.
        /// </summary>
        Update,

        /// <summary>
        /// Transaction to delete the application.
        /// </summary>
        Delete
    }
}
