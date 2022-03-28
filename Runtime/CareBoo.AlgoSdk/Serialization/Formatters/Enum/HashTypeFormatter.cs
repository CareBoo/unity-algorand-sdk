namespace AlgoSdk.Formatters
{
    public sealed class HashTypeFormatter : KeywordByteEnumFormatter<HashType>
    {
        public HashTypeFormatter() : base(HashTypeExtensions.TypeToString) { }
    }
}
