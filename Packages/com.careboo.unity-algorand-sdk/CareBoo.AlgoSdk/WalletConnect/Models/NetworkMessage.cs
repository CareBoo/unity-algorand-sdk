using System;
using System.Text;
using UnityEngine;

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
        public AlgoApiObject Payload;

        public bool Equals(NetworkMessage other)
        {
            return StringComparer.Equals(Topic, other.Topic)
                && StringComparer.Equals(Type, other.Type)
                && Payload.Equals(other.Payload)
                ;
        }

        public byte[] ToByteArray() =>
            Encoding.UTF8.GetBytes(JsonUtility.ToJson(this));
    }
}
