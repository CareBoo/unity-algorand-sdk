using System;
using MessagePack;
using MessagePack.Formatters;

namespace AlgoSdk.MsgPack.Formatters
{
    public sealed class SignedTransaction : IMessagePackFormatter<ISignedTransaction>
    {
        public ISignedTransaction Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public void Serialize(ref MessagePackWriter writer, ISignedTransaction value, MessagePackSerializerOptions options)
        {
            RawSignedTransaction raw = default;
            value.CopyTo(ref raw);
            options.Resolver.GetFormatter<RawSignedTransaction>().Serialize(ref writer, raw, options);
        }
    }

    public sealed class SignedTransactionFormatter<T>
        : IMessagePackFormatter<T>
        where T : struct, ISignedTransaction
    {
        public void Serialize(ref MessagePackWriter writer, T value, MessagePackSerializerOptions options)
        {
            RawSignedTransaction raw = default;
            value.CopyTo(ref raw);
            options.Resolver.GetFormatter<RawSignedTransaction>().Serialize(ref writer, raw, options);
        }

        public T Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            var resolver = options.Resolver;
            var raw = resolver.GetFormatter<RawSignedTransaction>().Deserialize(ref reader, options);
            T result = default;
            result.CopyFrom(in raw);
            return result;
        }
    }
}
