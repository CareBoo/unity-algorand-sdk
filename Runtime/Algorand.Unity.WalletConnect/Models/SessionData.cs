using System;
using Algorand.Unity.LowLevel;
using Unity.Collections;
using UnityEngine;

namespace Algorand.Unity.WalletConnect
{
    [Serializable]
    public struct SessionData : IWalletConnectSessionData
    {
        [SerializeField] private string clientId;

        [SerializeField] private string bridgeUrl;

        [SerializeField] private Hex key;

        [SerializeField] private string peerId;

        [SerializeField] private ulong handshakeId;

        [SerializeField] private int networkId;

        [SerializeField] private Address[] accounts;

        [SerializeField] private int chainId;

        [SerializeField] private ClientMeta dappMeta;

        [SerializeField] private ClientMeta walletMeta;

        public string ClientId
        {
            get => clientId;
            set => clientId = value;
        }

        public string BridgeUrl
        {
            get => bridgeUrl;
            set => bridgeUrl = value;
        }

        public Hex Key
        {
            get => key;
            set => key = value;
        }

        public string PeerId
        {
            get => peerId;
            set => peerId = value;
        }

        public ulong HandshakeId
        {
            get => handshakeId;
            set => handshakeId = value;
        }

        public int NetworkId
        {
            get => networkId;
            set => networkId = value;
        }

        public Address[] Accounts
        {
            get => accounts;
            set => accounts = value;
        }

        public int ChainId
        {
            get => chainId;
            set => chainId = value;
        }

        public ClientMeta DappMeta
        {
            get => dappMeta;
            set => dappMeta = value;
        }

        public ClientMeta WalletMeta
        {
            get => walletMeta;
            set => walletMeta = value;
        }

        public SessionData Reinitialize()
        {
            return InitSession(dappMeta, bridgeUrl);
        }

        public static SessionData InitSession(ClientMeta dappMeta, string bridgeUrl)
        {
            return new SessionData
            {
                ClientId = Guid.NewGuid().ToString(),
                BridgeUrl = string.IsNullOrEmpty(bridgeUrl) ? DefaultBridge.GetRandomBridgeUrl() : bridgeUrl,
                Key = GenKey(),
                DappMeta = dappMeta
            };
        }

        public static Hex GenKey()
        {
            using var secret = new NativeByteArray(32, Allocator.Persistent);
            Crypto.Random.Randomize(secret);
            return secret.ToArray();
        }
    }
}
