using System;
using Netcode.Transports.WebSocket;

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
            client.Connect();
        }

        public void Dispose()
        {
            if (client != null)
                client.Close();
            client = null;
        }
    }
}
