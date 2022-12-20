using System;
using System.Net.WebSockets;
using System.Threading;
using Algorand.Unity.Json;
using Algorand.Unity.WebSocket;
using Cysharp.Threading.Tasks;
using UnityEngine;
using static Algorand.Unity.WebSocket.WebSocketEvent;

namespace Algorand.Unity.WalletConnect
{
    /// <summary>
    /// Wrapper class around <see cref="IWebSocketClient"/> for making and receiving Json RPC messages.
    /// </summary>
    public class JsonRpcClient
        : IDisposable
    {
        private IWebSocketClient webSocketClient;

        private Hex key;

        private CancellationTokenSource connectionCancellation;

        public event Action<JsonRpcRequest> OnRequestReceived;

        public event Action<JsonRpcResponse> OnResponseReceived;

        public event Action<string> OnSocketClosed;

        /// <summary>
        /// Start a client for handling JsonRpcEvents via websockets
        /// </summary>
        /// <param name="url">url to connect to the websocket server</param>
        /// <param name="key">The key to use to encrypt and decrypt payloads</param>
        public JsonRpcClient(string url, Hex key)
        {
            webSocketClient = WebSocketClientFactory.Create(url);
            this.key = key;
        }

        /// <summary>
        /// Connect to the Json RPC Server and begin listening for messages.
        /// </summary>
        /// <param name="clientId">The websocket topic to connect to.</param>
        /// <param name="timeout">An optional timeout to cancel the connection early.</param>
        /// <param name="cancellationToken">An optional cancellation to stop the connection early.</param>
        public async UniTask Connect(
            string clientId,
            CancellationToken cancellationToken = default
            )
        {
            webSocketClient.Connect();
            await webSocketClient.PollUntilOpen(cancellationToken);
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
            Optional<JsonRpcResponse> matchingResponse = default;
            void onResponseReceived(JsonRpcResponse response)
            {
                if (response.Id != id)
                    return;

                matchingResponse = response;
                OnResponseReceived -= onResponseReceived;
            }
            OnResponseReceived += onResponseReceived;
            while (!matchingResponse.HasValue)
                await UniTask.Yield(cancellation.Token);

            return matchingResponse;
        }

        public void Dispose()
        {
            connectionCancellation?.Cancel();
            connectionCancellation = null;

            if (webSocketClient != null && webSocketClient.ReadyState == WebSocketState.Open)
            {
                webSocketClient.Close(reason: "session being disposed");
            }

            webSocketClient = null;
        }

        private void CheckConnected()
        {
            if (webSocketClient == null)
            {
                throw new InvalidOperationException($"{nameof(JsonRpcClient)}.{nameof(webSocketClient)} is null. Can't connect.");
            }

            var readyState = webSocketClient.ReadyState;
            if (readyState != WebSocketState.Open && readyState != WebSocketState.Connecting)
            {
                throw new InvalidOperationException($"{nameof(JsonRpcClient)} is not open. Current state: {readyState}");
            }
        }

        private async UniTaskVoid PollJsonRpcMessages(CancellationToken cancellationToken)
        {
            try
            {
                while (true)
                {
                    var evt = await webSocketClient.PollUntilEvent(cancellationToken);
                    switch (evt.Type)
                    {
                        case WebSocketEventType.Payload:
                            try
                            {
                                var responseOrRequest = evt.ReadJsonRpcPayload(key);
                                if (responseOrRequest.TryGetValue1(out var response) && OnResponseReceived != null)
                                    OnResponseReceived(response);
                                else if (responseOrRequest.TryGetValue2(out var request) && OnRequestReceived != null)
                                    OnRequestReceived(request);
                            }
                            catch (AggregateException ex)
                            {
                                foreach (var inner in ex.InnerExceptions)
                                    Debug.LogWarning(inner.ToString());
                                throw;
                            }
                            catch (JsonReadException ex)
                            {
                                Debug.LogWarning($"Response from JsonRpcServer was not Json:\n\"{System.Text.Encoding.UTF8.GetString(evt.Payload)}\"\nError:\n{ex}");
                            }
                            break;
                        case WebSocketEventType.Error:
                            Debug.LogWarning(evt.Error);
                            break;
                        case WebSocketEventType.Close:
                            if (OnSocketClosed != null)
                                OnSocketClosed(evt.Reason);
                            return;
                    }
                }
            }
            catch (OperationCanceledException)
            {
            }
        }
    }
}
