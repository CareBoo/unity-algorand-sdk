using AlgoSdk.LowLevel;
using MessagePack;
using MessagePack.Formatters;
using Unity.Collections;

namespace AlgoSdk.MsgPack.Formatters
{
    public sealed class ByteArrayFormatter<TByteArray> : IMessagePackFormatter<TByteArray>
        where TByteArray : unmanaged, IByteArray
    {
        public static ByteArrayFormatter<TByteArray> Instance = new ByteArrayFormatter<TByteArray>();

        public TByteArray Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            return options.IsJson
                ? Base64ByteArrayFormatter<TByteArray>.Instance.Deserialize(ref reader, options)
                : reader.ReadBytes().Value.ToByteArray<TByteArray>();
        }

        public void Serialize(ref MessagePackWriter writer, TByteArray value, MessagePackSerializerOptions options)
        {
            if (options.IsJson)
                Base64ByteArrayFormatter<TByteArray>.Instance.Serialize(ref writer, value, options);
            else
                writer.Write(value.AsReadOnlySpan());
        }
    }

    public sealed class Base64ByteArrayFormatter<T> : IMessagePackFormatter<T>
        where T : struct, IByteArray
    {
        public static Base64ByteArrayFormatter<T> Instance = new Base64ByteArrayFormatter<T>();

        public T Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            using var s = options.Resolver.GetFormatter<NativeText>().Deserialize(ref reader, options);
            T result = default;
            result.CopyFromBase64(s);
            return result;
        }

        public void Serialize(ref MessagePackWriter writer, T value, MessagePackSerializerOptions options)
        {
            var s = new NativeText(value.Length * 4 / 3, Allocator.Temp);
            try
            {
                value.CopyToBase64(ref s);
                options.Resolver.GetFormatter<NativeText>().Serialize(ref writer, s, options);
            }
            finally
            {
                s.Dispose();
            }
        }
    }
}
