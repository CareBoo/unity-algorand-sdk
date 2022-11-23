using Algorand.Unity.Crypto;
using Algorand.Unity.Json;
using Algorand.Unity.MessagePack;
using Unity.Collections;

namespace Algorand.Unity.Formatters
{
    public sealed class GenesisHashFormatter : IAlgoApiFormatter<GenesisHash>
    {
        public GenesisHash Deserialize(ref JsonReader reader)
        {
            var s = new FixedString64Bytes();
            reader.ReadString(ref s)
                .ThrowIfError(reader);
            GenesisHash result = default;
            result.CopyFromBase64(s);
            return result;
        }

        public GenesisHash Deserialize(ref MessagePackReader reader)
        {
            return AlgoApiFormatterCache<Sha512_256_Hash>.Formatter.Deserialize(ref reader);
        }

        public void Serialize(ref JsonWriter writer, GenesisHash value)
        {
            var fs = new FixedString64Bytes();
            value.CopyToBase64(ref fs);
            writer.WriteString(fs);
        }

        public void Serialize(ref MessagePackWriter writer, GenesisHash value)
        {
            AlgoApiFormatterCache<Sha512_256_Hash>.Formatter.Serialize(ref writer, value);
        }
    }
}
