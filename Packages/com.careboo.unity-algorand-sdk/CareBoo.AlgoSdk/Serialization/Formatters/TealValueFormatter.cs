using System;
using AlgoSdk.Json;
using AlgoSdk.MessagePack;
using Unity.Collections;

namespace AlgoSdk.Formatters
{
    public sealed class TealValueFormatter : IAlgoApiFormatter<TealValue>
    {
        public TealValue Deserialize(ref JsonReader reader)
        {
            TealValue result = default;
            if (!reader.TryRead(JsonToken.ObjectBegin))
                JsonReadError.IncorrectType.ThrowIfError(reader.Char, reader.Position);
            for (var i = 0; i < 2; i++)
            {
                var key = new FixedString32Bytes();
                reader.ReadString(ref key).ThrowIfError(reader.Char, reader.Position);
                if (key == "type")
                    result.Type = AlgoApiFormatterCache<TealValueType>.Formatter.Deserialize(ref reader);
                else if (key == "bytes")
                    result.Bytes = AlgoApiFormatterCache<TealBytes>.Formatter.Deserialize(ref reader);
                else if (key == "uint")
                    result.Uint = AlgoApiFormatterCache<ulong>.Formatter.Deserialize(ref reader);
                else
                    throw new ArgumentOutOfRangeException($"Found unexpected key in message: {key}");
            }
            if (!reader.TryRead(JsonToken.ObjectEnd))
                JsonReadError.IncorrectFormat.ThrowIfError(reader.Char, reader.Position);
            return result;
        }

        public TealValue Deserialize(ref MessagePackReader reader)
        {
            TealValue result = default;
            var length = reader.ReadMapHeader();
            if (length != 2)
                throw new ArgumentOutOfRangeException($"Expecting only 2 fields in Teal Value object, but got {length} fields.");
            for (var i = 0; i < length; i++)
            {
                var key = new FixedString32Bytes();
                reader.ReadString(ref key);
                if (key == "tt")
                    result.Type = AlgoApiFormatterCache<TealValueType>.Formatter.Deserialize(ref reader);
                else if (key == "tb")
                    result.Bytes = AlgoApiFormatterCache<TealBytes>.Formatter.Deserialize(ref reader);
                else if (key == "ui")
                    result.Uint = AlgoApiFormatterCache<ulong>.Formatter.Deserialize(ref reader);
                else
                    throw new ArgumentOutOfRangeException($"Found unexpected key in message: {key}");
            }
            return result;
        }

        public void Serialize(ref JsonWriter writer, TealValue value)
        {
            writer.BeginObject();
            writer.WriteObjectKey(new FixedString32Bytes("type"));
            AlgoApiFormatterCache<TealValueType>.Formatter.Serialize(ref writer, value.Type);
            writer.BeginNextItem();
            switch (value.Type)
            {
                case TealValueType.Bytes:
                    writer.WriteObjectKey(new FixedString32Bytes("bytes"));
                    AlgoApiFormatterCache<TealBytes>.Formatter.Serialize(ref writer, value.Bytes);
                    break;
                case TealValueType.Uint:
                    writer.WriteObjectKey(new FixedString32Bytes("uint"));
                    AlgoApiFormatterCache<ulong>.Formatter.Serialize(ref writer, value.Uint);
                    break;
                default:
                    throw new NotSupportedException($"Cannot serialize {nameof(TealValue)} with type: {value.Type}");
            }
            writer.EndObject();
        }

        public void Serialize(ref MessagePackWriter writer, TealValue value)
        {
            writer.WriteMapHeader(2);
            writer.WriteString(new FixedString32Bytes("tt"));
            AlgoApiFormatterCache<TealValueType>.Formatter.Serialize(ref writer, value.Type);
            switch (value.Type)
            {
                case TealValueType.Bytes:
                    writer.WriteString(new FixedString32Bytes("tb"));
                    AlgoApiFormatterCache<TealBytes>.Formatter.Serialize(ref writer, value.Bytes);
                    break;
                case TealValueType.Uint:
                    writer.WriteString(new FixedString32Bytes("ui"));
                    AlgoApiFormatterCache<ulong>.Formatter.Serialize(ref writer, value.Uint);
                    break;
                default:
                    throw new NotSupportedException($"Cannot serialize {nameof(TealValue)} with type: {value.Type}");
            }
        }
    }
}
