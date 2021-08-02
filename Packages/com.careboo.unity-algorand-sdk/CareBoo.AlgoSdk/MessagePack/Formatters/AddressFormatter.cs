using MessagePack;
using Unity.Collections;

namespace AlgoSdk.MsgPack.Formatters
{
    public sealed class AddressFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::AlgoSdk.Address>
    {
        public Address Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            var s = options.Resolver.GetFormatter<FixedString128>().Deserialize(ref reader, options);
            return Address.FromString(in s);
        }

        public void Serialize(ref MessagePackWriter writer, Address value, MessagePackSerializerOptions options)
        {
            var s = value.ToFixedString();
            options.Resolver.GetFormatter<FixedString128>().Serialize(ref writer, s, options);
        }
    }
}
