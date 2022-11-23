namespace Algorand.Unity.Experimental.Abi
{
    public enum AbiTransactionType : byte
    {
        None,

        /// <summary>
        /// A payment transaction.
        /// </summary>
        pay,

        /// <summary>
        /// A key-registration transaction.
        /// </summary>
        keyreg,

        /// <summary>
        /// An asset transfer transactions.
        /// </summary>
        axfer,

        /// <summary>
        /// An asset freeze transaction.
        /// </summary>
        afrz,

        /// <summary>
        /// An asset configure transaction.
        /// </summary>
        acfg,

        /// <summary>
        /// An applicatiopn call transaction.
        /// </summary>
        appl,

        /// <summary>
        /// Any transaction type.
        /// </summary>
        txn,
    }

    public static class AbiTransactionTypeExtensions
    {
        public static AbiTransactionType Parse(string type) => type switch
        {
            "txn" => AbiTransactionType.txn,
            "pay" => AbiTransactionType.pay,
            "keyreg" => AbiTransactionType.keyreg,
            "axfer" => AbiTransactionType.axfer,
            "afrz" => AbiTransactionType.afrz,
            "acfg" => AbiTransactionType.acfg,
            "appl" => AbiTransactionType.appl,
            _ => AbiTransactionType.None
        };

        public static bool Equals(this AbiTransactionType abiType, TransactionType txnType)
        {
            return abiType == AbiTransactionType.txn || (byte)abiType == (byte)txnType;
        }
    }
}
