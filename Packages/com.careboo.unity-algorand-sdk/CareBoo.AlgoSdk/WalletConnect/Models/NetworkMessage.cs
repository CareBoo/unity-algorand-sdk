using System;
using UnityEngine;

namespace AlgoSdk.WalletConnect
{
    [Serializable]
    public struct NetworkMessage
    {
        [SerializeField]
        private string topic;

        [SerializeField]
        private string type;

        [SerializeField]
        private string payload;

        public string Topic
        {
            get => topic;
            set => topic = value;
        }

        public string Type
        {
            get => type;
            set => type = value;
        }

        public string Payload
        {
            get => payload;
            set => payload = value;
        }
    }
}
