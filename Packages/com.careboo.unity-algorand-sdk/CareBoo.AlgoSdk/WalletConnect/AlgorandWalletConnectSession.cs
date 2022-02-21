using System;
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

        const string SessionUpdateMethod = "wc_sessionUpdate";

        public UnityEvent<AlgorandWalletConnectSession> OnSessionConnect { get; private set; } = new UnityEvent<AlgorandWalletConnectSession>();

        public UnityEvent<string> OnSessionDisconnect { get; private set; } = new UnityEvent<string>();

        public UnityEvent<WalletConnectSessionData> OnSessionUpdate { get; private set; } = new UnityEvent<WalletConnectSessionData>();

        public UnityEvent<JsonRpcResponse> OnResponseReceived { get; private set; } = new UnityEvent<JsonRpcResponse>();

        public string Url => session.Url;

        public Address[] Accounts => session.Accounts;

        public string Version => session.Version;

        public Status ConnectionStatus { get; private set; }

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

        public AlgorandWalletConnectSession(ClientMeta clientMeta, string bridgeUrl = null)
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

        public AlgorandWalletConnectSession(SavedSession savedSession)
        {
            session = savedSession;
        }

        public void Dispose()
        {
            if (ConnectionStatus != Status.NotConnected)
                Disconnect();
        }

        public SavedSession Save()
        {
            return session;
        }

        public async UniTask Connect()
        {
            if (ConnectionStatus != Status.NotConnected)
                throw new InvalidOperationException($"Session connection status is {ConnectionStatus}");
            ConnectionStatus = Status.Connecting;
            try
            {
                using var timeout = new CancellationTokenSource();
                timeout.CancelAfterSlim(TimeSpan.FromMinutes(1));
                await ConnectToBridgeServer(timeout.Token);
                await ConnectToSession();
            }
            catch (Exception ex)
            {
                Disconnect(ex.Message);
                throw ex;
            }
            pollCancellationTokenSource = new CancellationTokenSource();
            PollWebSocketEvents(pollCancellationTokenSource.Token).Forget();
            ConnectionStatus = Status.Connected;
            OnSessionConnect.Invoke(this);
        }

        public void Disconnect(string reason = default)
        {
            if (ConnectionStatus == Status.NotConnected)
                throw new InvalidOperationException($"Session connection status is {ConnectionStatus}");

            TryCancelPolling();
            if (webSocketClient.ReadyState == WebSocketSharp.WebSocketState.Open)
                webSocketClient.Close(reason: reason);
            ConnectionStatus = Status.NotConnected;
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

        async UniTask ConnectToSession()
        {
            if (Accounts == null)
                await RequestWalletConnection();
        }

        async UniTaskVoid PollWebSocketEvents(CancellationToken cancellationToken)
        {
            try
            {
                while (true)
                {
                    var responseOrRequest = await PollUntilPayload(cancellationToken);
                    HandleResponseOrRequest(responseOrRequest);
                }
            }
            catch (Exception ex) when (ex is not OperationCanceledException)
            {
                OnSessionDisconnect.Invoke(ex.Message);
                ConnectionStatus = Status.NotConnected;
            }
        }

        async UniTask RequestWalletConnection()
        {
            var request = new WCSessionRequestRequest
            {
                Id = (ulong)UnityEngine.Random.Range(1, int.MaxValue),
                Params = new WCSessionRequestParams
                {
                    PeerId = ClientId,
                    PeerMeta = DappMeta,
                    ChainId = AlgorandChainId
                }
            };
            PublishRequest(request);
            var response = await PollUntilResponse(request.Id);
            var sessionData = AlgoApiSerializer.DeserializeJson<WalletConnectSessionData>(response.Result.Json);
            UpdateSession(sessionData);
        }

        async UniTask<JsonRpcResponse> PollUntilResponse(ulong id, CancellationToken cancellationToken = default)
        {
            while (true)
            {
                var responseOrRequest = await PollUntilPayload(cancellationToken);
                if (responseOrRequest.TryGetValue1(out var response) && response.Id == id)
                    return response;
            }
        }

        async UniTask<Either<JsonRpcResponse, JsonRpcRequest>> PollUntilPayload(CancellationToken cancellationToken = default)
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
                        return response.ReadJsonRpcPayload(Key);
                    case WebSocketEventType.Close:
                        throw new Exception(response.Reason);
                    case WebSocketEventType.Error:
                        throw new Exception(response.Error);
                    case WebSocketEventType.Open:
                        throw new InvalidOperationException($"Got web socket event {WebSocketEventType.Open} when it should already be open.");
                    default:
                        throw new NotSupportedException($"The WebSocketEventType {response.Type} is not supported.");
                }
            }
        }

        async UniTask<TResult> PublishRequestAndListenForResponse<TRequest, TResult>(TRequest request, CancellationToken cancellationToken = default)
            where TRequest : IJsonRpcRequest
        {
            AlgoApiObject result = default;
            var receivedResponse = false;
            var disconnected = false;
            void onResponse(JsonRpcResponse response)
            {
                if (response.Id != request.Id) return;
                receivedResponse = true;
                result = response.Result;
            }
            void onDisconnect(string reason)
            {
                disconnected = true;
            }
            OnResponseReceived.AddListener(onResponse);
            OnSessionDisconnect.AddListener(onDisconnect);

            try
            {
                PublishRequest(request);
                await UniTask.WaitUntil(() => receivedResponse || disconnected, cancellationToken: cancellationToken);
            }
            finally
            {
                OnResponseReceived.RemoveListener(onResponse);
                OnSessionDisconnect.RemoveListener(onDisconnect);
            }

            if (disconnected)
                throw new Exception("Server disconnected before request could be finished.");
            return AlgoApiSerializer.DeserializeJson<TResult>(result.Json);
        }

        void PublishRequest<TRequest>(TRequest request)
            where TRequest : IJsonRpcRequest
        {
            var requestMessage = request.SerializeAsNetworkMessage(Key, PeerId);
            webSocketClient.Send(new ArraySegment<byte>(requestMessage));
        }

        void HandleResponseOrRequest(Either<JsonRpcResponse, JsonRpcRequest> eitherResponseOrRequest)
        {
            if (eitherResponseOrRequest.TryGetValue1(out var response))
            {
                OnResponseReceived.Invoke(response);
            }
            else if (eitherResponseOrRequest.TryGetValue2(out var request))
            {
                HandleJsonRpcRequestReceived(request);
            }
        }

        void HandleJsonRpcRequestReceived(JsonRpcRequest request)
        {
            switch (request.Method)
            {
                case SessionUpdateMethod:
                    var sessionUpdateParams = AlgoApiSerializer.DeserializeJson<WalletConnectSessionData[]>(request.Params.Json);
                    if (sessionUpdateParams == null || sessionUpdateParams.Length != 1)
                        throw new NotSupportedException($"The JsonRpcRequest method \"{SessionUpdateMethod}\" only supports arrays of length 1.");
                    UpdateSession(sessionUpdateParams[0]);
                    OnSessionUpdate.Invoke(sessionUpdateParams[0]);
                    break;
                default:
                    throw new NotSupportedException($"The JsonRpcRequest method \"{request.Method}\" is not supported");
            }
        }

        void UpdateSession(WalletConnectSessionData sessionData)
        {
            if (!sessionData.IsApproved)
                throw new Exception("Wallet denied connection request.");

            session.PeerId = sessionData.PeerId;
            session.WalletMeta = sessionData.PeerMeta;
            session.NetworkId = sessionData.NetworkId;
            session.ChainId = sessionData.ChainId;
            session.Accounts = sessionData.Accounts;
        }

        bool TryCancelPolling()
        {
            if (pollCancellationTokenSource == null)
                return false;
            pollCancellationTokenSource.Cancel();
            pollCancellationTokenSource.Dispose();
            pollCancellationTokenSource = null;
            return true;
        }

        public enum Status
        {
            Unknown,
            NotConnected,
            Connecting,
            Connected
        }
    }
}
