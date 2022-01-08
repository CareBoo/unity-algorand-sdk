using System;
using AlgoSdk.LowLevel;
using Unity.Collections;
using UnityEngine.Events;

namespace AlgoSdk.WalletConnect
{
    public class AlgorandWalletConnectSession : IWalletConnectSession<AlgorandWalletConnectSession>
    {
        Hex key;

        string bridgeUrl;

        string handshakeTopic;

        string clientId;

        long handshakeId;

        ClientMeta dappMeta;

        ClientMeta walletMeta;

        public UnityEvent<AlgorandWalletConnectSession> OnSessionConnect { get; set; } = new UnityEvent<AlgorandWalletConnectSession>();

        public UnityEvent OnSessionDisconnect { get; set; } = new UnityEvent();

        public UnityEvent<AlgorandWalletConnectSession> OnSend { get; set; } = new UnityEvent<AlgorandWalletConnectSession>();

        public UnityEvent<WalletConnectSessionData> OnSessionUpdate { get; set; } = new UnityEvent<WalletConnectSessionData>();

        public string Url => $"wc:{handshakeTopic}@{Version}?bridge={bridgeUrl}&key={key}";

        public Address[] Accounts { get; private set; }

        public string Version => "1";

        public bool SessionConnected { get; private set; }

        public int ChainId { get; private set; }

        public int NetworkId { get; private set; }

        public string PeerId { get; private set; }

        public AlgorandWalletConnectSession(
            ClientMeta clientMeta,
            string bridgeUrl = null
        )
        {
            dappMeta = clientMeta;
            handshakeTopic = Guid.NewGuid().ToString();
            clientId = Guid.NewGuid().ToString();

            using var secret = new NativeByteArray(32, Allocator.Temp);
            AlgoSdk.Crypto.Random.Randomize(secret);
            key = secret.ToArray();

            bridgeUrl = bridgeUrl ?? DefaultBridge.GetRandomBridgeUrl();
            if (bridgeUrl.StartsWith("http"))
                bridgeUrl = bridgeUrl.Replace("http", "ws");

            this.bridgeUrl = bridgeUrl;
        }

        public AlgorandWalletConnectSession(
            SavedSession savedSession
        )
        {
            dappMeta = savedSession.DappMeta;
            walletMeta = savedSession.WalletMeta;
            ChainId = savedSession.ChainId;

            clientId = savedSession.ClientId;

            Accounts = savedSession.Accounts;

            NetworkId = savedSession.NetworkId;

            SessionConnected = true;
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public SavedSession Save()
        {
            return new SavedSession
            {
                ClientId = clientId,
                BridgeUrl = bridgeUrl,
                Key = key,
                PeerId = PeerId,
                NetworkId = NetworkId,
                Accounts = Accounts,
                ChainId = ChainId,
                DappMeta = dappMeta,
                WalletMeta = walletMeta,
            };
        }

        void StartSession()
        {
            handshakeTopic = Guid.NewGuid().ToString();
            clientId = Guid.NewGuid().ToString();
            GenerateKey();
        }

        void GenerateKey()
        {
            using var secret = new NativeByteArray(32, Allocator.Temp);
            AlgoSdk.Crypto.Random.Randomize(secret);
            key = secret.Data.ToArray();
        }
    }
}
