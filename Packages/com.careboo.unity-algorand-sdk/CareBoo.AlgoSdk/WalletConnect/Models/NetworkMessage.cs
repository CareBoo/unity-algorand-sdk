using System;

namespace AlgoSdk.WalletConnect
{
    [AlgoApiObject]
    public struct NetworkMessage
        : IEquatable<NetworkMessage>
    {
        [AlgoApiField("topic", null)]
        public string Topic;

        [AlgoApiField("type", null)]
        public string Type;

        [AlgoApiField("payload", null)]
        public string Payload;

        public bool Equals(NetworkMessage other)
        {
            return StringComparer.Equals(Topic, other.Topic)
                && StringComparer.Equals(Type, other.Type)
                && Payload.Equals(other.Payload)
                ;
        }

        public static NetworkMessage SubscribeToTopic(string topic)
        {
            return new NetworkMessage
            {
                Topic = topic,
                Type = "sub",
                Payload = ""
            };
        }

        public static NetworkMessage PublishToTopic<T>(T payload, string topic)
        {
            var payloadJson = AlgoApiSerializer.SerializeJson(payload);
            return new NetworkMessage
            {
                Topic = topic,
                Type = "pub",
                Payload = payloadJson
            };
        }
    }
}
