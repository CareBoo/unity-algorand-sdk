using Algorand.Unity.Json;
using Algorand.Unity.MessagePack;
using Unity.Collections;

namespace Algorand.Unity.Formatters
{
    public class TransactionIdFormatter : IAlgoApiFormatter<TransactionId>
    {
        public TransactionId Deserialize(ref JsonReader reader)
        {
            var fs = new FixedString64Bytes();
            reader.ReadString(ref fs).ThrowIfError(reader);
            return TransactionId.FromString(fs);
        }

        public TransactionId Deserialize(ref MessagePackReader reader)
        {
            return reader.ReadBytes<TransactionId>();
        }

        public void Serialize(ref JsonWriter writer, TransactionId value)
        {
            writer.WriteString(value.ToFixedString());
        }

        public unsafe void Serialize(ref MessagePackWriter writer, TransactionId value)
        {
            writer.WriteBytes(value.GetUnsafePtr(), value.Length);
        }
    }
}
