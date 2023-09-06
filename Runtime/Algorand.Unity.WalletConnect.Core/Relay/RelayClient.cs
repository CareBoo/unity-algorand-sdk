using System.Net.WebSockets;
using Algorand.Unity.Collections;
using Algorand.Unity.JsonRpc;
using Algorand.Unity.WebSocket;
using Unity.Collections;
using UnityEngine.Networking;

namespace Algorand.Unity.WalletConnect.Core
{
    /// <summary>
    /// Wrapper around <see cref="IWebSocketClient"/> that implements the WalletConnect Relay Protocol.
    /// </summary>
    public struct RelayClient
    {
        public const ulong DefaultTimeToLive = 60 * 60 * 24;

        public readonly IWebSocketClient ws;

        public RelayClient(IWebSocketClient webSocket)
        {
            ws = webSocket;
        }

        public RelayClient(
            string relayUrl,
            string projectId,
            UserAgent userAgent)
        {
            var seed = Crypto.Random.Bytes<Crypto.Ed25519.Seed>();
            var auth = JwtUrl.SignJwt(relayUrl, seed);
            auth = UnityWebRequest.EscapeURL(auth);
            var ua = userAgent.ToString();
            ua = UnityWebRequest.EscapeURL(ua);

            var url = $"{relayUrl}?auth={auth}&projectId={projectId}&ua={ua}";
            ws = WebSocketClientFactory.Create(url);
        }

        /// <summary>
        /// Connects to the relay.
        /// </summary>
        public void Connect()
        {
            ws.Connect();
        }

        /// <summary>
        /// Closes the connection to the relay.
        /// </summary>
        /// <param name="code"></param>
        /// <param name="reason"></param>
        public void Close(WebSocketCloseStatus code = WebSocketCloseStatus.NormalClosure, string reason = null)
        {
            ws.Close(code, reason);
        }

        public JsonRpcEvent Poll()
        {
            var wsEvent = ws.Poll();
            switch (wsEvent.Type)
            {
                case WebSocketEvent.WebSocketEventType.Close:
                    return new JsonRpcEvent
                    {
                        Type = JsonRpcEventType.Close,
                        Reason = wsEvent.Reason
                    };
                case WebSocketEvent.WebSocketEventType.Error:
                    return new JsonRpcEvent
                    {
                        Type = JsonRpcEventType.Error,
                        Error = wsEvent.Error
                    };
                case WebSocketEvent.WebSocketEventType.Open:
                    return new JsonRpcEvent
                    {
                        Type = JsonRpcEventType.Open
                    };
                case WebSocketEvent.WebSocketEventType.Payload:
                    var json = wsEvent.Payload;
                    var eitherRequestOrResponse = AlgoApiSerializer.DeserializeJson<Either<JsonRpcRequest, JsonRpcResponse>>(json);
                    if (eitherRequestOrResponse.TryGetValue1(out var request))
                        return new JsonRpcEvent
                        {
                            Type = JsonRpcEventType.Request,
                            Request = request
                        };
                    else if (eitherRequestOrResponse.TryGetValue2(out var response))
                        return new JsonRpcEvent
                        {
                            Type = JsonRpcEventType.Response,
                            Response = response
                        };
                    return new JsonRpcEvent
                    {
                        Type = JsonRpcEventType.Error,
                        Error = "Could not deserialize payload"
                    };
                case WebSocketEvent.WebSocketEventType.Nothing:
                    return new JsonRpcEvent
                    {
                        Type = JsonRpcEventType.Nothing
                    };
                default:
                    return new JsonRpcEvent
                    {
                        Type = JsonRpcEventType.Error,
                        Error = $"Unknown WebSocketEvent type: {wsEvent.Type}"
                    };
            }
        }

        public ulong Publish(Topic topic, string message, ulong tag, ulong timeToLive = DefaultTimeToLive)
        {
            var request = new PublishRequestPayload
            {
                Topic = topic,
                Message = message,
                TimeToLive = timeToLive,
                Tag = tag
            };
            var jsonRpc = new JsonRpcRequest<PublishRequestPayload>
            {
                Id = RpcId.GenerateId(),
                JsonRpc = "2.0",
                Method = "irn_publish",
                Params = request
            };
            using var json = AlgoApiSerializer.SerializeJson(jsonRpc, Allocator.Temp);
            ws.Send(json.AsArray());
            return jsonRpc.Id;
        }

        public ulong Subscribe(Topic topic)
        {
            var request = new SubscribeRequestPayload
            {
                Topic = topic
            };
            var jsonRpc = new JsonRpcRequest<SubscribeRequestPayload>
            {
                Id = RpcId.GenerateId(),
                JsonRpc = "2.0",
                Method = "irn_subscribe",
                Params = request
            };
            using var json = AlgoApiSerializer.SerializeJson(jsonRpc, Allocator.Temp);
            ws.Send(json.AsArray());
            return jsonRpc.Id;
        }

        public ulong Unsubscribe(Topic topic, string id)
        {
            var request = new UnsubscribeRequestPayload
            {
                Topic = topic,
                Id = id
            };
            var jsonRpc = new JsonRpcRequest<UnsubscribeRequestPayload>
            {
                Id = RpcId.GenerateId(),
                JsonRpc = "2.0",
                Method = "irn_unsubscribe",
                Params = request
            };
            using var json = AlgoApiSerializer.SerializeJson(jsonRpc, Allocator.Temp);
            ws.Send(json.AsArray());
            return jsonRpc.Id;
        }

        public ulong FetchMessages(Topic topic)
        {
            var request = new FetchMessagesRequestPayload
            {
                Topic = topic
            };
            var jsonRpc = new JsonRpcRequest<FetchMessagesRequestPayload>
            {
                Id = RpcId.GenerateId(),
                JsonRpc = "2.0",
                Method = "irn_fetchMessages",
                Params = request
            };
            using var json = AlgoApiSerializer.SerializeJson(jsonRpc, Allocator.Temp);
            ws.Send(json.AsArray());
            return jsonRpc.Id;
        }
    }
}
