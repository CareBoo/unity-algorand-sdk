using System;
using UnityEngine;

namespace AlgoSdk.WalletConnect
{
    [Serializable]
    public struct SavedSession : IWalletConnectSession
    {
        [SerializeField]
        string clientId;

        [SerializeField]
        string bridgeUrl;

        [SerializeField]
        Hex key;

        [SerializeField]
        string peerId;

        [SerializeField]
        long handshakeId;

        [SerializeField]
        int networkId;

        [SerializeField]
        Address[] accounts;

        [SerializeField]
        int chainId;

        [SerializeField]
        ClientMeta dappMeta;

        [SerializeField]
        ClientMeta walletMeta;

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

        public long HandshakeId
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
    }
}
