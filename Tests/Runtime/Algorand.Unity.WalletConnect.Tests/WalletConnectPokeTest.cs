using System.Collections;
using Algorand.Unity.WalletConnect.Core;
using Algorand.Unity.JsonRpc;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.Text;

namespace Algorand.Unity.WalletConnect.Test
{
    public class WalletConnectPokeTest
    {
        [UnityTest]
        [Ignore("This test requires a running relay server. It is not meant to be run as part of the CI pipeline.")]
        [Timeout(5000)]
        public IEnumerator PokeRelay()
        {
            const string relayUrl = "wss://relay.walletconnect.com";
            const string projectId = "da87b3345726d69030aa66218202ec19";
            var userAgent = new UserAgent
            {
                protocol = "wc",
                version = "2",
                sdkVersion = "5.0.0-exp.1"
            };
            var topic = Topic.Random();
            var relayClient = new RelayClient(relayUrl, projectId, userAgent);
            relayClient.Connect();
            const int maxIterations = 50000;

            var helloWorldId = 0UL;
            var fetchMessagesId = 0UL;

            for (var i = 0; i < maxIterations; i++)
            {
                yield return null;
                var evt = relayClient.Poll();
                switch (evt.Type)
                {
                    case JsonRpcEventType.Close:
                        Debug.LogError($"Got close with reason: {evt.Reason}");
                        continue;
                    case JsonRpcEventType.Error:
                        Debug.LogError($"Got error with message: {evt.Error}");
                        continue;
                    case JsonRpcEventType.Open:
                        Debug.Log("Got open");
                        helloWorldId = relayClient.Publish(topic, "Hello World!", 1);
                        Debug.Log($"Publishing hello world message with id={helloWorldId}");
                        continue;
                    case JsonRpcEventType.Response:
                        Debug.Log(Encoding.UTF8.GetString(evt.Response.Result.Json));
                        Debug.Log($"Got JSON RPC response: {AlgoApiSerializer.SerializeJson(evt.Response)}");
                        if (evt.Response.Id == helloWorldId)
                        {
                            Debug.Log("Got hello world confirmation. Fetching messages in same topic.");
                            fetchMessagesId = relayClient.FetchMessages(topic);
                        }
                        if (evt.Response.Id == fetchMessagesId)
                        {
                            Debug.Log("Fetched messages");
                        }
                        continue;
                    case JsonRpcEventType.Request:
                        Debug.Log($"Got JSON RPC request: {AlgoApiSerializer.SerializeJson(evt.Request)}");
                        continue;
                }
            }
        }
    }
}
