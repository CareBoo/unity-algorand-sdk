using System;
using MessagePack;
using MessagePack.Formatters;
using Unity.Collections;

namespace AlgoSdk.MsgPack.Formatters
{
    public sealed class TransactionFormatter
        : IMessagePackFormatter<ITransaction>
    {
        public ITransaction Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            var resolver = options.Resolver;
            var rawTransaction = resolver.GetFormatter<RawTransaction>().Deserialize(ref reader, options);
            switch (rawTransaction.TransactionType)
            {
                default:
                    throw new NotSupportedException($"Transaction type {rawTransaction.TransactionType} not supported!");
            }
        }

        public void Serialize(ref MessagePackWriter writer, ITransaction value, MessagePackSerializerOptions options)
        {
            var rawTransaction = new RawTransaction();
            value.CopyToRawTransaction(ref rawTransaction);
            options.Resolver.GetFormatter<RawTransaction>().Serialize(ref writer, rawTransaction, options);
        }
    }

    public sealed class TransactionFormatter<T>
        : IMessagePackFormatter<T>
        where T : ITransaction
    {
        public T Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            var resolver = options.Resolver;
            var rawTransaction = resolver.GetFormatter<RawTransaction>().Deserialize(ref reader, options);
            T result = default;
            result.CopyFromRawTransaction(in rawTransaction);
            return result;
        }

        public void Serialize(ref MessagePackWriter writer, T value, MessagePackSerializerOptions options)
        {
            var rawTransaction = new RawTransaction();
            value.CopyToRawTransaction(ref rawTransaction);
            options.Resolver.GetFormatter<RawTransaction>().Serialize(ref writer, rawTransaction, options);
        }
    }

    public sealed class RawTransactionFormatter
        : IMessagePackFormatter<RawTransaction>
    {

        public RawTransaction Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            var result = new RawTransaction();
            var length = reader.ReadMapHeader();
            for (var i = 0; i < length; i++)
            {
                var key = options.Resolver.GetFormatter<FixedString32>().Deserialize(ref reader, options);
                result.MessagePackFields[key].Deserialize(ref result, ref reader, options);
            }
            return result;
        }

        public void Serialize(ref MessagePackWriter writer, RawTransaction value, MessagePackSerializerOptions options)
        {
            using var fieldsToSerialize = value.GetFieldsToSerialize(Allocator.Temp);
            writer.WriteMapHeader(fieldsToSerialize.Length);
            for (var i = 0; i < fieldsToSerialize.Length; i++)
            {
                var key = fieldsToSerialize[i];
                options.Resolver.GetFormatter<FixedString32>().Serialize(ref writer, key, options);
                value.MessagePackFields[key].Serialize(ref value, ref writer, options);
            }
        }
    }
}
