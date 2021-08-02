using System.Collections;
using System.Collections.Generic;
using AlgoSdk.LowLevel;
using MessagePack;
using MessagePack.Formatters;

namespace AlgoSdk.MsgPack.Formatters
{
    public sealed class ByteArrayFormatter<TByteArray> : IMessagePackFormatter<TByteArray>
        where TByteArray : unmanaged, IByteArray
    {
        public TByteArray Deserialize(ref MessagePackReader reader, MessagePackSerializerOptions options)
        {
            return reader.ReadBytes().Value.ToByteArray<TByteArray>();
        }

        public void Serialize(ref MessagePackWriter writer, TByteArray value, MessagePackSerializerOptions options)
        {
            writer.Write(value.AsReadOnlySpan());
        }
    }
}
