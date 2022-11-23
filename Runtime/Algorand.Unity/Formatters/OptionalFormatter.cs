using System;
using Algorand.Unity.Json;
using Algorand.Unity.MessagePack;

namespace Algorand.Unity.Formatters
{
    public sealed class OptionalFormatter<T>
        : IAlgoApiFormatter<Optional<T>>
        where T : struct, IEquatable<T>
    {
        public Optional<T> Deserialize(ref JsonReader reader)
        {
            if (reader.Peek() == JsonToken.Null)
            {
                reader.ReadNull();
                return default;
            }

            return AlgoApiFormatterCache<T>.Formatter.Deserialize(ref reader);
        }

        public Optional<T> Deserialize(ref MessagePackReader reader)
        {
            return reader.TryReadNil()
                ? default
                : AlgoApiFormatterCache<T>.Formatter.Deserialize(ref reader);
        }

        public void Serialize(ref JsonWriter writer, Optional<T> value)
        {
            if (!value.HasValue)
            {
                writer.WriteNull();
                return;
            }

            AlgoApiFormatterCache<T>.Formatter.Serialize(ref writer, value.Value);
        }

        public void Serialize(ref MessagePackWriter writer, Optional<T> value)
        {
            if (!value.HasValue)
            {
                writer.WriteNil();
                return;
            }

            AlgoApiFormatterCache<T>.Formatter.Serialize(ref writer, value.Value);
        }
    }
}
