using System;
using System.Text;
using System.Threading;
using AlgoSdk.Json;
using AlgoSdk.WebSocket;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace AlgoSdk.WalletConnect
{
    public class AlgorandWalletConnectSession
        : IActiveWalletConnectSession<AlgorandWalletConnectSession>
    {
        const string SessionUpdateMethod = "wc_sessionUpdate";

        SavedSession sessionData;

        WalletConnectSessionEvents events;

        IWebSocketClient webSocketClient;

        CancellationTokenSource pollCancellationTokenSource;

        /// <inheritdoc />
        public UnityEvent<AlgorandWalletConnectSession> OnSessionConnect => events?.OnSessionConnect;

        /// <inheritdoc />
        public UnityEvent<string> OnSessionDisconnect => events?.OnSessionDisconnect;

        /// <inheritdoc />
        public UnityEvent<WalletConnectSessionData> OnSessionUpdate => events?.OnSessionUpdate;

        /// <summary>
        /// Occurs when this session received a JsonRpcResponse from a request that was made.
        /// </summary>
        public UnityEvent<JsonRpcResponse> OnResponseReceived { get; set; }

        /// <inheritdoc />
        public Address[] Accounts => sessionData.Accounts;

        /// <inheritdoc />
        public string Version => "1";

        public SessionStatus ConnectionStatus { get; private set; }

        /// <inheritdoc />
        public int ChainId => sessionData.ChainId;

        /// <inheritdoc />
        public ulong HandshakeId => sessionData.HandshakeId;

        /// <inheritdoc />
        public int NetworkId => sessionData.NetworkId;

        /// <inheritdoc />
        public string PeerId => sessionData.PeerId;

        /// <inheritdoc />
        public string ClientId => sessionData.ClientId;

        /// <inheritdoc />
        public string BridgeUrl => sessionData.BridgeUrl;

        /// <inheritdoc />
        public Hex Key => sessionData.Key;

        /// <inheritdoc />
        public ClientMeta DappMeta => sessionData.DappMeta;

        /// <inheritdoc />
        public ClientMeta WalletMeta => sessionData.WalletMeta;

        /// <summary>
        /// The current handshake topic if this session is handshaking.
        /// </summary>
        public string HandshakeTopic { get; set; }

        /// <summary>
        /// Create a new session.
        /// </summary>
        /// <param name="clientMeta">The metadata of the Dapp.</param>
        /// <param name="bridgeUrl">An optional WalletConnect bridgeurl. e.g. https://bridge.walletconnect.org</param>
        public AlgorandWalletConnectSession(
            ClientMeta clientMeta,
            string bridgeUrl = null,
            WalletConnectSessionEvents events = null
            )
        {
            sessionData = SavedSession.InitSession(
                clientMeta,
                bridgeUrl
            );
            this.events = events ?? new WalletConnectSessionEvents();
            webSocketClient = WebSocketClientFactory.Create(sessionData.BridgeUrl.Replace("http", "ws"));
            ConnectionStatus = SessionStatus.NoConnection;
        }

        /// <summary>
        /// Continue an existing session.
        /// </summary>
        /// <param name="savedSession">A previously existing session.</param>
        public AlgorandWalletConnectSession(
            SavedSession savedSession,
            WalletConnectSessionEvents events = null
            )
        {
            sessionData = savedSession;
            this.events = events ?? new WalletConnectSessionEvents();
            webSocketClient = WebSocketClientFactory.Create(sessionData.BridgeUrl.Replace("http", "ws"));
            if (!string.IsNullOrEmpty(PeerId))
            {
                ConnectionStatus = SessionStatus.Connected;
                pollCancellationTokenSource = new CancellationTokenSource();
                PollWebSocketEvents(pollCancellationTokenSource.Token).Forget();
            }
            else if (HandshakeId != default)
            {
                ConnectionStatus = SessionStatus.RequestingConnection;
            }
            else
            {
                ConnectionStatus = SessionStatus.NoConnection;
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
            return sessionData;
        }

        /// <summary>
        /// Begin the handshake process for a new WalletConnect session.
        /// </summary>
        /// <param name="cancellationToken">An optional <see cref="CancellationToken"/> for cancelling this request early.</param>
        /// <returns>A WalletConnect Standard URI format (EIP-1328) used for handshaking.</returns>
        public async UniTask<HandshakeUrl> StartConnection(CancellationToken cancellationToken = default)
        {
            if (ConnectionStatus != SessionStatus.NoConnection)
                throw new InvalidOperationException($"Session connection status is {ConnectionStatus}");

            await ConnectToBridgeServer(cancellationToken);
            SubscribeToMessages();
            RequestHandshake();
            ConnectionStatus = SessionStatus.RequestingConnection;
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
            if (ConnectionStatus != SessionStatus.RequestingConnection)
                throw new InvalidOperationException($"Session connection status is {ConnectionStatus}");

            var handshakeResponse = await PollUntilResponse(HandshakeId, cancellationToken);
            var sessionData = AlgoApiSerializer.DeserializeJson<WalletConnectSessionData>(handshakeResponse.Result.Json);
            UpdateSession(sessionData);
            HandshakeTopic = null;
            pollCancellationTokenSource = new CancellationTokenSource();
            PollWebSocketEvents(pollCancellationTokenSource.Token).Forget();
            ConnectionStatus = SessionStatus.Connected;
            OnSessionConnect.Invoke(this);
        }

        /// <summary>
        /// Disconnect from a <see cref="SessionStatus.Connected"/> or <see cref="SessionStatus.RequestingConnection"/> session.
        /// </summary>
        /// <param name="reason">An optional reason to inform the web socket client.</param>
        public void Disconnect(string reason = default)
        {
            TryCancelPolling();
            if (webSocketClient.ReadyState == WebSocketSharp.WebSocketState.Open)
                webSocketClient.Close(reason: reason);
            ConnectionStatus = SessionStatus.NoConnection;

            sessionData = SavedSession.InitSession(sessionData.DappMeta, sessionData.BridgeUrl);
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
            if (ConnectionStatus == SessionStatus.NoConnection)
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
                sessionData.HandshakeId = WalletConnectRpc.GetRandomId();
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
                    Debug.LogWarning($"Did not recognize payload: {s}\n{e}");
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

            this.sessionData.PeerId = sessionData.PeerId;
            this.sessionData.WalletMeta = sessionData.PeerMeta;
            this.sessionData.ChainId = sessionData.ChainId;
            this.sessionData.Accounts = sessionData.Accounts;
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
    }
}
