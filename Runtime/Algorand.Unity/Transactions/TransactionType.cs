using Unity.Collections;

namespace Algorand.Unity
{
    /// <summary>
    /// An enum representing the type of transaction.
    /// </summary>
    public enum TransactionType : byte
    {
        None,
        Payment,
        KeyRegistration,
        AssetTransfer,
        AssetFreeze,
        AssetConfiguration,
        ApplicationCall,
        Count,
    }

    public static class TransactionTypeExtensions
    {
        public static readonly string[] TypeToString = new string[(int)TransactionType.Count]
        {
            string.Empty,
            "pay",
            "keyreg",
            "axfer",
            "afrz",
            "acfg",
            "appl"
        };

        public static FixedString32Bytes ToFixedString(this TransactionType txType)
        {
            return TypeToString[(int)txType];
        }

        public static Optional<FixedString32Bytes> ToOptionalFixedString(this TransactionType txType)
        {
            return txType == TransactionType.None
                ? default(Optional<FixedString32Bytes>)
                : (Optional<FixedString32Bytes>)txType.ToFixedString()
                ;
        }
    }
}
