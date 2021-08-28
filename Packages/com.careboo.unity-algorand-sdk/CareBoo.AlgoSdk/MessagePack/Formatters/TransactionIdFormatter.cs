
using MessagePack;
using MessagePack.Formatters;
using Unity.Collections;

namespace AlgoSdk.MsgPack.Formatters
{
    public sealed class TransactionIdFormatter : IMessagePackFormatter<TransactionId>
    {
        public TransactionId Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            var contentFormatter = options.Resolver.GetFormatter<FixedString128Bytes>();
            return contentFormatter.Deserialize(ref reader, options);
        }

        public void Serialize(ref MessagePackWriter writer, TransactionId value, MessagePackSerializerOptions options)
        {
            var contentFormatter = options.Resolver.GetFormatter<FixedString128Bytes>();
            contentFormatter.Serialize(ref writer, value, options);
        }
    }
}
