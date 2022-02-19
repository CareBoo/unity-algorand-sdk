using System;
using System.Collections;
using System.Text;
using AlgoSdk.LowLevel;
using Cysharp.Threading.Tasks;
using Netcode.Transports.WebSocket;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;
using static Netcode.Transports.WebSocket.WebSocketEvent;

namespace AlgoSdk.WalletConnect
{
    public class AlgorandWalletConnectSession : IActiveWalletConnectSession<AlgorandWalletConnectSession>
    {
        public const int AlgorandChainId = 4160;

        public UnityEvent<AlgorandWalletConnectSession> OnSessionConnect { get; set; } = new UnityEvent<AlgorandWalletConnectSession>();

        public UnityEvent OnSessionDisconnect { get; set; } = new UnityEvent();

        public UnityEvent<WalletConnectSessionData> OnSessionUpdate { get; set; } = new UnityEvent<WalletConnectSessionData>();

        public string Url => session.Url;

        public Address[] Accounts => session.Accounts;

        public string Version => session.Version;

        public bool SessionConnected { get; private set; }

        public int ChainId => session.ChainId;

        public long HandshakeId => session.HandshakeId;

        public int NetworkId => session.NetworkId;

        public string PeerId => session.PeerId;

        public string ClientId => session.ClientId;

        public string BridgeUrl => session.BridgeUrl;

        public Hex Key => session.Key;

        public ClientMeta DappMeta => session.DappMeta;

        public ClientMeta WalletMeta => session.WalletMeta;

        IWebSocketClient webSocketClient;

        SavedSession session;

        IEnumerator pollingUpdate;

        protected AlgorandWalletConnectSession(
            ClientMeta clientMeta,
            string bridgeUrl = null
        )
        {
            using var secret = new NativeByteArray(32, Allocator.Temp);
            Crypto.Random.Randomize(secret);

            session = new SavedSession
            {
                ClientId = Guid.NewGuid().ToString(),
                BridgeUrl = bridgeUrl ?? DefaultBridge.GetRandomBridgeUrl(),
                Key = secret.ToArray(),
                PeerId = Guid.NewGuid().ToString(),
                ChainId = AlgorandChainId,
                DappMeta = clientMeta,
                Version = "1"
            };
            session.Url = $"wc:{session.PeerId}@{session.Version}?bridge={session.BridgeUrl}&key={session.Key}";
        }

        protected AlgorandWalletConnectSession(
            SavedSession savedSession
        )
        {
            session = savedSession;
        }

        public void Dispose()
        {
            webSocketClient.Close();
        }

        public SavedSession Save()
        {
            return session;
        }

        async UniTask ConnectToBridgeServer()
        {
            webSocketClient = WebSocketClientFactory.Create(BridgeUrl.Replace("http", "ws"));
            webSocketClient.Connect();
            while (true)
            {
                var response = webSocketClient.Poll();
                switch (response.Type)
                {
                    case WebSocketEventType.Open:
                        return;
                    case WebSocketEventType.Nothing:
                        break;
                    default:
                        throw new Exception($"Could not connect to bridge server: {response.Error}");
                }
                await UniTask.Yield();
            }
        }

        void PublishRequest<T>(T obj) where T : JsonRpcRequest
        {
            var payloadData = Encoding.UTF8.GetBytes(AlgoApiSerializer.SerializeJson(obj));
            var encryptedPayload = AesCipher.EncryptWithKey(Key, payloadData);
            var msg = new NetworkMessage
            {
                Payload = AlgoApiSerializer.SerializeJson(encryptedPayload),
                Type = "pub",
                Topic = PeerId
            };
            var msgData = Encoding.UTF8.GetBytes(JsonUtility.ToJson(msg));
            webSocketClient.Send(new ArraySegment<byte>(msgData));
        }

        public static async UniTask<AlgorandWalletConnectSession> StartNewSession(
            ClientMeta clientMeta,
            string bridgeUrl = null
        )
        {
            var session = new AlgorandWalletConnectSession(clientMeta, bridgeUrl);
            await session.ConnectToBridgeServer();
            return session;
        }

        public static async UniTask<AlgorandWalletConnectSession> ContinueSavedSession(
            SavedSession savedSession
        )
        {
            var session = new AlgorandWalletConnectSession(savedSession);
            await session.ConnectToBridgeServer();
            session.SessionConnected = true;
            return session;
        }
    }
}
