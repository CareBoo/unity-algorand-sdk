using Algorand.Unity.MessagePack;

namespace Algorand.Unity.Formatters
{
    public sealed class OnCompletionFormatter : KeywordByteEnumFormatter<OnCompletion>
    {
        private static readonly string[] typeToString = new string[]
        {
            string.Empty,
            "noop",
            "optin",
            "closeout",
            "clear",
            "update",
            "delete"
        };

        public OnCompletionFormatter() : base(typeToString) { }

        public override OnCompletion Deserialize(ref MessagePackReader reader)
        {
            return (OnCompletion)reader.ReadByte();
        }

        public override void Serialize(ref MessagePackWriter writer, OnCompletion value)
        {
            writer.Write((byte)value);
        }
    }
}
