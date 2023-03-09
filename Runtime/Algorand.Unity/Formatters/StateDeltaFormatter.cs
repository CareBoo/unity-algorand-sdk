using Algorand.Unity.Json;
using Algorand.Unity.MessagePack;
using Unity.Collections;

namespace Algorand.Unity
{
    public sealed class StateDeltaFormatter : IAlgoApiFormatter<StateDelta>
    {
        public StateDelta Deserialize(ref JsonReader reader)
        {
            if (reader.TryReadNull())
                return null;
            if (!reader.TryRead(JsonToken.ObjectBegin))
                JsonReadError.IncorrectType.ThrowIfError(reader);

            using var map = new NativeList<ValueDeltaKeyValue>(64, Allocator.Temp);
            while (reader.Peek() != JsonToken.ObjectEnd && reader.Peek() != JsonToken.None)
            {
                FixedString64Bytes key = default;
                reader.ReadString(ref key).ThrowIfError(reader);
                var value = AlgoApiFormatterCache<ValueDelta>.Formatter.Deserialize(ref reader);
                map.Add(new ValueDeltaKeyValue { Key = key, Value = value });
            }
            if (!reader.TryRead(JsonToken.ObjectEnd))
                JsonReadError.IncorrectFormat.ThrowIfError(reader);
            return map.AsArray().ToArray();
        }

        public StateDelta Deserialize(ref MessagePackReader reader)
        {
            if (reader.TryReadNil())
                return null;

            var result = new ValueDeltaKeyValue[reader.ReadMapHeader()];
            for (var i = 0; i < result.Length; i++)
            {
                FixedString64Bytes key = default;
                reader.ReadString(ref key);
                var value = AlgoApiFormatterCache<ValueDelta>.Formatter.Deserialize(ref reader);
                result[i] = new ValueDeltaKeyValue { Key = key, Value = value };
            }
            return result;
        }

        public void Serialize(ref JsonWriter writer, StateDelta value)
        {
            var map = value.Map;
            if (map == null)
                writer.WriteNull();

            writer.BeginObject();
            for (var i = 0; i < map.Length; i++)
            {
                if (i > 0)
                    writer.BeginNextItem();
                writer.WriteObjectKey(map[i].Key);
                AlgoApiFormatterCache<ValueDelta>.Formatter.Serialize(ref writer, map[i].Value);
            }
            writer.EndObject();
        }

        public void Serialize(ref MessagePackWriter writer, StateDelta value)
        {
            var map = value.Map;
            if (map == null)
                writer.WriteNil();

            writer.WriteMapHeader(map.Length);
            for (var i = 0; i < map.Length; i++)
            {
                writer.WriteString(map[i].Key);
                AlgoApiFormatterCache<ValueDelta>.Formatter.Serialize(ref writer, map[i].Value);
            }
        }
    }
}
