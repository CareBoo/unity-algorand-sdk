using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Algorand.Unity.WalletConnect.Core
{
    /// <summary>
    /// Used when a client publishes a message to a server.
    /// <br/>
    /// https://docs.walletconnect.com/2.0/specs/servers/relay/relay-server-rpc#publish-payload
    /// </summary>
    [AlgoApiObject]
    [Serializable]
    public partial struct PublishRequestPayload : IEquatable<PublishRequestPayload>
    {
        [SerializeField]
        private Topic topic;

        [SerializeField]
        private string message;

        [SerializeField]
        private ulong timeToLive;

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

        [AlgoApiField("ttl")]
        public ulong TimeToLive
        {
            get => timeToLive;
            set => timeToLive = value;
        }

        [AlgoApiField("tag")]
        public ulong Tag
        {
            get => tag;
            set => tag = value;
        }

        public bool Equals(PublishRequestPayload other)
        {
            return Topic.Equals(other.Topic)
                   && StringComparer.Equals(Message, other.Message)
                   && TimeToLive.Equals(other.TimeToLive)
                   && Tag.Equals(other.Tag)
                ;
        }
    }

    /// <summary>
    /// Used when a client publishes a message to a server.
    /// <br/>
    /// https://docs.walletconnect.com/2.0/specs/servers/relay/relay-server-rpc#publish-payload
    /// </summary>
    [Serializable]
    [AlgoApiFormatter(typeof(WrappedValueFormatter<PublishResponsePayload, bool>))]
    public partial struct PublishResponsePayload
        : IEquatable<PublishResponsePayload>
        , IWrappedValue<bool>
    {
        [SerializeField]
        private bool value;

        public bool WrappedValue { get => value; set => this.value = value; }

        public bool Equals(PublishResponsePayload other)
        {
            return value == other.value;
        }

        public static implicit operator bool(PublishResponsePayload publishResponsePayload)
        {
            return publishResponsePayload.value;
        }

        public static implicit operator PublishResponsePayload(bool value)
        {
            return new PublishResponsePayload { value = value };
        }
    }
}
