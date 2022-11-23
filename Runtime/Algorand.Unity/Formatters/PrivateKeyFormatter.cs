using Algorand.Unity.Json;
using Algorand.Unity.MessagePack;

namespace Algorand.Unity.Formatters
{
    public class PrivateKeyFormatter : IAlgoApiFormatter<PrivateKey>
    {
        public PrivateKey Deserialize(ref JsonReader reader)
        {
            var s = AlgoApiFormatterCache<string>.Formatter.Deserialize(ref reader);
            return PrivateKey.FromString(s);
        }

        public PrivateKey Deserialize(ref MessagePackReader reader)
        {
            var bytes = reader.ReadBytes();
            PrivateKey result = default;
            for (var i = 0; i < bytes.Length; i++)
                result[i] = bytes[i];
            return result;
        }

        public void Serialize(ref JsonWriter writer, PrivateKey value)
        {
            var s = value.ToString();
            AlgoApiFormatterCache<string>.Formatter.Serialize(ref writer, s);
        }

        public void Serialize(ref MessagePackWriter writer, PrivateKey value)
        {
            unsafe
            {
                writer.WriteBytes(value.GetUnsafePtr(), value.Length);
            }
        }
    }
}
