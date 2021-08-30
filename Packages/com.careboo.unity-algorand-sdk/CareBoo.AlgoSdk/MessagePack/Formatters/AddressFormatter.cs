using AlgoSdk.Crypto;
using MessagePack;
using MessagePack.Formatters;
using Unity.Collections;

namespace AlgoSdk.MsgPack.Formatters
{
    public sealed class AddressFormatter : IMessagePackFormatter<AlgoSdk.Address>
    {
        public Address Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            if (options.IsJson)
            {
                var s = options.Resolver.GetFormatter<FixedString128Bytes>().Deserialize(ref reader, options);
                return Address.FromString(in s);
            }
            else
            {
                var pk = ByteArrayFormatter<Ed25519.PublicKey>.Instance.Deserialize(ref reader, options);
                return ((Address)pk).GenerateCheckSum();
            }
        }

        public void Serialize(ref MessagePackWriter writer, Address value, MessagePackSerializerOptions options)
        {
            if (options.IsJson)
            {
                var s = value.ToFixedString();
                options.Resolver.GetFormatter<FixedString128Bytes>().Serialize(ref writer, s, options);
            }
            else
            {
                ByteArrayFormatter<Ed25519.PublicKey>.Instance.Serialize(ref writer, value, options);
            }
        }
    }
}
