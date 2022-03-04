using System;
using System.Text;
using System.Threading;
using AlgoSdk.Json;
using AlgoSdk.LowLevel;
using Cysharp.Threading.Tasks;
using Netcode.Transports.WebSocket;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace AlgoSdk.WalletConnect
{
    public class AlgorandWalletConnectSession : IActiveWalletConnectSession<AlgorandWalletConnectSession>
    {
        const string SessionUpdateMethod = "wc_sessionUpdate";

        public UnityEvent<AlgorandWalletConnectSession> OnSessionConnect { get; private set; } = new UnityEvent<AlgorandWalletConnectSession>();

        public UnityEvent<string> OnSessionDisconnect { get; private set; } = new UnityEvent<string>();

        public UnityEvent<WalletConnectSessionData> OnSessionUpdate { get; private set; } = new UnityEvent<WalletConnectSessionData>();

        public UnityEvent<JsonRpcResponse> OnResponseReceived { get; private set; } = new UnityEvent<JsonRpcResponse>();

        public Address[] Accounts => session.Accounts;

        public string Version => "1";

        public Status ConnectionStatus { get; private set; }

        public int ChainId => session.ChainId;

        public ulong HandshakeId => session.HandshakeId;

        public int NetworkId => session.NetworkId;

        public string PeerId => session.PeerId;

        public string ClientId => session.ClientId;

        public string BridgeUrl => session.BridgeUrl;

        public Hex Key => session.Key;

        public ClientMeta DappMeta => session.DappMeta;

        public ClientMeta WalletMeta => session.WalletMeta;

        public string HandshakeTopic { get; set; }

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
            session = InitSession(
                clientMeta,
                bridgeUrl
            );
            webSocketClient = WebSocketClientFactory.Create(session.BridgeUrl.Replace("http", "ws"));
            ConnectionStatus = Status.NoConnection;
        }

        /// <summary>
        /// Continue an existing session.
        /// </summary>
        /// <param name="savedSession">A previously existing session.</param>
        public AlgorandWalletConnectSession(SavedSession savedSession)
        {
            session = savedSession;
            webSocketClient = WebSocketClientFactory.Create(session.BridgeUrl.Replace("http", "ws"));
            if (!string.IsNullOrEmpty(PeerId))
            {
                ConnectionStatus = Status.Connected;
                pollCancellationTokenSource = new CancellationTokenSource();
                PollWebSocketEvents(pollCancellationTokenSource.Token).Forget();
            }
            else if (HandshakeId != default)
            {
                ConnectionStatus = Status.RequestingConnection;
            }
            else
            {
                ConnectionStatus = Status.NoConnection;
            }
        }

        public void Dispose()
        {
            TryCancelPolling();
            if (webSocketClient.ReadyState == WebSocketSharp.WebSocketState.Open)
                webSocketClient.Close(reason: "session being disposed");

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
        /// Begin the handshake process for a new WalletConnect session.
        /// </summary>
        /// <param name="cancellationToken">An optional <see cref="CancellationToken"/> for cancelling this request early.</param>
        /// <returns>A WalletConnect Standard URI format (EIP-1328) used for handshaking.</returns>
        public async UniTask<HandshakeUrl> StartConnection(CancellationToken cancellationToken = default)
        {
            if (ConnectionStatus != Status.NoConnection)
                throw new InvalidOperationException($"Session connection status is {ConnectionStatus}");

            await ConnectToBridgeServer(cancellationToken);
            SubscribeToMessages();
            RequestHandshake();
            ConnectionStatus = Status.RequestingConnection;
            return new HandshakeUrl(HandshakeTopic, Version, BridgeUrl, Key);
        }

        /// <summary>
        /// Wait for an approval response from the handshake.
        /// </summary>
        /// <param name="cancellationToken">
        /// An optional <see cref="CancellationToken"/> that can be used for things like timeouts.
        /// </param>
        public async UniTask WaitForConnectionApproval(CancellationToken cancellationToken = default)
        {
            if (ConnectionStatus != Status.RequestingConnection)
                throw new InvalidOperationException($"Session connection status is {ConnectionStatus}");

            var handshakeResponse = await PollUntilResponse(HandshakeId, cancellationToken);
            var sessionData = AlgoApiSerializer.DeserializeJson<WalletConnectSessionData>(handshakeResponse.Result.Json);
            UpdateSession(sessionData);
            HandshakeTopic = null;
            pollCancellationTokenSource = new CancellationTokenSource();
            PollWebSocketEvents(pollCancellationTokenSource.Token).Forget();
            ConnectionStatus = Status.Connected;
            OnSessionConnect.Invoke(this);
        }

        /// <summary>
        /// Disconnect from a <see cref="Status.Connected"/> or <see cref="Status.RequestingConnection"/> session.
        /// </summary>
        /// <param name="reason">An optional reason to inform the web socket client.</param>
        public void Disconnect(string reason = default)
        {
            TryCancelPolling();
            if (webSocketClient.ReadyState == WebSocketSharp.WebSocketState.Open)
                webSocketClient.Close(reason: reason);
            ConnectionStatus = Status.NoConnection;

            session = InitSession(session.DappMeta, session.BridgeUrl);
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
        /// Either the result of the request or a <see cref="IJsonRpcError"/> if the request was invalid.
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
        public async UniTask<Either<SignTxnsError, byte[][]>> SignTransactions(
            WalletTransaction[] transactions,
            SignTxnsOpts options = default,
            CancellationToken cancellationToken = default
            )
        {
            if (ConnectionStatus == Status.NoConnection)
                throw new InvalidOperationException($"Session connection status is {ConnectionStatus}");

            if (ChainId != WalletConnectRpc.Algorand.ChainId)
                throw new InvalidOperationException($"Wallet does not have Algorand as the active chain.");

            var request = WalletConnectRpc.Algorand.SignTransactions(transactions, options);
            var listeningForResponse = ListenForResponse(request, cancellationToken);
            var msg = NetworkMessage.PublishToTopicEncrypted(request, Key, PeerId);
            webSocketClient.Send(msg);
            var response = await listeningForResponse;
            if (response.IsError) return (SignTxnsError)response.Error;
            return AlgoApiSerializer.DeserializeJson<byte[][]>(response.Result.Json);
        }

        static SavedSession InitSession(ClientMeta dappMeta, string bridgeUrl)
        {
            return new SavedSession
            {
                ClientId = Guid.NewGuid().ToString(),
                BridgeUrl = string.IsNullOrEmpty(bridgeUrl) ? DefaultBridge.GetRandomBridgeUrl() : bridgeUrl,
                Key = GenKey(),
                DappMeta = dappMeta
            };
        }

        static Hex GenKey()
        {
            using var secret = new NativeByteArray(32, Allocator.Temp);
            Crypto.Random.Randomize(secret);
            return secret.ToArray();
        }

        async UniTask ConnectToBridgeServer(CancellationToken cancellationToken = default)
        {
            webSocketClient.Connect();
            await webSocketClient.PollUntilOpen(cancellationToken);
        }

        void SubscribeToMessages()
        {
            var msg = NetworkMessage.SubscribeToTopic(ClientId);
            webSocketClient.Send(msg);
        }

        async UniTaskVoid PollWebSocketEvents(CancellationToken cancellationToken)
        {
            try
            {
                while (true)
                {
                    var responseOrRequest = await PollUntilJsonRpcEvent(cancellationToken);
                    HandleResponseOrRequest(responseOrRequest);
                }
            }
            catch (Exception ex) when (!(ex is OperationCanceledException))
            {
                Debug.LogWarning(ex);
            }
        }

        void RequestHandshake()
        {
            if (HandshakeId == default)
                session.HandshakeId = WalletConnectRpc.GetRandomId();
            if (string.IsNullOrEmpty(HandshakeTopic))
                HandshakeTopic = Guid.NewGuid().ToString();
            var request = WalletConnectRpc.SessionRequest(
                peerId: ClientId,
                peerMeta: DappMeta,
                id: HandshakeId
            );
            var msg = NetworkMessage.PublishToTopicEncrypted(request, Key, HandshakeTopic);
            webSocketClient.Send(msg);
        }

        async UniTask<JsonRpcResponse> PollUntilResponse(ulong id, CancellationToken cancellationToken = default)
        {
            while (true)
            {
                var responseOrRequest = await PollUntilJsonRpcEvent(cancellationToken);
                if (responseOrRequest.TryGetValue1(out var response) && response.Id == id)
                {
                    return response;
                }
            }
        }

        async UniTask<Either<JsonRpcResponse, JsonRpcRequest>> PollUntilJsonRpcEvent(CancellationToken cancellationToken = default)
        {
            while (true)
            {
                var payloadEvent = await webSocketClient.PollUntilPayload(cancellationToken);
                try
                {
                    return payloadEvent.ReadJsonRpcPayload(Key);
                }
                catch (Exception e) when (e is JsonReadException || e is SerializationException || e is AggregateException)
                {
                    var s = Encoding.UTF8.GetString(payloadEvent.Payload);
                    Debug.LogWarning($"Did not recognize payload: {s}");
                }
            }
        }

        async UniTask<JsonRpcResponse> ListenForResponse<TRequest>(TRequest request, CancellationToken cancellationToken = default)
            where TRequest : IJsonRpcRequest
        {
            JsonRpcResponse matchingResponse = default;
            var receivedResponse = false;
            string disconnected = null;
            void onResponse(JsonRpcResponse response)
            {
                if (response.Id != request.Id) return;
                receivedResponse = true;
                matchingResponse = response;
            }
            void onDisconnect(string reason)
            {
                disconnected = reason;
            }
            OnResponseReceived.AddListener(onResponse);
            OnSessionDisconnect.AddListener(onDisconnect);

            try
            {
                await UniTask.WaitUntil(() => receivedResponse || disconnected != null, cancellationToken: cancellationToken);
            }
            finally
            {
                OnResponseReceived.RemoveListener(onResponse);
                OnSessionDisconnect.RemoveListener(onDisconnect);
            }

            if (disconnected != null)
                throw new Exception($"Server disconnected before request could be finished with reason: {disconnected}");
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
            {
                Disconnect("Session no longer approved.");
                return;
            }

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
            NoConnection,
            RequestingConnection,
            Connected
        }
    }
}
