namespace Algorand.Unity.Formatters
{
    public sealed class HashTypeFormatter : KeywordByteEnumFormatter<HashType>
    {
        public HashTypeFormatter() : base(HashTypeExtensions.TypeToString) { }
    }
}
