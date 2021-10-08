using Unity.Collections;

namespace AlgoSdk.Formatters
{
    public sealed class OnCompletionFormatter : KeywordByteEnumFormatter<OnCompletion>
    {
        private static readonly FixedString32Bytes[] typeToString = new FixedString32Bytes[]
        {
            default,
            "noop",
            "optin",
            "closeout",
            "clear",
            "update",
            "delete"
        };

        public OnCompletionFormatter() : base(typeToString) { }
    }
}
