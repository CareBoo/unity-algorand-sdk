using System;
using System.Text;
using System.Threading;
using AlgoSdk.LowLevel;
using Cysharp.Threading.Tasks;
using Netcode.Transports.WebSocket;
using Unity.Collections;
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

        public UnityEvent<WebSocketEvent> OnMessageReceived { get; set; } = new UnityEvent<WebSocketEvent>();

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

        CancellationTokenSource pollCancellationTokenSource;

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

        protected AlgorandWalletConnectSession(SavedSession savedSession)
        {
            session = savedSession;
        }

        public void Dispose()
        {
            if (pollCancellationTokenSource != null)
            {
                pollCancellationTokenSource.Cancel();
                pollCancellationTokenSource = null;
            }
            webSocketClient.Close();
        }

        public SavedSession Save()
        {
            return session;
        }

        async UniTask ConnectToBridgeServer(CancellationToken cancellationToken)
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
                        await UniTask.Yield(cancellationToken);
                        break;
                    case WebSocketEventType.Error:
                        throw new Exception($"Could not connect to bridge server: {response.Error}");
                    default:
                        throw new InvalidOperationException($"Got unexpected response type {response.Type} while connecting to bridge server");
                }
            }
        }

        public async UniTask<JsonRpcResult<TResponse, TError>> PublishCustomRequest<TRequest, TResponse, TError>(TRequest request)
            where TRequest : JsonRpcRequest
            where TResponse : IJsonRpcResponse
            where TError : JsonRpcError
        {
            var requestMessage = request.SerializeAsNetworkMessage(Key, PeerId);
            webSocketClient.Send(new ArraySegment<byte>(requestMessage));
            throw new NotImplementedException();
        }

        void ConnectToSession()
        {
            if (SessionConnected)
            {
                throw new InvalidOperationException("Session is already connected and polling for events");
            }
            SessionConnected = true;
            pollCancellationTokenSource = new CancellationTokenSource();
            PollWebSocketEvents(pollCancellationTokenSource.Token).Forget();
        }

        async UniTaskVoid PollWebSocketEvents(CancellationToken cancellationToken)
        {
            while (true)
            {
                var response = webSocketClient.Poll();
                switch (response.Type)
                {
                    case WebSocketEventType.Nothing:
                        await UniTask.Yield(cancellationToken);
                        break;
                    case WebSocketEventType.Payload:
                        HandleWebSocketPayloadReceived(response);
                        break;
                    case WebSocketEventType.Error:
                        throw new InvalidOperationException($"Unexepected error occurred while polling for web socket events: {response.Error}");
                    case WebSocketEventType.Open:
                        throw new InvalidOperationException("Got web socket open when it should already be open.");
                    case WebSocketEventType.Close:
                        throw new InvalidOperationException("Unexpected close of web socket while polling");
                    default:
                        throw new NotSupportedException($"The WebSocketEventType {response.Type} is not supported.");
                }
            }
        }

        void HandleWebSocketPayloadReceived(WebSocketEvent response)
        {
        }

        public static async UniTask<AlgorandWalletConnectSession> StartNewSession(
            ClientMeta clientMeta,
            string bridgeUrl = null
        )
        {
            var session = new AlgorandWalletConnectSession(clientMeta, bridgeUrl);
            var cts = new CancellationTokenSource();
            cts.CancelAfterSlim(TimeSpan.FromMinutes(1));
            await session.ConnectToBridgeServer(cts.Token);
            return session;
        }

        public static async UniTask<AlgorandWalletConnectSession> ContinueSavedSession(
            SavedSession savedSession
        )
        {
            var session = new AlgorandWalletConnectSession(savedSession);
            var cts = new CancellationTokenSource();
            cts.CancelAfterSlim(TimeSpan.FromMinutes(1));
            await session.ConnectToBridgeServer(cts.Token);
            session.ConnectToSession();
            return session;
        }
    }
}
