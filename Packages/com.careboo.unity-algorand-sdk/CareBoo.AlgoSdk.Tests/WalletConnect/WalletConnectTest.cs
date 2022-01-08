using System.Collections;
using Cysharp.Threading.Tasks;
using NUnit.Framework;
using UnityEngine.TestTools;
using Netcode.Transports.WebSocket;
using AlgoSdk.WalletConnect;
using System.Text;
using System;
using AlgoSdk;

[TestFixture]
public class WalletConnectTest
{
    const string connectionRequestJson = @"
{
    ""id"": 1,
    ""jsonrpc"": ""2.0"",
    ""method"": ""wc_sessionRequest"",
    ""params"": [
        {
            ""peerId"": """",
            ""peerMeta"": """",
            ""chainId"": null
        }
    ]
}
";

    [UnityTest]
    public IEnumerator TestConnection() => UniTask.ToCoroutine(async () =>
    {
        var client = WebSocketClientFactory.Create(DefaultBridge.GetRandomBridgeUrl().Replace("http", "ws"));
        client.Connect();
        var sessionRequest = new WCSessionRequestRequest
        {
            Id = 4,
            Params = new WCSessionRequestParams
            {
                PeerId = Guid.NewGuid().ToString(),
                PeerMeta = new ClientMeta
                {
                    Description = "This is a test request",
                    Url = "www.example.com",
                    IconUrls = new[] { "www.example.com/icon1.png", "www.example.com/icon2.png" },
                    Name = "test"
                }
            }
        };
        var json = AlgoApiSerializer.SerializeJson(sessionRequest);
        var data = Encoding.UTF8.GetBytes(json);
        client.Send(new ArraySegment<byte>(data));
        var responseEvent = client.Poll();
        var count = 0;
        while (
            (responseEvent.Type == WebSocketEvent.WebSocketEventType.Open
            || responseEvent.Type == WebSocketEvent.WebSocketEventType.Nothing)
            && count < 10
        )
        {
            await UniTask.Delay(500);
            count++;
            responseEvent = client.Poll();
        }
        UnityEngine.Debug.Log(
            $"ClientId: {responseEvent.ClientId}" +
            $"\nError: {responseEvent.Error}" +
            $"\nPayload: {(responseEvent.Payload == null ? "" : Encoding.UTF8.GetString(responseEvent.Payload))}" +
            $"\nReason: {responseEvent.Reason}" +
            $"\nType: {responseEvent.Type}"
        );
    });
}
