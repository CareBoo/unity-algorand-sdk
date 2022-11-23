namespace Algorand.Unity.Formatters
{
    public class TransactionTypeFormatter : KeywordByteEnumFormatter<TransactionType>
    {
        public TransactionTypeFormatter() : base(TransactionTypeExtensions.TypeToString) { }
    }
}
