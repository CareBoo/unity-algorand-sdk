using System;
using AlgoSdk.LowLevel;
using AlgoSdk.MsgPack;
using MessagePack;
using Unity.Collections;

namespace AlgoSdk
{
    public static class AlgoApiSerializer
    {
        public static T Deserialize<T>(NativeArray<byte>.ReadOnly rawBytes, ContentType contentType)
        {
            return contentType switch
            {
                ContentType.Json => DeserializeJson<T>(rawBytes),
                ContentType.MessagePack => DeserializeMessagePack<T>(rawBytes),
                _ => throw new NotSupportedException($"ContentType {contentType} is not supported")
            };
        }

        public static T DeserializeJson<T>(NativeArray<byte>.ReadOnly rawBytes)
        {
            using var text = rawBytes.AsUtf8Text(Allocator.Temp);
            var data = MessagePackSerializer.ConvertFromJson(text.ToString(), AlgoSdkMessagePackConfig.SerializerOptions);
            return MessagePackSerializer.Deserialize<T>(data, AlgoSdkMessagePackConfig.SerializerOptions);
        }

        public static T DeserializeMessagePack<T>(NativeArray<byte>.ReadOnly rawBytes)
        {
            return MessagePackSerializer.Deserialize<T>(rawBytes.ToArray(), AlgoSdkMessagePackConfig.SerializerOptions);
        }
    }
}
