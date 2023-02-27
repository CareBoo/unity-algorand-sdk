using Algorand.Unity.Json;
using Algorand.Unity.MessagePack;

namespace Algorand.Unity.Formatters
{
    public class BoolFormatter : IAlgoApiFormatter<bool>
    {
        public bool Deserialize(ref JsonReader reader)
        {
            reader.ReadBool(out bool val)
                .ThrowIfError(reader);
            return val;
        }

        public bool Deserialize(ref MessagePackReader reader)
        {
            return reader.ReadBool();
        }

        public void Serialize(ref JsonWriter writer, bool value)
        {
            writer.WriteBool(value);
        }

        public void Serialize(ref MessagePackWriter writer, bool value)
        {
            writer.Write(value);
        }
    }
}
