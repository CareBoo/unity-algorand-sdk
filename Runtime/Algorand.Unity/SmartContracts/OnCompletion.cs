using Unity.Collections;

namespace Algorand.Unity
{
    /// <summary>
    /// [apan] defines the what additional actions occur with the transaction.
    /// </summary>
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

    public static class OnCompletionExtensions
    {
        public static readonly string[] TypeToString = new[]
        {
            "noop",
            "optin",
            "closeout",
            "clear",
            "update",
            "delete"
        };

        public static readonly FixedString32Bytes[] TypeToFixedString = new FixedString32Bytes[]
        {
            "noop",
            "optin",
            "closeout",
            "clear",
            "update",
            "delete"
        };

        public static FixedString32Bytes ToFixedString(this OnCompletion onCompletion)
        {
            return TypeToFixedString[(byte)onCompletion];
        }
    }
}
