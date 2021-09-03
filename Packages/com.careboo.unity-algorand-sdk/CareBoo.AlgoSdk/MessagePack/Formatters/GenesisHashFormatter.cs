using AlgoSdk.Crypto;
using MessagePack;
using MessagePack.Formatters;
using Unity.Collections;

namespace AlgoSdk
{
    public sealed class GenesisHashFormatter : IMessagePackFormatter<GenesisHash>
    {
        public GenesisHash Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            if (options.IsJson)
            {
                var s = options.Resolver.GetFormatter<FixedString64Bytes>().Deserialize(ref reader, options);
                var result = new GenesisHash();
                result.CopyFromBase64(s);
                return result;
            }
            else
            {
                return options.Resolver.GetFormatter<Sha512_256_Hash>().Deserialize(ref reader, options);
            }
        }

        public void Serialize(ref MessagePackWriter writer, GenesisHash value, MessagePackSerializerOptions options)
        {
            if (options.IsJson)
            {
                var fs = new FixedString64Bytes();
                value.CopyToBase64(ref fs);
                options.Resolver.GetFormatter<FixedString64Bytes>().Serialize(ref writer, fs, options);
            }
            else
            {
                options.Resolver.GetFormatter<Sha512_256_Hash>().Serialize(ref writer, value, options);
            }
        }
    }
}
