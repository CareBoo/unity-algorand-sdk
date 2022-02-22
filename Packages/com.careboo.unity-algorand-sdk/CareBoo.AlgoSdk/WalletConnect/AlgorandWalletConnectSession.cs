using System;
using System.Text;
using System.Threading;
using AlgoSdk.Json;
using AlgoSdk.LowLevel;
using AlgoSdk.WalletConnect;
using Cysharp.Threading.Tasks;
using Netcode.Transports.WebSocket;
using Unity.Collections;
using UnityEngine.Events;
using UnityEngine.Networking;
using static Netcode.Transports.WebSocket.WebSocketEvent;

namespace AlgoSdk.WalletConnect
{
    public class AlgorandWalletConnectSession : IActiveWalletConnectSession<AlgorandWalletConnectSession>
    {
        const string SessionUpdateMethod = "wc_sessionUpdate";

        public UnityEvent<AlgorandWalletConnectSession> OnSessionConnect { get; private set; } = new UnityEvent<AlgorandWalletConnectSession>();

        public UnityEvent<string> OnSessionDisconnect { get; private set; } = new UnityEvent<string>();

        public UnityEvent<WalletConnectSessionData> OnSessionUpdate { get; private set; } = new UnityEvent<WalletConnectSessionData>();

        public UnityEvent<JsonRpcResponse> OnResponseReceived { get; private set; } = new UnityEvent<JsonRpcResponse>();

        public string Url => session.Url;

        public Address[] Accounts => session.Accounts;

        public string Version => session.Version;

        public Status ConnectionStatus { get; private set; } = Status.NotConnected;

        public int ChainId => session.ChainId;

        public long HandshakeId => session.HandshakeId;

        public int NetworkId => session.NetworkId;

        public string PeerId => session.PeerId;

        public string ClientId => session.ClientId;

        public string BridgeUrl => session.BridgeUrl;

        public Hex Key => session.Key;

        public ClientMeta DappMeta => session.DappMeta;

        public ClientMeta WalletMeta => session.WalletMeta;

        public string Topic => ClientId;

        IWebSocketClient webSocketClient;

        SavedSession session;

        CancellationTokenSource pollCancellationTokenSource;

        /// <summary>
        /// Create a new session.
        /// </summary>
        /// <param name="clientMeta">The metadata of the Dapp.</param>
        /// <param name="bridgeUrl">An optional wallet connect bridgeurl. e.g. https://bridge.walletconnect.org</param>
        public AlgorandWalletConnectSession(ClientMeta clientMeta, string bridgeUrl = null)
        {
            using var secret = new NativeByteArray(32, Allocator.Temp);
            Crypto.Random.Randomize(secret);

            session = new SavedSession
            {
                ClientId = Guid.NewGuid().ToString(),
                BridgeUrl = string.IsNullOrEmpty(bridgeUrl) ? DefaultBridge.GetRandomBridgeUrl() : bridgeUrl,
                Key = secret.ToArray(),
                DappMeta = clientMeta,
                Version = "1",
            };
            var encodedBridgeUrl = UnityWebRequest.EscapeURL(session.BridgeUrl);
            session.Url = $"wc:{Topic}@{session.Version}?bridge={encodedBridgeUrl}&key={session.Key}";
            webSocketClient = WebSocketClientFactory.Create(session.BridgeUrl.Replace("http", "ws"));
        }

        /// <summary>
        /// Continue an existing session.
        /// </summary>
        /// <param name="savedSession">A previously existing session.</param>
        public AlgorandWalletConnectSession(SavedSession savedSession)
        {
            session = savedSession;
            webSocketClient = WebSocketClientFactory.Create(session.BridgeUrl.Replace("http", "ws"));
        }

        public void Dispose()
        {
            if (ConnectionStatus != Status.NotConnected)
                Disconnect();

            if (webSocketClient != null)
                webSocketClient = null;
        }

        /// <summary>
        /// Save the current session's state.
        /// </summary>
        /// <returns>A <see cref="SavedSession"/> that can be used for continuing an existing session later.</returns>
        public SavedSession Save()
        {
            return session;
        }

