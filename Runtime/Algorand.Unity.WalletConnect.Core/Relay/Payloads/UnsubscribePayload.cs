using System;
using UnityEngine;

namespace Algorand.Unity.WalletConnect.Core
{
    /// <summary>
    /// Used when a client unsubscribes a given topic.
    /// <br/>
    /// https://docs.walletconnect.com/2.0/specs/servers/relay/relay-server-rpc#unsubscribe-payload
    /// </summary>
    [AlgoApiObject]
    [Serializable]
    public partial struct UnsubscribeRequestPayload : IEquatable<UnsubscribeRequestPayload>
    {
        [SerializeField]
        private Topic topic;

        [SerializeField]
        private string id;

        [AlgoApiField("topic")]
        public Topic Topic
        {
            get => topic;
            set => topic = value;
        }

        [AlgoApiField("id")]
        public string Id
        {
            get => id;
            set => id = value;
        }

        public bool Equals(UnsubscribeRequestPayload other)
        {
            return Topic.Equals(other.Topic)
                   && StringComparer.Equals(Id, other.Id)
                ;
        }
    }

    /// <summary>
    /// Used when a client unsubscribes a given topic.
    /// <br/>
    /// https://docs.walletconnect.com/2.0/specs/servers/relay/relay-server-rpc#unsubscribe-payload
    /// </summary>
    [Serializable]
    [AlgoApiFormatter(typeof(WrappedValueFormatter<UnsubscribeResponsePayload, bool>))]
    public partial struct UnsubscribeResponsePayload
        : IEquatable<UnsubscribeResponsePayload>
        , IWrappedValue<bool>
    {
        [SerializeField]
        private bool value;

        public bool WrappedValue { get => value; set => this.value = value; }

        public bool Equals(UnsubscribeResponsePayload other)
        {
            return value == other.value;
        }

        public static implicit operator bool(UnsubscribeResponsePayload payload)
        {
            return payload.value;
        }

        public static implicit operator UnsubscribeResponsePayload(bool value)
        {
            return new UnsubscribeResponsePayload { value = value };
        }
    }
}
