using Unity.Collections;

namespace AlgoSdk.Formatters
{
    public sealed class SignatureTypeFormatter : ByteEnumFormatter<SignatureType>
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
