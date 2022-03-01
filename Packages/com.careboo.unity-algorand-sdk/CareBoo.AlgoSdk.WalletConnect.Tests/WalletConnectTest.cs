using System;
using System.Collections;
using System.Text;
using System.Threading;
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
    public IEnumerator TestConnection() => UniTask.ToCoroutine(async () =>
    {
        var timeout = new CancellationTokenSource();
        timeout.CancelAfterSlim(TimeSpan.FromSeconds(10));
        var session = new AlgorandWalletConnectSession(DappMeta);
        await session.StartConnection(timeout.Token);
        await WaitForRequest(session, timeout.Token);
        await session.WaitForConnectionApproval(timeout.Token);
    });

    async UniTask<string> WaitForRequest(AlgorandWalletConnectSession session, CancellationToken cancellationToken = default)
    {
        var peerId = Guid.NewGuid().ToString();
        var client = WebSocketClientFactory.Create(session.BridgeUrl.Replace("http", "ws"));
        client.Connect();
        await client.PollUntilOpen(cancellationToken);
        var message = NetworkMessage.SubscribeToTopic(session.HandshakeTopic);
        using var messageData = AlgoApiSerializer.SerializeJson(message, Allocator.Persistent);
        Debug.Log(messageData);
        client.Send(message);
        var evt = await client.PollUntilPayload(cancellationToken);
        Log(evt, "wallet");
        if (!evt.ReadJsonRpcPayload(session.Key).TryGetValue2(out var request))
            throw new Exception("payload was a response!");
        var approveResult = $"{{\"peerId\":\"{peerId}\",\"peerMeta\":{{\"description\":\"test\",\"url\":\"https://www.example.com\",\"icons\":[],\"name\":\"Test\"}},\"approved\":true,\"chainId\":4160,\"accounts\":[\"WHJRSOUX4P7HQBNGR6ZC3FKS3P3CUZUEZTL3LPN44JS37UTYZ4BLH2TLHA\"]}}";
        var jsonRpcResponse = new JsonRpcResponse { Id = session.HandshakeId, JsonRpc = "2.0", Result = approveResult };
        var sendMessage = NetworkMessage.PublishToTopicEncrypted(jsonRpcResponse, session.Key, session.ClientId);
        client.Send(sendMessage);
        return peerId;
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
