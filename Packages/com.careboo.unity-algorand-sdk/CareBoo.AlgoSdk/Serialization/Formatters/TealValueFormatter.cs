using System;
using AlgoSdk.Json;
using AlgoSdk.MessagePack;
using Unity.Collections;

namespace AlgoSdk.MsgPack.Formatters
{
    public sealed class TealValueFormatter : IAlgoApiFormatter<TealValue>
    {
        public TealValue Deserialize(ref JsonReader reader)
        {
            TealValue result = default;
            if (reader.Peek() != JsonToken.ObjectBegin)
                JsonReadError.IncorrectType.ThrowIfError();
            var token = reader.Read();
            do
            {
                var key = new FixedString32Bytes();
                reader.ReadKey(ref key).ThrowIfError();
                if (key == "type")
                    result.Type = AlgoApiFormatterCache<TealValueType>.Formatter.Deserialize(ref reader);
                else if (key == "bytes")
                    result.Bytes = AlgoApiFormatterCache<TealBytes>.Formatter.Deserialize(ref reader);
                else if (key == "uint")
                    result.Uint = AlgoApiFormatterCache<ulong>.Formatter.Deserialize(ref reader);
                else
                    throw new ArgumentOutOfRangeException($"Found unexpected key in message: {key}");
            }
            while (token == JsonToken.Next);
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
                if (key == "type")
                    result.Type = AlgoApiFormatterCache<TealValueType>.Formatter.Deserialize(ref reader);
                else if (key == "bytes")
                    result.Bytes = AlgoApiFormatterCache<TealBytes>.Formatter.Deserialize(ref reader);
                else if (key == "uint")
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
            writer.WriteString(new FixedString32Bytes("type"));
            AlgoApiFormatterCache<TealValueType>.Formatter.Serialize(ref writer, value.Type);
            switch (value.Type)
            {
                case TealValueType.Bytes:
                    writer.WriteString(new FixedString32Bytes("bytes"));
                    AlgoApiFormatterCache<TealBytes>.Formatter.Serialize(ref writer, value.Bytes);
                    break;
                case TealValueType.Uint:
                    writer.WriteString(new FixedString32Bytes("uint"));
                    AlgoApiFormatterCache<ulong>.Formatter.Serialize(ref writer, value.Uint);
                    break;
                default:
                    throw new NotSupportedException($"Cannot serialize {nameof(TealValue)} with type: {value.Type}");
            }
        }
    }
}
