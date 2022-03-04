using System;
using System.Threading;
using AlgoSdk.WebSocket;
using Cysharp.Threading.Tasks;
using Unity.Collections;
using static AlgoSdk.WebSocket.WebSocketEvent;

namespace AlgoSdk.WalletConnect
{
    public static class WebSocketExtensions
    {
        public static void Send(this IWebSocketClient client, NetworkMessage networkMessage)
        {
            using var networkMessageData = AlgoApiSerializer.SerializeJson(networkMessage, Allocator.Temp);
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

        static async UniTask<WebSocketEvent> PollUntilEvent(
            this IWebSocketClient client,
            WebSocketEventType expectedEventType,
            CancellationToken cancellationToken = default
            )
        {
            var evt = client.Poll();
            while (evt.Type == WebSocketEventType.Nothing)
            {
                await UniTask.Yield(cancellationToken);
                evt = client.Poll();
            }
            if (evt.Type == WebSocketEventType.Error)
                throw new Exception($"Got error web socket event: {evt.Error}");
            if (evt.Type == WebSocketEventType.Close)
                throw new Exception($"Got web socket closed event with reason: {evt.Reason}");
            if (evt.Type != expectedEventType)
                throw new InvalidOperationException($"Got unexpected event type {evt.Type}");

            return evt;
        }
    }
}
