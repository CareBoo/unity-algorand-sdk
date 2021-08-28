using System;
using System.Text;
using AlgoSdk.MsgPack;
using MessagePack;

namespace AlgoSdk
{
    public static class AlgoApiSerializer
    {
        public static T Deserialize<T>(byte[] rawBytes, AlgoApiFormat contentType)
        {
            return contentType switch
            {
                AlgoApiFormat.Json => DeserializeJson<T>(rawBytes),
                AlgoApiFormat.MessagePack => DeserializeMessagePack<T>(rawBytes),
                _ => throw new NotSupportedException($"ContentType {contentType} is not supported")
            };
        }

        public static T DeserializeJson<T>(byte[] rawBytes)
        {
            var data = MessagePackSerializer.ConvertFromJson(
                Encoding.UTF8.GetString(rawBytes, 0, rawBytes.Length),
                MessagePackConfig.SerializerOptions);
            return MessagePackSerializer.Deserialize<T>(data, MessagePackConfig.SerializerOptions);
        }

        public static T DeserializeMessagePack<T>(byte[] rawBytes)
        {
            return MessagePackSerializer.Deserialize<T>(rawBytes, MessagePackConfig.SerializerOptions);
        }

        public static byte[] SerializeMessagePack<T>(T obj)
        {
            return MessagePackSerializer.Serialize(obj, MessagePackConfig.SerializerOptions);
        }

        public static string SerializeJson<T>(T obj)
        {
            var data = MessagePackSerializer.Serialize(obj, MessagePackConfig.SerializerOptions);
            return MessagePackSerializer.ConvertToJson(data, MessagePackConfig.SerializerOptions);
        }
    }
}
