using Unity.Collections;

namespace AlgoSdk.Formatters
{
    public class TransactionTypeFormatter : ByteEnumFormatter<TransactionType>
    {
        private static readonly FixedString32Bytes[] typeToString = new FixedString32Bytes[(int)TransactionType.Count]
        {
            default,
            "pay",
            "keyreg",
            "axfer",
            "afrz",
            "acfg",
            "appl"
        };

        public TransactionTypeFormatter() : base(typeToString) { }
    }
}
