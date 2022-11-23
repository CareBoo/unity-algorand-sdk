using System;
using System.Threading;
using Algorand.Unity.Collections;
using Algorand.Unity.WebSocket;
using Cysharp.Threading.Tasks;
using Unity.Collections;
using static Algorand.Unity.WebSocket.WebSocketEvent;

namespace Algorand.Unity.WalletConnect
{
    public static class WebSocketExtensions
    {
        public static void Send(this IWebSocketClient client, NetworkMessage networkMessage)
        {
            using var networkMessageData = AlgoApiSerializer.SerializeJson(networkMessage, Allocator.Persistent);
            var networkMessageArraySegment = new ArraySegment<byte>(networkMessageData.AsArray().ToArray());
            client.Send(networkMessageArraySegment);
        }

        public static UniTask<WebSocketEvent> PollUntilOpen(
            this IWebSocketClient client,
            CancellationToken cancellationToken = default
            ) => client.PollUntilEvent(WebSocketEventType.Open, cancellationToken);

        public static UniTask<WebSocketEvent> PollUntilPayload(
            this IWebSocketClient client,
            CancellationToken cancellationToken = default
            ) => client.PollUntilEvent(WebSocketEventType.Payload, cancellationToken);

        public static async UniTask<WebSocketEvent> PollUntilEvent(
            this IWebSocketClient client,
            WebSocketEventType expectedEventType,
            CancellationToken cancellationToken = default
            )
        {
            var evt = await client.PollUntilEvent(cancellationToken);
            if (evt.Type == WebSocketEventType.Error)
                throw new Exception($"Got error web socket event: {evt.Error}");
            if (evt.Type == WebSocketEventType.Close)
                throw new Exception($"Got web socket closed event with reason: {evt.Reason}");
            if (evt.Type != expectedEventType)
                throw new InvalidOperationException($"Got unexpected event type {evt.Type}");
            return evt;
        }

        public static async UniTask<WebSocketEvent> PollUntilEvent(
            this IWebSocketClient client,
            CancellationToken cancellationToken = default
            )
        {
            if (cancellationToken.IsCancellationRequested)
                throw new OperationCanceledException();
            if (client == null)
                throw new ArgumentNullException(nameof(client));
            WebSocketEvent evt = client.Poll();
            while (evt.Type == WebSocketEventType.Nothing)
            {
                await UniTask.Yield(cancellationToken);
                evt = client.Poll();
            }
            return evt;
        }
    }
}
