using System;
using UnityEngine;

namespace Algorand.Unity.WalletConnect.Core
{
    [AlgoApiObject]
    [Serializable]
    public partial struct ProtocolOptions : IEquatable<ProtocolOptions>
    {
        [SerializeField]
        private string protocol;

        [SerializeField]
        private string data;

        [AlgoApiField("protocol")]
        public string Protocol
        {
            get => protocol;
            set => protocol = value;
        }

        [AlgoApiField("data")]
        public string Data
        {
            get => data;
            set => data = value;
        }

        public bool Equals(ProtocolOptions other)
        {
            return StringComparer.Equals(Protocol, other.Protocol)
                && StringComparer.Equals(Data, other.Data);
        }
    }
}
