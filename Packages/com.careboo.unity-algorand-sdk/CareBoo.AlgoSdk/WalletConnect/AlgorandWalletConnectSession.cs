using System;
using System.Text;
using AlgoSdk.LowLevel;
using Cysharp.Threading.Tasks;
using Netcode.Transports.WebSocket;
using static Netcode.Transports.WebSocket.WebSocketEvent;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace AlgoSdk.WalletConnect
{
    public class AlgorandWalletConnectSession : IWalletConnectSession<AlgorandWalletConnectSession>
    {
        public const int AlgorandChainId = 4160;

        public UnityEvent<AlgorandWalletConnectSession> OnSessionConnect { get; set; } = new UnityEvent<AlgorandWalletConnectSession>();

        public UnityEvent OnSessionDisconnect { get; set; } = new UnityEvent();

        public UnityEvent<AlgorandWalletConnectSession> OnSend { get; set; } = new UnityEvent<AlgorandWalletConnectSession>();

        public UnityEvent<WalletConnectSessionData> OnSessionUpdate { get; set; } = new UnityEvent<WalletConnectSessionData>();

        public string Url => $"wc:{PeerId}@{Version}?bridge={BridgeUrl}&key={Key}";

        public Address[] Accounts { get; private set; }

        public string Version => "1";

        public bool SessionConnected { get; private set; }

        public int ChainId { get; private set; }

        public long HandshakeId { get; private set; }

        public int NetworkId { get; private set; }

        public string PeerId { get; private set; }

        public string ClientId { get; private set; }

        public string BridgeUrl { get; private set; }

        public Hex Key { get; private set; }

        public ClientMeta DappMeta { get; private set; }

        public ClientMeta WalletMeta { get; private set; }

        IWebSocketClient webSocketClient;

        protected AlgorandWalletConnectSession(
            ClientMeta clientMeta,
            string bridgeUrl = null
        )
        {
            DappMeta = clientMeta;
            PeerId = Guid.NewGuid().ToString();
            ClientId = Guid.NewGuid().ToString();

            using var secret = new NativeByteArray(32, Allocator.Temp);
            AlgoSdk.Crypto.Random.Randomize(secret);
            Key = secret.ToArray();

            BridgeUrl = bridgeUrl ?? DefaultBridge.GetRandomBridgeUrl();
        }

        protected AlgorandWalletConnectSession(
            SavedSession savedSession
        )
        {
            ClientId = savedSession.ClientId;
            BridgeUrl = savedSession.BridgeUrl;
            Key = savedSession.Key;
            PeerId = savedSession.PeerId;
            NetworkId = savedSession.NetworkId;
            Accounts = savedSession.Accounts;
            ChainId = savedSession.ChainId;
            DappMeta = savedSession.DappMeta;
            WalletMeta = savedSession.WalletMeta;

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
                ClientId = ClientId,
                BridgeUrl = BridgeUrl,
                Key = Key,
                PeerId = PeerId,
                HandshakeId = HandshakeId,
                NetworkId = NetworkId,
                Accounts = Accounts,
                ChainId = ChainId,
                DappMeta = DappMeta,
                WalletMeta = WalletMeta,
            };
        }

        async UniTask StartSession()
        {
            webSocketClient = WebSocketClientFactory.Create(BridgeUrl.Replace("http", "ws"));
            webSocketClient.Connect();
            while (true)
            {
                var response = webSocketClient.Poll();
                switch (response.Type)
                {
                    case WebSocketEventType.Open: return;
                    case WebSocketEventType.Nothing: break;
                    default:
                        throw new Exception($"Could not start session: {response.Error}");
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

        void GenerateKey()
        {
            using var secret = new NativeByteArray(32, Allocator.Temp);
            AlgoSdk.Crypto.Random.Randomize(secret);
            Key = secret.Data.ToArray();
        }

        public static async UniTask<AlgorandWalletConnectSession> StartNew(
            ClientMeta clientMeta,
            string bridgeUrl = null
        )
        {
            var session = new AlgorandWalletConnectSession(clientMeta, bridgeUrl);
            await session.StartSession();
            return session;
        }

        public static async UniTask<AlgorandWalletConnectSession> StartSaved(
            SavedSession savedSession
        )
        {
            var session = new AlgorandWalletConnectSession(savedSession);
            await session.StartSession();
            return session;
        }
    }
}
