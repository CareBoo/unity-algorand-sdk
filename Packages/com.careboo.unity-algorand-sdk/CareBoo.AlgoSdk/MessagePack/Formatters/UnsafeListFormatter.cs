using MessagePack;
using MessagePack.Formatters;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace AlgoSdk
{
    public sealed class UnsafeListFormatter<T> : IMessagePackFormatter<UnsafeList<T>>
        where T : unmanaged
    {
        public UnsafeList<T> Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            var length = reader.ReadArrayHeader();
            var result = new UnsafeList<T>(length, Allocator.Persistent);
            for (var i = 0; i < length; i++)
                result.Add(options.Resolver.GetFormatter<T>().Deserialize(ref reader, options));
            return result;
        }

        public void Serialize(ref MessagePackWriter writer, UnsafeList<T> value, MessagePackSerializerOptions options)
        {
            writer.WriteArrayHeader(value.Length);
            for (var i = 0; i < value.Length; i++)
                options.Resolver.GetFormatter<T>().Serialize(ref writer, value[i], options);
        }
    }
}
