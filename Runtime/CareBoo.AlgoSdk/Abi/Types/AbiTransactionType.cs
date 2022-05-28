namespace AlgoSdk.Abi
{
    public enum AbiTransactionType : byte
    {
        None,
        pay,
        keyreg,
        axfer,
        afrz,
        acfg,
        appl,
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
