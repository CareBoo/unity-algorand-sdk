using MessagePack;

namespace AlgoSdk.MsgPack
{
    public sealed class AddressFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::AlgoSdk.Address>
    {
        public Address Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            throw new System.NotImplementedException();
        }

        public void Serialize(ref MessagePackWriter writer, Address value, MessagePackSerializerOptions options)
        {
            writer.WriteBinHeader(value.Length);
            writer.WriteRaw(value.AsSpan());
        }
    }
}
