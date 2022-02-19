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

[TestFixture]
[Ignore("This test is only used for manual testing of walletconnect")]
public class WalletConnectTest
{
    [UnityTest]
    public IEnumerator TestConnection() => UniTask.ToCoroutine(async () =>
    {
        var topic = Guid.NewGuid().ToString();
        var bridgeUrl = DefaultBridge.GetRandomBridgeUrl().Replace("http", "ws");
        var client = WebSocketClientFactory.Create(bridgeUrl);
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
        var data = Encoding.UTF8.GetBytes(AlgoApiSerializer.SerializeJson(sessionRequest));
        var key = GetKey();
        var encryptedPayload = AesCipher.EncryptWithKey(key, data);
        var message = new NetworkMessage
        {
            Payload = AlgoApiSerializer.SerializeJson(encryptedPayload),
            Type = "pub",
            Topic = topic
        };
        var messageData = Encoding.UTF8.GetBytes(JsonUtility.ToJson(message));
        try
        {
            client.Send(new ArraySegment<byte>(messageData));
        }
        catch (WebSocketException)
        {
            Assert.Ignore("Unable to send message using websockets.");
        }
        await WaitForMessage(bridgeUrl, topic, key);
        var responseEvent = client.Poll();
        Debug.Log(
            $"ClientId: {responseEvent.ClientId}" +
            $"\nError: {responseEvent.Error}" +
            $"\nPayload: {(responseEvent.Payload == null ? "" : Encoding.UTF8.GetString(responseEvent.Payload))}" +
            $"\nReason: {responseEvent.Reason}" +
            $"\nType: {responseEvent.Type}"
        );
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
            Debug.Log(
                $"ClientId: {responseEvent.ClientId}" +
                $"\nError: {responseEvent.Error}" +
                $"\nPayload: {(responseEvent.Payload == null ? "" : Encoding.UTF8.GetString(responseEvent.Payload))}" +
                $"\nReason: {responseEvent.Reason}" +
                $"\nType: {responseEvent.Type}"
            );
        }
    });

    async UniTask WaitForMessage(string bridgeUrl, string topic, Hex key)
    {
        var client = WebSocketClientFactory.Create(bridgeUrl);
        client.Connect();
        var message = new NetworkMessage
        {
            Payload = "",
            Type = "sub",
            Topic = topic
        };
        var messageData = Encoding.UTF8.GetBytes(JsonUtility.ToJson(message));
        client.Send(new ArraySegment<byte>(messageData));

        var responseEvent = client.Poll();
        Debug.Log(
            $"ClientId: {responseEvent.ClientId}" +
            $"\nError: {responseEvent.Error}" +
            $"\nPayload: {(responseEvent.Payload == null ? "" : Encoding.UTF8.GetString(responseEvent.Payload))}" +
            $"\nReason: {responseEvent.Reason}" +
            $"\nType: {responseEvent.Type}"
        );
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
            Debug.Log(
                $"ClientId: {responseEvent.ClientId}" +
                $"\nError: {responseEvent.Error}" +
                $"\nPayload: {(responseEvent.Payload == null ? "" : Encoding.UTF8.GetString(responseEvent.Payload))}" +
                $"\nReason: {responseEvent.Reason}" +
                $"\nType: {responseEvent.Type}"
            );
        }
    }

    Hex GetKey()
    {
        using var secret = new NativeByteArray(32, Allocator.Temp);
        AlgoSdk.Crypto.Random.Randomize(secret);
        return secret.ToArray();
    }
}
