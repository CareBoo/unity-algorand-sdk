using Unity.Collections;

namespace AlgoSdk.Formatters
{
    public sealed class SignatureTypeFormatter : KeywordByteEnumFormatter<SignatureType>
    {
        private static readonly FixedString32Bytes[] typeToString = new FixedString32Bytes[]
        {
            default,
            "sig",
            "msig",
            "lsig"
        };

        public SignatureTypeFormatter() : base(typeToString) { }
    }
}
