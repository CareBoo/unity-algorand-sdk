using System;
using AlgoSdk.Json;
using AlgoSdk.MessagePack;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace AlgoSdk
{
    public static class AlgoApiSerializer
    {
        public static T Deserialize<T>(NativeArray<byte>.ReadOnly bytes, ContentType contentType)
        {
            return contentType switch
            {
                ContentType.Json => DeserializeJson<T>(bytes),
                ContentType.MessagePack => DeserializeMessagePack<T>(bytes),
                _ when bytes.Length == 0 => default,
                _ => throw new NotSupportedException($"Cannot decode {bytes.Length} bytes of content type {contentType}")
            };
        }

        public static T DeserializeJson<T>(NativeArray<byte>.ReadOnly bytes)
        {
            var text = new NativeText(bytes.Length, Allocator.Temp);
            try
            {
                unsafe
                {
                    UnsafeUtility.MemCpy(text.GetUnsafePtr(), bytes.GetUnsafeReadOnlyPtr(), bytes.Length);
                    text.Length = bytes.Length;
                }
                return DeserializeJson<T>(text);
            }
            finally
            {
                text.Dispose();
            }
        }

        public static T DeserializeJson<T>(NativeText text)
        {
            var reader = new JsonReader(text);
            return AlgoApiFormatterCache<T>.Formatter.Deserialize(ref reader);
        }

        public static T DeserializeMessagePack<T>(NativeArray<byte>.ReadOnly bytes)
        {
            var reader = new MessagePackReader(bytes);
            return AlgoApiFormatterCache<T>.Formatter.Deserialize(ref reader);
        }

        public static NativeList<byte> SerializeMessagePack<T>(T obj, Allocator allocator)
        {
            var writer = new MessagePackWriter(allocator);
            var formatter = AlgoApiFormatterCache<T>.Formatter;
            formatter.Serialize(ref writer, obj);
            return writer.Data;
        }

        public static NativeText SerializeJson<T>(T obj, Allocator allocator)
        {
            var writer = new JsonWriter(allocator);
            var formatter = AlgoApiFormatterCache<T>.Formatter;
            formatter.Serialize(ref writer, obj);
            return writer.Text;
        }
    }
}
