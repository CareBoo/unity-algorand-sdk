using System;
using UnityEngine;

namespace Algorand.Unity.WalletConnect.Core
{
    /// <summary>
    /// Used when a client wants to fetch all undelivered messages matching a single topic before subscribing.
    /// <br/>
    /// https://docs.walletconnect.com/2.0/specs/servers/relay/relay-server-rpc#fetch-messsages-payload
    /// </summary>
    /// <remarks>
    /// Response will include a flag hasMore. If true, the consumer should fetch again to get the rest of the messages. If false, then all messages have been delivered.
    /// </remarks>
    [AlgoApiObject]
    [Serializable]
    public partial struct FetchMessagesRequestPayload : IEquatable<FetchMessagesRequestPayload>
    {
        [SerializeField]
        private Topic topic;

        [AlgoApiField("topic")]
        public Topic Topic
        {
            get => topic;
            set => topic = value;
        }

        public bool Equals(FetchMessagesRequestPayload other)
        {
            return Topic.Equals(other.Topic);
        }
    }

    /// <summary>
    /// Used when a client wants to fetch all undelivered messages matching a single topic before subscribing.
    /// <br/>
    /// https://docs.walletconnect.com/2.0/specs/servers/relay/relay-server-rpc#fetch-messsages-payload
    /// </summary>
    /// <remarks>
    /// Response will include a flag hasMore. If true, the consumer should fetch again to get the rest of the messages. If false, then all messages have been delivered.
    /// </remarks>
    [AlgoApiObject]
    [Serializable]
    public partial struct FetchMessagesResponsePayload : IEquatable<FetchMessagesResponsePayload>
    {
        [SerializeField]
        private ReceivedMessage[] messages;

        [SerializeField]
        bool hasMore;

        [AlgoApiField("messages")]
        public ReceivedMessage[] Messages
        {
            get => messages;
            set => messages = value;
        }

        [AlgoApiField("hasMore")]
        public bool HasMore
        {
            get => hasMore;
            set => hasMore = value;
        }

        public bool Equals(FetchMessagesResponsePayload other)
        {
            return ArrayComparer.Equals(Messages, other.Messages)
                   && HasMore.Equals(other.HasMore);
        }
    }
}
