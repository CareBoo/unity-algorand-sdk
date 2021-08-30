
using System;
using MessagePack;
using MessagePack.Formatters;
using Unity.Collections;

namespace AlgoSdk.MsgPack.Formatters
{
    public sealed class TransactionIdFormatter : IMessagePackFormatter<TransactionId>
    {
        private static readonly FixedString32Bytes Key = "txId";

        public TransactionId Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            var length = reader.ReadMapHeader();
            if (length != 1)
                throw new ArgumentOutOfRangeException($"Only expecting a map of length 1 for transaction ids...");
            var key = options.Resolver.GetFormatter<FixedString32Bytes>().Deserialize(ref reader, options);
            if (key != Key)
                throw new ArgumentOutOfRangeException($"Expecting a key of '{Key}', but it was '{key}'");
            return options.Resolver.GetFormatter<FixedString64Bytes>().Deserialize(ref reader, options);
        }

        public void Serialize(ref MessagePackWriter writer, TransactionId value, MessagePackSerializerOptions options)
        {
            writer.WriteMapHeader(1);
            options.Resolver.GetFormatter<FixedString32Bytes>().Serialize(ref writer, Key, options);
            options.Resolver.GetFormatter<FixedString64Bytes>().Serialize(ref writer, value, options);
        }
    }
}
