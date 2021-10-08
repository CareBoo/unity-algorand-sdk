using AlgoSdk.Formatters;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiFormatter(typeof(TransactionTypeFormatter))]
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
        public static readonly FixedString32Bytes[] TypeToString = new FixedString32Bytes[(int)TransactionType.Count]
        {
            default,
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
                : txType.ToFixedString()
                ;
        }
    }
}
