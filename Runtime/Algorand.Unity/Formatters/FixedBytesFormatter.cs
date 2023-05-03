using Algorand.Unity.Json;
using Algorand.Unity.LowLevel;
using Algorand.Unity.MessagePack;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace Algorand.Unity.Formatters
{
    public class FixedBytesFormatter<T> : IAlgoApiFormatter<T>
        where T : struct, INativeList<byte>
    {
        public T Deserialize(ref JsonReader reader)
        {
            var text = new NativeText(Allocator.Temp);
            try
            {
                reader.ReadString(ref text)
                    .ThrowIfError(reader);
                FixedBytesArray<T> bytes = default;
                bytes.CopyFromBase64(text);
                return bytes;
            }
            finally
            {
                text.Dispose();
            }
        }

        public T Deserialize(ref MessagePackReader reader)
        {
            var bytesSlice = reader.ReadBytes();
            T bytes = default;
            bytes.Length = bytesSlice.Length;
            for (var i = 0; i < bytesSlice.Length; i++)
                bytes[i] = bytesSlice[i];
            return bytes;
        }

        public void Serialize(ref JsonWriter writer, T value)
        {
            FixedBytesArray<T> bytes = value;
            var text = new NativeText(Allocator.Temp);
            try
            {
                bytes.CopyToBase64(ref text);
                writer.WriteString(text);
            }
            finally
            {
                text.Dispose();
            }
        }

        public void Serialize(ref MessagePackWriter writer, T value)
        {
            var arr = new NativeArray<byte>(value.Length, Allocator.Temp);
            try
            {
                for (var i = 0; i < value.Length; i++)
                    arr[i] = value[i];
                unsafe
                {
                    writer.WriteBytes(arr.GetUnsafePtr(), arr.Length);
                }
            }
            finally
            {
                arr.Dispose();
            }
        }
    }

    public struct FixedBytesArray<T>
        : IArray<byte>
        where T : struct, INativeList<byte>
    {
        private T bytes;

        public FixedBytesArray(T bytes)
        {
            this.bytes = bytes;
        }

        public byte this[int index]
        {
            get => bytes[index];
            set => bytes[index] = value;
        }

        public int Length
        {
            get => bytes.Length;
            set => bytes.Length = value;
        }

        public static implicit operator FixedBytesArray<T>(T bytes)
        {
            return new FixedBytesArray<T>(bytes);
        }

        public static implicit operator T(FixedBytesArray<T> fixedBytesArray)
        {
            return fixedBytesArray.bytes;
        }
    }
}
