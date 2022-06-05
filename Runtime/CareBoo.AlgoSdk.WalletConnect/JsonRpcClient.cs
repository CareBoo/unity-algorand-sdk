using System;
using System.Threading;
using AlgoSdk.WebSocket;
using Cysharp.Threading.Tasks;
using UnityEngine;
using static AlgoSdk.WebSocket.WebSocketEvent;

namespace AlgoSdk.WalletConnect
{
    /// <summary>
    /// Wrapper class around <see cref="IWebSocketClient"/> for making and receiving Json RPC messages.
    /// </summary>
    public class JsonRpcClient
        : IDisposable
    {
        IWebSocketClient webSocketClient;

        Hex key;

        TimeSpan timeout;

        CancellationTokenSource connectionCancellation;

        public event Action<JsonRpcRequest> OnRequestReceived;

        public event Action<JsonRpcResponse> OnResponseReceived;

        public event Action<string> OnSocketClosed;

        /// <summary>
        /// Start a client for handling JsonRpcEvents via websockets
        /// </summary>
        /// <param name="url">url to connect to the websocket server</param>
        /// <param name="key">The key to use to encrypt and decrypt payloads</param>
        public JsonRpcClient(string url, Hex key, Optional<TimeSpan> timeout = default)
        {
            webSocketClient = WebSocketClientFactory.Create(url);
            this.key = key;
            this.timeout = timeout.HasValue ? timeout.Value : TimeSpan.FromSeconds(60);
        }

        /// <summary>
        /// Connect to the Json RPC Server and begin listening for messages.
        /// </summary>
        /// <param name="clientId">The websocket topic to connect to.</param>
        /// <param name="cancellationToken">An optional cancellation to stop the connection early.</param>
        public async UniTask Connect(string clientId, CancellationToken cancellationToken = default)
        {
            var timeoutSource = new CancellationTokenSource();
            var cancellation = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, timeoutSource.Token);
            timeoutSource.CancelAfter(timeout);
            webSocketClient.Connect();
            await webSocketClient.PollUntilOpen(cancellation.Token);
            var msg = NetworkMessage.SubscribeToTopic(clientId);
            webSocketClient.Send(msg);
            connectionCancellation = new CancellationTokenSource();
            PollJsonRpcMessages(connectionCancellation.Token).Forget();
        }

        /// <summary>
        /// Disconnect from a currently connected Json RPC server.
        /// </summary>
        public void Disconnect(string reason = default)
        {
            CheckConnected();

            connectionCancellation?.Cancel();
            connectionCancellation = null;

            webSocketClient.Close(reason: reason);
        }

        /// <summary>
        /// Send a Json RPC Request to the server and listen for a response.
        /// </summary>
        /// <param name="request">The JsonRpcRequest to send.</param>
        /// <param name="peerId">The peer to receive the request.</param>
        /// <param name="cancellationToken">An optional cancellationToken to stop listening for a response.</param>
        /// <typeparam name="TJsonRpcRequest">The type of the request.</typeparam>
        /// <returns>A response from this request.</returns>
        public async UniTask<JsonRpcResponse> Send<TJsonRpcRequest>(
            TJsonRpcRequest request,
            string peerId,
            CancellationToken cancellationToken = default
            )
            where TJsonRpcRequest : IJsonRpcRequest
        {
            SendAndForget(request, peerId);
            return await ListenForResponse(request.Id, cancellationToken);
        }

        /// <summary>
        /// Send a Json RPC Request to the server without listening for a response.
        /// </summary>
        /// <param name="request">The JsonRpcRequest to send.</param>
        /// <param name="peerId">The peer to receive the request.</param>
        /// <typeparam name="TJsonRpcRequest">The type of the request.</typeparam>
        public void SendAndForget<TJsonRpcRequest>(
            TJsonRpcRequest request,
            string peerId
            )
            where TJsonRpcRequest : IJsonRpcRequest
        {
            CheckConnected();

            var msg = NetworkMessage.PublishToTopicEncrypted(request, key, peerId);
            webSocketClient.Send(msg);
        }

        /// <summary>
        /// Listen for a response given an id.
        /// </summary>
        /// <param name="id">The id of the expected response.</param>
        /// <param name="cancellationToken">An optional cancellation token for cancelling early.</param>
        /// <returns>A Response matching the given is</returns>
        public async UniTask<JsonRpcResponse> ListenForResponse(
            ulong id,
            CancellationToken cancellationToken = default
            )
        {
            var cancellation = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, connectionCancellation.Token);
            cancellation.CancelAfter(timeout);
            using var listeningForResponse = new ListenForResponseScope(id, OnResponseReceived);
            return await listeningForResponse.WaitForResponse(cancellation.Token);
        }

        public void Dispose()
        {
            connectionCancellation?.Cancel();
            connectionCancellation = null;

            if (webSocketClient != null && webSocketClient.ReadyState == WebSocketSharp.WebSocketState.Open)
            {
                webSocketClient.Close(reason: "session being disposed");
            }

            webSocketClient = null;
        }

        void CheckConnected()
        {
            if (webSocketClient == null || webSocketClient.ReadyState != WebSocketSharp.WebSocketState.Open)
                throw new InvalidOperationException($"{nameof(JsonRpcClient)} is not yet connected");
        }

        async UniTaskVoid PollJsonRpcMessages(CancellationToken cancellationToken)
        {
            try
            {
                while (true)
                {
                    var evt = await webSocketClient.PollUntilEvent(cancellationToken);
                    switch (evt.Type)
                    {
                        case WebSocketEventType.Payload:
                            var responseOrRequest = evt.ReadJsonRpcPayload(key);
                            if (responseOrRequest.TryGetValue1(out var response))
                                OnResponseReceived.Invoke(response);
                            else if (responseOrRequest.TryGetValue2(out var request))
                                OnRequestReceived.Invoke(request);
                            break;
                        case WebSocketEventType.Error:
                            Debug.LogError(evt.Error);
                            break;
                        case WebSocketEventType.Close:
                            OnSocketClosed(evt.Reason);
                            return;
                    }
                }
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }

        public class ListenForResponseScope
            : IDisposable
        {
            ulong requestId;
            bool hasReceivedResponse;
            Action<JsonRpcResponse> evt;
            JsonRpcResponse response;

            public ListenForResponseScope(ulong requestId, Action<JsonRpcResponse> evt)
            {
                this.requestId = requestId;
                this.hasReceivedResponse = false;
                this.evt = evt;
                this.response = default;
                this.evt += OnResponseReceived;
            }

            public async UniTask<JsonRpcResponse> WaitForResponse(CancellationToken cancellationToken = default)
            {
                while (!hasReceivedResponse)
                {
                    await UniTask.Yield(cancellationToken);
                    await UniTask.NextFrame(cancellationToken);
                }
                return response;
            }

            public void Dispose()
            {
                this.evt -= OnResponseReceived;
            }

            void OnResponseReceived(JsonRpcResponse response)
            {
                if (response.Id != requestId)
                    return;

                this.response = response;
                hasReceivedResponse = true;
            }
        }
    }
}
