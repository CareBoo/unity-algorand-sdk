using Algorand.Unity.Json;
using Algorand.Unity.LowLevel;
using Algorand.Unity.MessagePack;
using Unity.Collections;

namespace Algorand.Unity.Formatters
{
    public class ByteArrayFormatter : IAlgoApiFormatter<byte[]>
    {
        public byte[] Deserialize(ref JsonReader reader)
        {
            if (reader.TryReadNull())
                return null;

            if (reader.Peek() == JsonToken.ArrayBegin)
                return ArrayFormatter<byte>.Instance.Deserialize(ref reader);

            var b64 = new NativeText(Allocator.Temp);
            try
            {
                reader.ReadString(ref b64).ThrowIfError(reader);
                return System.Convert.FromBase64String(b64.ToString());
            }
            finally
            {
                b64.Dispose();
            }
        }

        public byte[] Deserialize(ref MessagePackReader reader)
        {
            return reader.Peek().ToMessagePackType() == MessagePackType.Array
                ? ArrayFormatter<byte>.Instance.Deserialize(ref reader)
                : reader.ReadBytes().ToArray()
                ;
        }

        public void Serialize(ref JsonWriter writer, byte[] value)
        {
            var s = System.Convert.ToBase64String(value);
            using var t = new NativeText(s, Allocator.Temp);
            writer.WriteString(t);
        }

        public void Serialize(ref MessagePackWriter writer, byte[] value)
        {
            writer.WriteBytes(value);
        }
    }

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
                    .ThrowIfError(reader);
                TByteArray result = default;
                result.CopyFromBase64(text);
                return result;
            }
            finally
            {
                text.Dispose();
            }
        }

        public TByteArray Deserialize(ref MessagePackReader reader)
        {
            TByteArray result = default;
            if (reader.TryReadBytes(out var bytes))
            {
                for (var i = 0; i < bytes.Length; i++)
                    result[i] = bytes[i];
                return result;
            }
            var bytesRequiredForBase64 = Base64Encoding.BytesRequiredForBase64Encoding(result.Length);
            if (bytesRequiredForBase64 <= FixedString64Bytes.UTF8MaxLengthInBytes)
            {
                var fs = new FixedString64Bytes();
                reader.ReadString(ref fs);
                result.CopyFromBase64(fs);
            }
            else if (bytesRequiredForBase64 <= FixedString128Bytes.UTF8MaxLengthInBytes)
            {
                var fs = new FixedString128Bytes();
                reader.ReadString(ref fs);
                result.CopyFromBase64(fs);
            }
            else
            {
                var text = new NativeText(Allocator.Temp);
                try
                {
                    reader.ReadString(ref text);
                    result.CopyFromBase64(text);
                }
                finally
                {
                    text.Dispose();
                }
            }
            return result;
        }

        public void Serialize(ref JsonWriter writer, TByteArray value)
        {
            var text = new NativeText(Allocator.Temp);
            try
            {
                value.CopyToBase64(ref text);
                writer.WriteString(text);
            }
            finally
            {
                text.Dispose();
            }
        }

        public unsafe void Serialize(ref MessagePackWriter writer, TByteArray value)
        {
            writer.WriteBytes(value.GetUnsafePtr(), value.Length);
        }
    }
}
