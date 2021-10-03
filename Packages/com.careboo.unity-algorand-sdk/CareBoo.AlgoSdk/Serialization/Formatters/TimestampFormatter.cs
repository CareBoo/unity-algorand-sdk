using AlgoSdk.Json;
using AlgoSdk.MessagePack;
using Unity.Collections;

namespace AlgoSdk
{
    public class TimestampFormatter : IAlgoApiFormatter<Timestamp>
    {
        public Timestamp Deserialize(ref JsonReader reader)
        {
            var s = AlgoApiFormatterCache<FixedString64Bytes>.Formatter.Deserialize(ref reader);
            return Timestamp.FromString(s.ToString());
        }

        public Timestamp Deserialize(ref MessagePackReader reader)
        {
            var s = AlgoApiFormatterCache<FixedString64Bytes>.Formatter.Deserialize(ref reader);
            return Timestamp.FromString(s.ToString());
        }

        public void Serialize(ref JsonWriter writer, Timestamp value)
        {
            var s = value.ToString();
            AlgoApiFormatterCache<FixedString64Bytes>.Formatter.Serialize(ref writer, s);
        }

        public void Serialize(ref MessagePackWriter writer, Timestamp value)
        {
            var s = value.ToString();
            AlgoApiFormatterCache<FixedString64Bytes>.Formatter.Serialize(ref writer, s);
        }
    }
}
