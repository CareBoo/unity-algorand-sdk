using System;
using System.Collections;
using System.Text;
using AlgoSdk;
using AlgoSdk.WalletConnect;
using Cysharp.Threading.Tasks;
using Netcode.Transports.WebSocket;
using NUnit.Framework;
using Unity.Collections;
using UnityEngine;
using UnityEngine.TestTools;
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
    [Timeout(10000)]
    public IEnumerator TestConnection() => UniTask.ToCoroutine(async () =>
    {
        await WaitForMessage("", "", "");
    });

    async UniTask WaitForMessage(string bridgeUrl, string topic, string peerId)
    {
        var client = WebSocketClientFactory.Create(bridgeUrl.Replace("http", "ws"));
        client.Connect();
        await client.PollUntilOpen();
        var message = NetworkMessage.SubscribeToTopic(topic);
        using var messageData = AlgoApiSerializer.SerializeJson(message, Allocator.Persistent);
        Debug.Log(messageData);
        client.Send(new ArraySegment<byte>(messageData.AsArray().ToArray()));
        var evt = await client.PollUntilPayload();
        Log(evt, "wallet");
        var sendMessage = NetworkMessage.PublishToTopic((AlgoApiObject)"{\"test\":1}", peerId);
        client.Send(sendMessage);
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
