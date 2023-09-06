using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Algorand.Unity.WalletConnect.Core
{
    /// <summary>
    /// Used when a client subscribes a given topic.
    /// <br/>
    /// https://docs.walletconnect.com/2.0/specs/servers/relay/relay-server-rpc#subscribe-payload
    /// </summary>
    [AlgoApiObject]
    [Serializable]
    public partial struct SubscribeRequestPayload : IEquatable<SubscribeRequestPayload>
    {
        [SerializeField]
        private Topic topic;

        [AlgoApiField("topic")]
        public Topic Topic
        {
            get => topic;
            set => topic = value;
        }

        public bool Equals(SubscribeRequestPayload other)
        {
            return Topic.Equals(other.Topic);
        }
    }

    /// <summary>
    /// Used when a client subscribes a given topic.
    /// <br/>
    /// https://docs.walletconnect.com/2.0/specs/servers/relay/relay-server-rpc#subscribe-payload
    /// </summary>
    [Serializable]
    [AlgoApiFormatter(typeof(WrappedValueFormatter<SubscribeResponsePayload, string>))]
    public partial struct SubscribeResponsePayload
        : IEquatable<SubscribeResponsePayload>
        , IWrappedValue<string>
    {
        [SerializeField]
        private string value;

        public string WrappedValue { get => value; set => this.value = value; }

        public string SubscriptionId { get => value; set => this.value = value; }

        public bool Equals(SubscribeResponsePayload other)
        {
            return StringComparer.Equals(value, other.value);
        }

        public static implicit operator string(SubscribeResponsePayload payload)
        {
            return payload.value;
        }

        public static implicit operator SubscribeResponsePayload(string value)
        {
            return new SubscribeResponsePayload { value = value };
        }
    }
}
