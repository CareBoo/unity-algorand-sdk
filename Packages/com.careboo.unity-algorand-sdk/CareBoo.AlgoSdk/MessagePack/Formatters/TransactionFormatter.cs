using System;
using MessagePack;
using MessagePack.Formatters;

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
            value.CopyTo(ref rawTransaction);
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
            result.CopyFrom(in rawTransaction);
            return result;
        }

        public void Serialize(ref MessagePackWriter writer, T value, MessagePackSerializerOptions options)
        {
            var rawTransaction = new RawTransaction();
            value.CopyTo(ref rawTransaction);
            options.Resolver.GetFormatter<RawTransaction>().Serialize(ref writer, rawTransaction, options);
        }
    }
}
