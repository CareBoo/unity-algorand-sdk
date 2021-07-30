using System;
using MessagePack;
using Unity.Collections;

namespace AlgoSdk.MsgPack.Formatters
{
    public sealed class NativeReferenceFormatter<T>
    : global::MessagePack.Formatters.IMessagePackFormatter<global::Unity.Collections.NativeReference<T>>
    where T : unmanaged
    {
        public NativeReference<T> Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            if (reader.IsNil)
                return default;

            var childFormatter = options.Resolver.GetFormatter<T>();
            if (childFormatter == null)
                throw new InvalidOperationException($"Could not resolve formatter for type {typeof(T)}");

            var value = childFormatter.Deserialize(ref reader, options);
            return new NativeReference<T>(Allocator.Persistent) { Value = value };
        }

        public void Serialize(ref MessagePackWriter writer, NativeReference<T> value, MessagePackSerializerOptions options)
        {
            if (!value.IsCreated)
                writer.WriteNil();

            var childFormatter = options.Resolver.GetFormatter<T>();
            if (childFormatter == null)
                throw new InvalidOperationException($"Could not resolve formatter for type {typeof(T)}");

            childFormatter.Serialize(ref writer, value.Value, options);
        }
    }
}
