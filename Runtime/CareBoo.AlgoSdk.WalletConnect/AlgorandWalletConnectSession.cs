using System;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace AlgoSdk.WalletConnect
{
    public class AlgorandWalletConnectSession
        : IActiveWalletConnectSession<AlgorandWalletConnectSession>
    {
        SavedSession sessionData;

        JsonRpcClient rpc;

        UniTask<JsonRpcResponse> listeningForApproval;

        /// <inheritdoc />
        public event Action<AlgorandWalletConnectSession> OnSessionConnect;

        /// <inheritdoc />
        public event Action<string> OnSessionDisconnect;

        /// <inheritdoc />
        public event Action<WalletConnectSessionData> OnSessionUpdate;

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
        public string HandshakeTopic { get; protected set; }

        /// <summary>
        /// Create a new session.
        /// </summary>
        /// <param name="clientMeta">The metadata of the Dapp.</param>
        /// <param name="bridgeUrl">An optional WalletConnect bridgeurl. e.g. https://bridge.walletconnect.org</param>
        public AlgorandWalletConnectSession(ClientMeta clientMeta, string bridgeUrl = null)
        {
            sessionData = SavedSession.InitSession(
                clientMeta,
                bridgeUrl
            );
            rpc = new JsonRpcClient(sessionData.BridgeUrl.Replace("http", "ws"), sessionData.Key);
        }

        /// <summary>
        /// Continue an existing session.
        /// </summary>
        /// <param name="savedSession">A previously existing session.</param>
        public AlgorandWalletConnectSession(SavedSession savedSession)
        {
            sessionData = savedSession;
            rpc = new JsonRpcClient(sessionData.BridgeUrl.Replace("http", "ws"), sessionData.Key);
        }

        public void Dispose()
        {
            rpc?.Dispose();
            rpc = null;
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
        /// Connect to WalletConnect Bridge and begin listening for messages.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async UniTask Connect(CancellationToken cancellationToken = default)
        {
            if (ConnectionStatus != SessionStatus.None)
                throw new InvalidOperationException($"Session connection status is {ConnectionStatus}");

            rpc.OnSocketClosed += OnSessionDisconnect;
            rpc.OnRequestReceived += HandleJsonRpcRequestReceived;
            await rpc.Connect(ClientId, cancellationToken);
            if (!string.IsNullOrEmpty(PeerId))
            {
                ConnectionStatus = SessionStatus.WalletConnected;
                OnSessionConnect(this);
            }
            else if (HandshakeId != default)
            {
                ConnectionStatus = SessionStatus.RequestingWalletConnection;
                listeningForApproval = rpc.ListenForResponse(HandshakeId);
            }
            else
            {
                ConnectionStatus = SessionStatus.NoWalletConnected;
            }
        }

        /// <summary>
        /// Start the handshake process for a new WalletConnect session.
        /// </summary>
        /// <returns>A WalletConnect Standard URI format (EIP-1328) used for handshaking.</returns>
        public HandshakeUrl RequestHandshake()
        {
            switch (ConnectionStatus)
            {
                case SessionStatus.NoWalletConnected:
                    sessionData.HandshakeId = WalletConnectRpc.GetRandomId();
                    HandshakeTopic = Guid.NewGuid().ToString();
                    var request = WalletConnectRpc.SessionRequest(
                        peerId: ClientId,
                        peerMeta: DappMeta,
                        id: HandshakeId
                    );
                    listeningForApproval = rpc.Send(request, HandshakeTopic);
                    ConnectionStatus = SessionStatus.RequestingWalletConnection;
                    break;
                case SessionStatus.RequestingWalletConnection:
                    break;
                default:
                    throw new InvalidOperationException($"Session connection status is {ConnectionStatus}");
            }
            return new HandshakeUrl(HandshakeTopic, Version, BridgeUrl, Key);
        }

        /// <summary>
        /// Wait for an approval response from the handshake.
        /// </summary>
        /// <param name="cancellationToken">
        /// An optional <see cref="CancellationToken"/> that can be used for things like timeouts.
        /// </param>
        public async UniTask WaitForWalletConnectionApproval(CancellationToken cancellationToken = default)
        {
            if (ConnectionStatus != SessionStatus.RequestingWalletConnection)
                throw new InvalidOperationException($"Session connection status is {ConnectionStatus}");

            var handshakeResponse = await listeningForApproval;
            var sessionData = AlgoApiSerializer.DeserializeJson<WalletConnectSessionData>(handshakeResponse.Result.Json);
            UpdateSession(sessionData);
            HandshakeTopic = null;
            ConnectionStatus = SessionStatus.WalletConnected;
            OnSessionConnect(this);
        }

        /// <summary>
        /// Disconnect from a <see cref="SessionStatus.WalletConnected"/> or <see cref="SessionStatus.RequestingWalletConnection"/> session.
        /// </summary>
        /// <param name="reason">An optional reason to inform the web socket client.</param>
        public void DisconnectWalletConnection(string reason = default)
        {
            rpc.Disconnect(reason);
            ConnectionStatus = SessionStatus.None;
            sessionData = SavedSession.InitSession(sessionData.DappMeta, sessionData.BridgeUrl);
            OnSessionDisconnect(reason);
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
            if (ConnectionStatus != SessionStatus.WalletConnected)
                throw new InvalidOperationException($"Session connection status is {ConnectionStatus}");

            if (ChainId != WalletConnectRpc.Algorand.ChainId)
                throw new InvalidOperationException($"Wallet does not have Algorand as the active chain.");

            var request = WalletConnectRpc.Algorand.SignTransactions(transactions, options);
            var response = await rpc.Send(request, PeerId, cancellationToken);
            if (response.IsError)
                return (SignTxnsError)response.Error;
            return AlgoApiSerializer.DeserializeJson<byte[][]>(response.Result.Json);
        }

        void HandleJsonRpcRequestReceived(JsonRpcRequest request)
        {
            switch (request.Method)
            {
                case WalletConnectRpc.SessionUpdateMethod:
                    if (request.Params == null || request.Params.Length != 1)
                        throw new NotSupportedException($"The JsonRpcRequest method \"{WalletConnectRpc.SessionUpdateMethod}\" only supports params of length 1.");
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
                DisconnectWalletConnection("Session no longer approved.");
                return;
            }

            this.sessionData.PeerId = sessionData.PeerId;
            this.sessionData.WalletMeta = sessionData.PeerMeta;
            this.sessionData.ChainId = sessionData.ChainId;
            this.sessionData.Accounts = sessionData.Accounts;
        }
    }
}
