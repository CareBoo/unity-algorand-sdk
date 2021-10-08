namespace AlgoSdk.Formatters
{
    public sealed class SignatureTypeFormatter : KeywordByteEnumFormatter<SignatureType>
    {
        public SignatureTypeFormatter() : base(SignatureTypeExtensions.TypeToString) { }
    }
}
