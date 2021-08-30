using System;
using MessagePack;
using MessagePack.Formatters;
using Unity.Collections;

namespace AlgoSdk.MsgPack.Formatters
{
    public sealed class TealValueFormatter : IMessagePackFormatter<TealValue>
    {
        public TealValue Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            TealValue result = default;
            var fixedStringFormatter = options.Resolver.GetFormatter<FixedString32Bytes>();
            var length = reader.ReadMapHeader();
            if (length != 2)
                throw new ArgumentOutOfRangeException($"Expecting only 2 fields in Teal Value object, but got {length} fields.");
            for (var i = 0; i < length; i++)
            {
                var key = fixedStringFormatter.Deserialize(ref reader, options);
                if (key == "type")
                    result.Type = options.Resolver.GetFormatter<TealValueType>().Deserialize(ref reader, options);
                else if (key == "bytes")
                    result.Bytes = options.Resolver.GetFormatter<TealBytes>().Deserialize(ref reader, options);
                else if (key == "uint")
                    result.Uint = options.Resolver.GetFormatter<ulong>().Deserialize(ref reader, options);
                else
                    throw new ArgumentOutOfRangeException($"Found unexpected key in message: {key}");
            }
            return result;
        }

        public void Serialize(ref MessagePackWriter writer, TealValue value, MessagePackSerializerOptions options)
        {
            writer.WriteMapHeader(2);
            var resolver = options.Resolver;
            var fixedStringFormatter = resolver.GetFormatter<FixedString32Bytes>();
            fixedStringFormatter.Serialize(ref writer, "type", options);
            switch (value.Type)
            {
                case TealValueType.Bytes:
                    fixedStringFormatter.Serialize(ref writer, "bytes", options);
                    resolver.GetFormatter<TealBytes>().Serialize(ref writer, value.Bytes, options);
                    break;
                case TealValueType.Uint:
                    fixedStringFormatter.Serialize(ref writer, "uint", options);
                    resolver.GetFormatter<ulong>().Serialize(ref writer, value.Uint, options);
                    break;
                default:
                    throw new NotSupportedException($"Cannot serialize {nameof(TealValue)} with type: {value.Type}");
            }
        }
    }
}
