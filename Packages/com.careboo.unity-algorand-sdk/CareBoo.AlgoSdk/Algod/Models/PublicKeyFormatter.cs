using AlgoSdk.Crypto;
using MessagePack;
using MessagePack.Formatters;
using Unity.Collections;

namespace AlgoSdk.MsgPack.Formatters
{
    public sealed class PublicKeyFormatter : IMessagePackFormatter<Ed25519.PublicKey>
    {
        public Ed25519.PublicKey Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            var s = options.Resolver.GetFormatter<FixedString64Bytes>().Deserialize(ref reader, options);
            var pk = new Ed25519.PublicKey();
            pk.CopyFromBase64(s);
            return pk;
        }

        public void Serialize(ref MessagePackWriter writer, Ed25519.PublicKey value, MessagePackSerializerOptions options)
        {
            var s = new FixedString64Bytes();
            value.CopyToBase64(ref s);
            options.Resolver.GetFormatter<FixedString64Bytes>().Serialize(ref writer, s, options);
        }
    }
}
