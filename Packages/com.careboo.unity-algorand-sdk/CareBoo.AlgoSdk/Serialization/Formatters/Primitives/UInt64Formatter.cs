using AlgoSdk.Json;
using AlgoSdk.MessagePack;

namespace AlgoSdk.Formatters
{
    public class UInt64Formatter : IAlgoApiFormatter<ulong>
    {
        public ulong Deserialize(ref JsonReader reader)
        {
            reader.ReadNumber(out ulong val)
                .ThrowIfError();
            return val;
        }

        public ulong Deserialize(ref MessagePackReader reader)
        {
            return reader.ReadUInt64();
        }

        public void Serialize(ref JsonWriter writer, ulong value)
        {
            writer.WriteNumber(value);
        }

        public void Serialize(ref MessagePackWriter writer, ulong value)
        {
            writer.Write(value);
        }
    }
}
