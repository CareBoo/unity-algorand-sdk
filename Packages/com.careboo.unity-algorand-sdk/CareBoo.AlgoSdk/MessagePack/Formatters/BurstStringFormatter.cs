using System.Collections;
using AlgoSdk.MsgPack;
using MessagePack;
using MessagePack.Formatters;
using Unity.Collections;

namespace AlgoSdk
{
    public sealed class FixedStringFormatter<T> : IMessagePackFormatter<T>
        where T : struct, IUTF8Bytes, INativeList<byte>
    {
        public T Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            var result = new T();
            if (reader.TryReadStringSpan(out var span))
                span.CopyTo(ref result);
            else
                reader.ReadStringSequence().Value.CopyTo(ref result);
            return result;
        }

        public void Serialize(ref MessagePackWriter writer, T value, MessagePackSerializerOptions options)
        {
            writer.WriteString(value.ToReadOnlySpan());
        }
    }

    public sealed class NativeTextFormatter : IMessagePackFormatter<NativeText>
    {
        public NativeText Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            var result = new NativeText(Allocator.Persistent);
            if (reader.TryReadStringSpan(out var span))
                span.CopyTo(ref result);
            else
                reader.ReadStringSequence().Value.CopyTo(ref result);
            return result;
        }

        public void Serialize(ref MessagePackWriter writer, NativeText value, MessagePackSerializerOptions options)
        {
            writer.WriteString(value.ToReadOnlySpan());
        }
    }
}
