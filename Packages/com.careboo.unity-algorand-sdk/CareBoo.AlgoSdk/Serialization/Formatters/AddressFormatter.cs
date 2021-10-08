using AlgoSdk.Json;
using AlgoSdk.MessagePack;
using Unity.Collections;

namespace AlgoSdk.Formatters
{
    public class AddressFormatter : IAlgoApiFormatter<Address>
    {
        public Address Deserialize(ref JsonReader reader)
        {
            var text = new NativeText(Allocator.Temp);
            var fs = new FixedString128Bytes();
            reader.ReadString(ref fs).ThrowIfError(reader.Char, reader.Position);
            return Address.FromString(fs);
        }

        public Address Deserialize(ref MessagePackReader reader)
        {
            var bytes = reader.ReadBytes();
            Address result = default;
            for (var i = 0; i < bytes.Length; i++)
                result[i] = bytes[i];
            return result.GenerateCheckSum();
        }

        public void Serialize(ref JsonWriter writer, Address value)
        {
            writer.WriteString(value.ToFixedString());
        }

        public void Serialize(ref MessagePackWriter writer, Address value)
        {
            unsafe
            {
                writer.WriteBytes(value.GetUnsafePtr(), value.publicKey.Length);
            }
        }
    }
}
