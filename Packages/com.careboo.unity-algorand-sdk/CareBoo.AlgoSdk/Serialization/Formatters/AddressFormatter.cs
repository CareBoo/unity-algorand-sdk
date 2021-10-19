using AlgoSdk.Json;
using AlgoSdk.MessagePack;
using Unity.Collections;

namespace AlgoSdk.Formatters
{
    public class AddressFormatter : IAlgoApiFormatter<Address>
    {
        public Address Deserialize(ref JsonReader reader)
        {
            var fs = new FixedString128Bytes();
            reader.ReadString(ref fs).ThrowIfError(reader.Char, reader.Position);
            return Address.FromString(fs);
        }

        public unsafe Address Deserialize(ref MessagePackReader reader)
        {
            return reader.ReadBytes<Address>();
        }

        public void Serialize(ref JsonWriter writer, Address value)
        {
            writer.WriteString(value.ToFixedString());
        }

        public unsafe void Serialize(ref MessagePackWriter writer, Address value)
        {
            writer.WriteBytes(value.GetUnsafePtr(), value.Length);
        }
    }
}
