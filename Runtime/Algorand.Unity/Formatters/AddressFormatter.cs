using Algorand.Unity.Json;
using Algorand.Unity.MessagePack;
using Unity.Collections;

namespace Algorand.Unity.Formatters
{
    public class AddressFormatter : IAlgoApiFormatter<Address>
    {
        public Address Deserialize(ref JsonReader reader)
        {
            var fs = new FixedString128Bytes();
            reader.ReadString(ref fs).ThrowIfError(reader);
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
