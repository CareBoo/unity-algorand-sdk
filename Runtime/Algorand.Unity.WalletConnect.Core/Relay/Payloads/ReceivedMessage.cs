using System;
using UnityEngine;

namespace Algorand.Unity.WalletConnect.Core
{
    [AlgoApiObject]
    [Serializable]
    public partial struct ReceivedMessage : IEquatable<ReceivedMessage>
    {
        [SerializeField]
        private Topic topic;

        [SerializeField]
        private string message;

        [SerializeField]
        private ulong publishedAt;

        [SerializeField]
        private ulong tag;

        [AlgoApiField("topic")]
        public Topic Topic
        {
            get => topic;
            set => topic = value;
        }

        [AlgoApiField("message")]
        public string Message
        {
            get => message;
            set => message = value;
        }

        [AlgoApiField("publishedAt")]
        public ulong PublishedAt
        {
            get => publishedAt;
            set => publishedAt = value;
        }

        [AlgoApiField("tag")]
        public ulong Tag
        {
            get => tag;
            set => tag = value;
        }

        public bool Equals(ReceivedMessage other)
        {
            return Topic.Equals(other.Topic)
                   && StringComparer.Equals(Message, other.Message)
                   && PublishedAt.Equals(other.PublishedAt)
                   && Tag.Equals(other.Tag)
                ;
        }
    }
}
