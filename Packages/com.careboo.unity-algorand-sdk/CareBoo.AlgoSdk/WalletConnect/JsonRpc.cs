using AlgoSdk.WalletConnect;
using Netcode.Transports.WebSocket;
using Unity.Collections;

namespace AlgoSdk.WalletConnect
{
    public static class JsonRpc
    {
        public static byte[] SerializeAsNetworkMessage<T>(this T request, Hex encryptionKey, string topic)
            where T : IJsonRpcRequest
        {
            using var requestJson = AlgoApiSerializer.SerializeJson(request, Allocator.Temp);
            var payloadData = requestJson.AsArray().ToArray();
            var encryptedPayload = AesCipher.EncryptWithKey(encryptionKey, payloadData);
            using var encryptedPayloadJson = AlgoApiSerializer.SerializeJson(encryptedPayload, Allocator.Temp);
            var networkMessage = new NetworkMessage
            {
                Payload = new AlgoApiObject(encryptedPayloadJson.AsArray().ToArray(), ContentType.Json),
                Type = "pub",
                Topic = topic
            };
            using var networkMessageJson = AlgoApiSerializer.SerializeJson(networkMessage, Allocator.Temp);
            return networkMessageJson.AsArray().ToArray();
        }

        public static Either<JsonRpcResponse, JsonRpcRequest> ReadJsonRpcPayload(this WebSocketEvent response, Hex encryptionKey)
        {
            var networkMessage = AlgoApiSerializer.DeserializeJson<NetworkMessage>(response.Payload);
            var encryptedPayload = AlgoApiSerializer.DeserializeJson<EncryptedPayload>(networkMessage.Payload.Json);
            var responseData = AesCipher.DecryptWithKey(encryptionKey, encryptedPayload);
            return AlgoApiSerializer.DeserializeJson<Either<JsonRpcResponse, JsonRpcRequest>>(responseData);
        }
    }
}

namespace AlgoSdk
{
    [AlgoApiFormatter(typeof(EitherFormatter<JsonRpcResponse, JsonRpcRequest>))]
    public partial struct Either<T, U> { }
}
