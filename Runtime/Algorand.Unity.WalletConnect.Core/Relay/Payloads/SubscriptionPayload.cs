using System;
using UnityEngine;

namespace Algorand.Unity.WalletConnect.Core
{
    /// <summary>
    /// Used when a server sends a subscription message to a client.
    /// <br/>
    /// https://docs.walletconnect.com/2.0/specs/servers/relay/relay-server-rpc#subscription-payload
    /// </summary>
    [AlgoApiObject]
    [Serializable]
    public partial struct SubscriptionRequestPayload : IEquatable<SubscriptionRequestPayload>
    {
        [SerializeField]
        private string id;

        [SerializeField]
        private ReceivedMessage data;

        [AlgoApiField("id")]
        public string Id
        {
            get => id;
            set => id = value;
        }

        [AlgoApiField("data")]
        public ReceivedMessage Data
        {
            get => data;
            set => data = value;
        }

        public bool Equals(SubscriptionRequestPayload other)
        {
            return StringComparer.Equals(Id, other.Id)
                   && Data.Equals(other.Data)
                ;
        }
    }

    /// <summary>
    /// Used when a server sends a subscription message to a client.
    /// <br/>
    /// https://docs.walletconnect.com/2.0/specs/servers/relay/relay-server-rpc#subscription-payload
    /// </summary>
    [Serializable]
    [AlgoApiFormatter(typeof(WrappedValueFormatter<SubscriptionResponsePayload, bool>))]
    public partial struct SubscriptionResponsePayload
        : IEquatable<SubscriptionResponsePayload>
        , IWrappedValue<bool>
    {
        [SerializeField]
        private bool value;

        public bool WrappedValue { get => value; set => this.value = value; }

        public bool Equals(SubscriptionResponsePayload other)
        {
            return value == other.value;
        }

        public static implicit operator bool(SubscriptionResponsePayload payload)
        {
            return payload.value;
        }

        public static implicit operator SubscriptionResponsePayload(bool value)
        {
            return new SubscriptionResponsePayload { value = value };
        }
    }
}
