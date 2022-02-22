using System;
using System.Collections;
using System.Text;
using AlgoSdk;
using AlgoSdk.LowLevel;
using AlgoSdk.WalletConnect;
using Cysharp.Threading.Tasks;
using Netcode.Transports.WebSocket;
using NUnit.Framework;
using Unity.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using WebSocketSharp;
using static Netcode.Transports.WebSocket.WebSocketEvent;

[TestFixture]
[Ignore("This test is only used for manual testing of walletconnect")]
public class WalletConnectTest
{
    static readonly ClientMeta DappMeta = new ClientMeta
    {
        Description = "This is a test request",
        Url = "www.example.com",
        IconUrls = new[] { "www.example.com/icon1.png", "www.example.com/icon2.png" },
        Name = "test"
    };

    [UnityTest]
    public IEnumerator TestConnection() => UniTask.ToCoroutine(async () =>
    {
        var topic = Guid.NewGuid().ToString();
        var dappId = Guid.NewGuid().ToString();
        var bridgeUrl = DefaultBridge.GetRandomBridgeUrl().Replace("http", "ws");
        var client = WebSocketClientFactory.Create(bridgeUrl);
        client.Connect();
        var sessionRequest = WalletConnectRpc.SessionRequest(
            peerId: dappId,
            peerMeta: DappMeta,
            chainId: WalletConnectRpc.Algorand.ChainId
        );
        var data = Encoding.UTF8.GetBytes(AlgoApiSerializer.SerializeJson(sessionRequest));
        var key = GetKey();
        var msg = sessionRequest.SerializeAsNetworkMessage(key, topic);
        Debug.Log(Encoding.UTF8.GetString(msg));
        try
        {
            client.Send(new ArraySegment<byte>(msg));
        }
        catch (WebSocketException)
        {
            Assert.Ignore("Unable to send message using websockets.");
        }
        var responseEvent = client.Poll();
        Log(responseEvent, "dapp");
        await WaitForMessage(bridgeUrl, topic, key);
        var count = 0;
        while (
            (responseEvent.Type == WebSocketEvent.WebSocketEventType.Open
            || responseEvent.Type == WebSocketEvent.WebSocketEventType.Nothing)
            && count < 1
        )
        {
            await UniTask.Delay(500);
            count++;
            responseEvent = client.Poll();
            Log(responseEvent, "dapp");
        }
    });

    async UniTask WaitForMessage(string bridgeUrl, string topic, Hex key)
    {
        var client = WebSocketClientFactory.Create(bridgeUrl);
        client.Connect();
        var message = NetworkMessage.SubscribeToTopic(topic);
        var messageData = Encoding.UTF8.GetBytes(AlgoApiSerializer.SerializeJson(message));
        client.Send(new ArraySegment<byte>(messageData));

        var responseEvent = client.Poll();
        Log(responseEvent, "wallet");
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
            Log(responseEvent, "wallet");
        }
    }

    Hex GetKey()
    {
        using var secret = new NativeByteArray(32, Allocator.Temp);
        AlgoSdk.Crypto.Random.Randomize(secret);
        return secret.ToArray();
    }

    void Log(WebSocketEvent webSocketEvent, string clientId)
    {
        var details = webSocketEvent.Type switch
        {
            WebSocketEventType.Close => webSocketEvent.Reason,
            WebSocketEventType.Error => webSocketEvent.Error,
            WebSocketEventType.Payload => $"[Payload: {Encoding.UTF8.GetString(webSocketEvent.Payload)}] [Error: {webSocketEvent.Error}] [Reason: {webSocketEvent.Reason}]",
            _ => ""
        };
        Debug.Log($"{clientId} WebSocketEvent {webSocketEvent.Type}: {details}");
    }
}
