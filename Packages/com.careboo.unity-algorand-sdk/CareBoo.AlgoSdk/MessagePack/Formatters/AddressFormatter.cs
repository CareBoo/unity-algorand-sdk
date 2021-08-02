using AlgoSdk.LowLevel;
using MessagePack;

namespace AlgoSdk.MsgPack.Formatters
{
    public sealed class AddressFormatter : global::MessagePack.Formatters.IMessagePackFormatter<global::AlgoSdk.Address>
    {
        public Address Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            return reader.ReadBytes().Value.ToByteArray<Address>();
        }

        public void Serialize(ref MessagePackWriter writer, Address value, MessagePackSerializerOptions options)
        {
            writer.Write(value.AsReadOnlySpan());
        }
    }
}
