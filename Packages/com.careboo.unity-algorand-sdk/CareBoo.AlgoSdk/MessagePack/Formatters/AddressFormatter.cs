using MessagePack;
using MessagePack.Formatters;
using Unity.Collections;

namespace AlgoSdk.MsgPack.Formatters
{
    public sealed class AddressFormatter : IMessagePackFormatter<AlgoSdk.Address>
    {
        public Address Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            var s = options.Resolver.GetFormatter<FixedString128Bytes>().Deserialize(ref reader, options);
            return Address.FromString(in s);
        }

        public void Serialize(ref MessagePackWriter writer, Address value, MessagePackSerializerOptions options)
        {
            var s = value.ToFixedString();
            options.Resolver.GetFormatter<FixedString128Bytes>().Serialize(ref writer, s, options);
        }
    }
}
