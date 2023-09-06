using System;
using UnityEngine;

namespace Algorand.Unity.WalletConnect.Core
{
    [Serializable]
    public struct ClientOptions : ISerializationCallbackReceiver
    {
        [SerializeField]
        private string projectId;

        [SerializeField]
        private string name;

        [SerializeField]
        private string relayUrl;

        [SerializeField]
        private string baseContext;

        [SerializeField]
        [HideInInspector]
        private string connectionUrl;

        public ClientOptions(
            string projectId,
            string name,
            string relayUrl = default,
            string baseContext = default
        )
        {
            this.projectId = projectId;
            this.name = name;
            this.relayUrl = string.IsNullOrEmpty(relayUrl)
                ? "wss://relay.walletconnect.org"
                : relayUrl;
            this.baseContext = string.IsNullOrEmpty(baseContext)
                ? $"{name}-client"
                : baseContext;
            connectionUrl = $"{relayUrl}/?projectId={projectId}";
        }

        public string ProjectId => projectId;
        public string Name => name;
        public string RelayUrl => relayUrl;
        public string BaseContext => baseContext;
        public string ConnectionUrl => connectionUrl;

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            if (string.IsNullOrEmpty(relayUrl)) relayUrl = "wss://relay.walletconnect.org";
            if (string.IsNullOrEmpty(baseContext)) baseContext = $"{name}-client";
            connectionUrl = $"{relayUrl}/?projectId={projectId}";
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
        }
    }
}