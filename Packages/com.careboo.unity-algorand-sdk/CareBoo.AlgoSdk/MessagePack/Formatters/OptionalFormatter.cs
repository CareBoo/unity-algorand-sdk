using System;
using MessagePack;
using MessagePack.Formatters;

namespace AlgoSdk.MsgPack.Formatters
{
    public sealed class OptionalFormatter<T>
        : IMessagePackFormatter<Optional<T>>
        where T : struct, IEquatable<T>
    {
        public Optional<T> Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            if (reader.IsNil)
                return default;

            var childFormatter = options.Resolver.GetFormatter<T>();
            if (childFormatter == null)
                throw new InvalidOperationException($"Could not resolve formatter for type {typeof(T)}");

            return childFormatter.Deserialize(ref reader, options);
        }

        public void Serialize(ref MessagePackWriter writer, Optional<T> value, MessagePackSerializerOptions options)
        {
            if (!value.HasValue)
                writer.WriteNil();

            var childFormatter = options.Resolver.GetFormatter<T>();
            if (childFormatter == null)
                throw new InvalidOperationException($"Could not resolve formatter for type {typeof(T)}");

            childFormatter.Serialize(ref writer, value.Value, options);
        }
    }
}
