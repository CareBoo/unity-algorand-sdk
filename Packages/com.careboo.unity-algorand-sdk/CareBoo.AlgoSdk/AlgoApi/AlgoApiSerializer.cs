using System;
using AlgoSdk.Json;
using AlgoSdk.MessagePack;
using Unity.Collections;

namespace AlgoSdk
{
    public static class AlgoApiSerializer
    {
        public static T Deserialize<T>(NativeArray<byte>.ReadOnly bytes, AlgoApiFormat contentType)
        {
            return contentType switch
            {
                AlgoApiFormat.Json => DeserializeJson<T>(bytes),
                AlgoApiFormat.MessagePack => DeserializeMessagePack<T>(bytes),
                _ => throw new NotSupportedException($"ContentType {contentType} is not supported")
            };
        }

        public static T DeserializeJson<T>(NativeArray<byte>.ReadOnly bytes)
        {
            throw new NotImplementedException();
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

        public static void SerializeMessagePack<T>(T obj, NativeList<byte> bytes)
        {
            var writer = new MessagePackWriter(bytes);
            AlgoApiFormatterCache<T>.Formatter.Serialize(ref writer, obj);
        }

        public static void SerializeJson<T>(T obj, NativeText text)
        {
            var writer = new JsonWriter(text);
            AlgoApiFormatterCache<T>.Formatter.Serialize(ref writer, obj);
        }
    }
}
