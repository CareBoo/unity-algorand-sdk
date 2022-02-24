using System;
using Netcode.Transports.WebSocket;
using WebSocketSharp;

namespace AlgoSdk.WalletConnect
{
    public struct WebSocketTransport : IDisposable
    {
        IWebSocketClient client;

        public WebSocketTransport(string url)
        {
            if (url.StartsWith("http"))
                url = url.Replace("http", "ws");
            client = WebSocketClientFactory.Create(url);
        }

        public void Open()
        {
            client.Connect();
        }

        public bool Connected => client.ReadyState == WebSocketState.Open;

        public void Dispose()
        {
            if (client != null)
                client.Close();
            client = null;
        }
    }
}
