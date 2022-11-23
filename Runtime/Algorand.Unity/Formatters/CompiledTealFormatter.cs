using Algorand.Unity.Json;
using Algorand.Unity.MessagePack;

namespace Algorand.Unity
{
    public class CompiledTealFormatter : IAlgoApiFormatter<CompiledTeal>
    {
        public CompiledTeal Deserialize(ref JsonReader reader)
        {
            return AlgoApiFormatterCache<byte[]>.Formatter.Deserialize(ref reader);
        }

        public CompiledTeal Deserialize(ref MessagePackReader reader)
        {
            return AlgoApiFormatterCache<byte[]>.Formatter.Deserialize(ref reader);
        }

        public void Serialize(ref JsonWriter writer, CompiledTeal value)
        {
            AlgoApiFormatterCache<byte[]>.Formatter.Serialize(ref writer, value);
        }

        public void Serialize(ref MessagePackWriter writer, CompiledTeal value)
        {
            AlgoApiFormatterCache<byte[]>.Formatter.Serialize(ref writer, value);
        }
    }
}
