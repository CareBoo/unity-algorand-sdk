using Algorand.Unity.Collections;
using Algorand.Unity.WebSocket;
using Unity.Collections;

namespace Algorand.Unity.WalletConnect
{
    public static class JsonRpc
    {
        public static NetworkMessage ToNetworkMessage<T>(this T request, Hex encryptionKey, string topic)
            where T : IJsonRpcRequest
        {
            using var requestJson = AlgoApiSerializer.SerializeJson(request, Allocator.Persistent);
            var payloadData = requestJson.AsArray().ToArray();
            var encryptedPayload = AesCipher.EncryptWithKey(encryptionKey, payloadData);
            return NetworkMessage.PublishToTopic(encryptedPayload, topic);
        }

        public static Either<JsonRpcResponse, JsonRpcRequest> ReadJsonRpcPayload(this WebSocketEvent response, Hex encryptionKey)
        {
            var networkMessage = AlgoApiSerializer.DeserializeJson<NetworkMessage>(response.Payload);
            var encryptedPayload = AlgoApiSerializer.DeserializeJson<EncryptedPayload>(networkMessage.Payload);
            var responseData = AesCipher.DecryptWithKey(encryptionKey, encryptedPayload);
            return AlgoApiSerializer.DeserializeJson<Either<JsonRpcResponse, JsonRpcRequest>>(responseData);
        }
    }
}
