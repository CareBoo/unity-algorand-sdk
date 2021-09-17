
using System;
using AlgoSdk.Json;
using AlgoSdk.MessagePack;
using Unity.Collections;

namespace AlgoSdk.Formatters
{
    public sealed class TransactionIdFormatter : IAlgoApiFormatter<TransactionId>
    {
        private static readonly FixedString32Bytes Key = "txId";

        public TransactionId Deserialize(ref JsonReader reader)
        {
            if (reader.Peek() != JsonToken.ObjectBegin)
                JsonReadError.IncorrectType.ThrowIfError();

            reader.Read();
            var key = new FixedString32Bytes();
            reader.ReadString(ref key).ThrowIfError();
            if (key != Key)
                JsonReadError.IncorrectFormat.ThrowIfError();
            var token = reader.Read();
            if (token != JsonToken.KeyValueSeparator)
                JsonReadError.IncorrectFormat.ThrowIfError();

            var value = new FixedString64Bytes();
            reader.ReadString(ref value).ThrowIfError();
            if (reader.Read() != JsonToken.ObjectEnd)
                JsonReadError.IncorrectFormat.ThrowIfError();
            return value;
        }

        public TransactionId Deserialize(ref MessagePackReader reader)
        {
            var length = reader.ReadMapHeader();
            if (length != 1)
                throw new ArgumentOutOfRangeException($"Only expecting a map of length 1 for transaction ids...");
            var key = new FixedString32Bytes();
            reader.ReadString(ref key);
            if (key != Key)
                throw new ArgumentOutOfRangeException($"Expecting a key of '{Key}', but it was '{key}'");
            var value = new FixedString64Bytes();
            reader.ReadString(ref value);
            return value;
        }

        public void Serialize(ref JsonWriter writer, TransactionId value)
        {
            writer.BeginObject();
            writer.WriteObjectKey(in Key);
            writer.WriteString(in value.Text);
            writer.EndObject();
        }

        public void Serialize(ref MessagePackWriter writer, TransactionId value)
        {
            writer.WriteMapHeader(1);
            writer.WriteString(in Key);
            writer.WriteString(in value.Text);
        }
    }
}
