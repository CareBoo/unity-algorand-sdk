using System;
using System.Collections.Generic;
using System.Net.WebSockets;

namespace Algorand.Unity.WebSocket
{
    public interface IWebSocketClient
    {
        Queue<WebSocketEvent> EventQueue { get; }

        WebSocketState ReadyState { get; }

        void Connect();
        void Close(WebSocketCloseStatus code = WebSocketCloseStatus.NormalClosure, string reason = null);
        void Send(ArraySegment<byte> data);
        WebSocketEvent Poll();
    }

    public static class WebSocketExtensions
    {
        public static void Send(this IWebSocketClient client, ReadOnlySpan<byte> data)
        {
            client.Send(new ArraySegment<byte>(data.ToArray()));
        }
    }
}
