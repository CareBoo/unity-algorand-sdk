using Algorand.Unity.Json;
using Algorand.Unity.MessagePack;
using Unity.Collections;

namespace Algorand.Unity.Formatters
{
    public class StringFormatter : IAlgoApiFormatter<string>
    {
        public string Deserialize(ref JsonReader reader)
        {
            if (reader.TryReadNull())
            {
                return null;
            }
            var text = new NativeText(Allocator.Persistent);
            try
            {
                reader.ReadString(ref text);
                return text.ToString();
            }
            finally
            {
                text.Dispose();
            }
        }

        public string Deserialize(ref MessagePackReader reader)
        {
            if (reader.TryReadNil())
            {
                return null;
            }
            var text = new NativeText(Allocator.Persistent);
            try
            {
                reader.ReadString(ref text);
                return text.ToString();
            }
            finally
            {
                text.Dispose();
            }
        }

        public void Serialize(ref JsonWriter writer, string value)
        {
            using var text = new NativeText(value, Allocator.Persistent);
            writer.WriteString(text);
        }

        public void Serialize(ref MessagePackWriter writer, string value)
        {
            using var text = new NativeText(value, Allocator.Persistent);
            writer.WriteString(text);
        }
    }
}