        /// <summary>
        /// Initiate a wallet connect session
        /// </summary>
        /// <param name="cancellationToken">
        /// An optional <see cref="CancellationToken"/> that can be used for things like timeouts.
        /// </param>
        public async UniTask Connect(CancellationToken cancellationToken = default)
        {
            if (ConnectionStatus != Status.NotConnected)
                throw new InvalidOperationException($"Session connection status is {ConnectionStatus}");
            ConnectionStatus = Status.Connecting;
            try
            {
                using var timeout = new CancellationTokenSource();
                timeout.CancelAfterSlim(TimeSpan.FromMinutes(1));
                await ConnectToBridgeServer(timeout.Token);
                await ConnectToSession(cancellationToken);
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

        /// <summary>
        /// Disconnect from a <see cref="Status.Connected"/> or <see cref="Status.Connecting"/> session.
        /// </summary>
        /// <param name="reason">An optional reason to inform the web socket client.</param>
        public void Disconnect(string reason = default)
        {
            if (ConnectionStatus == Status.NotConnected)
                throw new InvalidOperationException($"Session connection status is {ConnectionStatus}");

            TryCancelPolling();
            if (webSocketClient.ReadyState == WebSocketSharp.WebSocketState.Open)
            {
                webSocketClient.Close(reason: reason);
            }
            ConnectionStatus = Status.NotConnected;
        }

        /// <summary>
        /// Sign a group of transactions.
        /// </summary>
        /// <param name="options">Optional sign transaction options.</param>
        /// <param name="cancellationToken">
        /// Optional cancellation token used for interrupting this request.
        /// It's recommended to use <see cref="CancellationTokenSourceExtensions.CancelAfterSlim"/> for UniTask. 
        /// https://github.com/Cysharp/UniTask#timeout-handling
        /// </param>
        /// <param name="transactions">
        /// The atomic transaction group of [1,16] transactions. Contains information about how to sign
        /// each transaction, and which ones to sign.
        /// </param>
        /// <returns>
        /// Either the result of the request or a <see cref="JsonRpcError"/> if the request was invalid.
        /// 
        /// The result is an array of the same length as the number of transactions provided in
        /// <see cref="AlgoSignTxnsRequest.Params"/>.
        /// 
        /// For every index in this result, the value is
        /// <list type="bullet">
        ///     <item><c>null</c> if the wallet was not requested to sign this transaction</item>
        ///     <item>the canonical message pack encoding of the signed transaction</item>
        /// </list>
        /// </returns>
        public async UniTask<Either<byte[][], SignTxnsError>> SignTransactions(
            SignTxnsOpts options = default,
            CancellationToken cancellationToken = default,
            params WalletTransaction[] transactions
            )
        {
            if (ConnectionStatus == Status.NotConnected)
                throw new InvalidOperationException($"Session connection status is {ConnectionStatus}");

            var request = WalletConnectRpc.Algorand.SignTransactions(transactions, options);
            var listeningForResponse = ListenForResponse(request, cancellationToken);
            PublishRequest(request);
            var response = await listeningForResponse;
            return AlgoApiSerializer.DeserializeJson<Either<byte[][], SignTxnsError>>(response.Result.Json);
        }

        async UniTask ConnectToBridgeServer(CancellationToken cancellationToken = default)
        {
            webSocketClient.Connect();
            await PollUntilEventType(WebSocketEventType.Open, cancellationToken);
        }

        void SubscribeToTopic()
        {
            var msg = NetworkMessage.SubscribeToTopic(Topic);
            using var msgData = AlgoApiSerializer.SerializeJson(msg, Allocator.Persistent);
            webSocketClient.Send(new ArraySegment<byte>(msgData.AsArray().ToArray()));
        }

        async UniTask ConnectToSession(CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(PeerId))
                await RequestWalletConnection(cancellationToken);
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
            catch (Exception ex) when (!(ex is OperationCanceledException))
            {
                OnSessionDisconnect.Invoke(ex.Message);
                ConnectionStatus = Status.NotConnected;
            }
        }

        async UniTask RequestWalletConnection(CancellationToken cancellationToken = default)
        {
            var request = WalletConnectRpc.SessionRequest(
                peerId: ClientId,
                peerMeta: DappMeta
            );
            UnityEngine.Debug.Log($"requesting connection with params: {AlgoApiSerializer.SerializeJson(request)}");
            PublishRequest(request);
            var response = await PollUntilResponse(request.Id, cancellationToken);
            var sessionData = AlgoApiSerializer.DeserializeJson<WalletConnectSessionData>(response.Result.Json);
            UpdateSession(sessionData);
        }

        async UniTask<JsonRpcResponse> PollUntilResponse(ulong id, CancellationToken cancellationToken = default)
        {
            while (true)
            {
                var responseOrRequest = await PollUntilPayload(cancellationToken);
                if (responseOrRequest.TryGetValue1(out var response))
                {
                    UnityEngine.Debug.Log($"Got response: {AlgoApiSerializer.SerializeJson(response)}");
                    if (response.Id == id)
                        return response;
                }
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
                        try
                        {
                            return response.ReadJsonRpcPayload(Key);
                        }
                        catch (JsonReadException)
                        {
                            throw new Exception(Encoding.UTF8.GetString(response.Payload));
                        }
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

        async UniTask<WebSocketEvent> PollUntilEventType(WebSocketEventType expectedEventType, CancellationToken cancellationToken = default)
        {
            var response = webSocketClient.Poll();
            while (response.Type == WebSocketEventType.Nothing)
            {
                await UniTask.Yield(cancellationToken);
                response = webSocketClient.Poll();
            }
            if (response.Type == WebSocketEventType.Error)
                throw new Exception($"Got error web socket event: {response.Error}");
            if (response.Type != expectedEventType)
                throw new InvalidOperationException($"Got unexpected response type {response.Type}");
            return response;
        }

        void PublishRequest<TRequest>(TRequest request)
            where TRequest : IJsonRpcRequest
        {
            var requestMessage = request.SerializeAsNetworkMessage(Key, Topic);
            webSocketClient.Send(new ArraySegment<byte>(requestMessage));
        }

        async UniTask<JsonRpcResponse> ListenForResponse<TRequest>(TRequest request, CancellationToken cancellationToken = default)
            where TRequest : IJsonRpcRequest
        {
            JsonRpcResponse matchingResponse = default;
            var receivedResponse = false;
            var disconnected = false;
            void onResponse(JsonRpcResponse response)
            {
                if (response.Id != request.Id) return;
                receivedResponse = true;
                matchingResponse = response;
            }
            void onDisconnect(string reason)
            {
                disconnected = true;
            }
            OnResponseReceived.AddListener(onResponse);
            OnSessionDisconnect.AddListener(onDisconnect);

            try
            {
                await UniTask.WaitUntil(() => receivedResponse || disconnected, cancellationToken: cancellationToken);
            }
            finally
            {
                OnResponseReceived.RemoveListener(onResponse);
                OnSessionDisconnect.RemoveListener(onDisconnect);
            }

            if (disconnected)
                throw new Exception("Server disconnected before request could be finished.");
            return matchingResponse;
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
                    if (request.Params == null || request.Params.Length != 1)
                        throw new NotSupportedException($"The JsonRpcRequest method \"{SessionUpdateMethod}\" only supports params of length 1.");
                    var sessonUpdate = AlgoApiSerializer.DeserializeJson<WalletConnectSessionData>(request.Params[0].Json);
                    UpdateSession(sessonUpdate);
                    OnSessionUpdate.Invoke(sessonUpdate);
                    break;
                default:
                    throw new NotSupportedException($"The JsonRpcRequest method \"{request.Method}\" is not supported");
            }
        }

        void UpdateSession(WalletConnectSessionData sessionData)
        {
            if (!sessionData.IsApproved)
                throw new Exception("Wallet denied connection request.");

            if (sessionData.ChainId != WalletConnectRpc.Algorand.ChainId)
                throw new NotSupportedException($"ChainId {sessionData.ChainId} is not supported. Algorand's chain id is {WalletConnectRpc.Algorand.ChainId}");

            session.PeerId = sessionData.PeerId;
            session.WalletMeta = sessionData.PeerMeta;
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

namespace AlgoSdk
{
    [AlgoApiFormatter(typeof(EitherFormatter<byte[][], SignTxnsError>))]
    public partial struct Either<T, U> { }
}
