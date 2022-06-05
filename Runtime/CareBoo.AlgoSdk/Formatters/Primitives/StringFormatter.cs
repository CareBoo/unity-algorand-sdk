using AlgoSdk.Json;
using AlgoSdk.MessagePack;
using Unity.Collections;

namespace AlgoSdk.Formatters
{
    public class StringFormatter : IAlgoApiFormatter<string>
    {
        public string Deserialize(ref JsonReader reader)
        {
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
