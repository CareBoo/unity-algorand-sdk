using AlgoSdk.Json;
using AlgoSdk.MessagePack;
using AlgoSdk.LowLevel;
using Unity.Collections;

namespace AlgoSdk.Formatters
{
    public class ByteArrayFormatter<TByteArray>
        : IAlgoApiFormatter<TByteArray>
        where TByteArray : unmanaged, IByteArray
    {
        public static ByteArrayFormatter<TByteArray> Instance = new ByteArrayFormatter<TByteArray>();

        public TByteArray Deserialize(ref JsonReader reader)
        {
            var text = new NativeText(Allocator.Temp);
            try
            {
                reader.ReadString(ref text)
                    .ThrowIfError();
                return BytesFromString(in text);
            }
            finally
            {
                text.Dispose();
            }
        }

        public TByteArray Deserialize(ref MessagePackReader reader)
        {
            var bytes = reader.ReadBytes();
            TByteArray result = default;
            for (var i = 0; i < bytes.Length; i++)
                result[i] = bytes[i];
            return result;
        }

        public void Serialize(ref JsonWriter writer, TByteArray value)
        {
            var text = new NativeText(Allocator.Temp);
            try
            {
                BytesToString(value, ref text);
                writer.WriteString(in text);
            }
            finally
            {
                text.Dispose();
            }
        }

        public void Serialize(ref MessagePackWriter writer, TByteArray value)
        {
            unsafe
            {
                writer.WriteBytes((void*)value.Buffer, value.Length);
            }
        }

        protected virtual TByteArray BytesFromString<T>(in T fs)
            where T : struct, INativeList<byte>, IUTF8Bytes
        {
            TByteArray result = default;
            result.CopyFromBase64(in fs);
            return result;
        }

        protected virtual void BytesToString<T>(TByteArray value, ref T fs)
            where T : struct, INativeList<byte>, IUTF8Bytes
        {
            value.CopyToBase64(ref fs);
        }
    }
}
