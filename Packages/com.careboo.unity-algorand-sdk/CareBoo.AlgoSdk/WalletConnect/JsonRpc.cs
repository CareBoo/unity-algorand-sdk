using System.Text;
using Unity.Collections;

namespace AlgoSdk.WalletConnect
{
    public static class JsonRpc
    {
        public static NetworkMessage ToWalletConnectMessage<T>(this T request, Hex key, string topic)
            where T : JsonRpcRequest
        {
            var payloadData = Encoding.UTF8.GetBytes(AlgoApiSerializer.SerializeJson(request));
            var encryptedPayload = AesCipher.EncryptWithKey(key, payloadData);
            using var encryptedPayloadJson = AlgoApiSerializer.SerializeJson(encryptedPayload, Allocator.Temp);
            return new NetworkMessage
            {
                Payload = new AlgoApiObject(encryptedPayloadJson.AsArray().ToArray(), ContentType.Json),
                Type = "pub",
                Topic = topic
            };
        }

        public static T ToJsonRpcResponse<T>(this EncryptedPayload encryptedPayload, Hex key)
            where T : IJsonRpcResponse
        {
            var responseData = AesCipher.DecryptWithKey(key, encryptedPayload);
            return AlgoApiSerializer.DeserializeJson<T>(responseData);
        }

        public static byte[] SerializeAsNetworkMessage<T>(this T request, Hex encryptionKey, string topic)
            where T : JsonRpcRequest
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

        public static JsonRpcResponse ReadJsonRpcResponse(byte[] websocketPayload, Hex encryptionKey)
        {
            var networkMessage = AlgoApiSerializer.DeserializeJson<NetworkMessage>(websocketPayload);
            var encryptedPayload = AlgoApiSerializer.DeserializeJson<EncryptedPayload>(networkMessage.Payload.Json);
            var responseData = AesCipher.DecryptWithKey(encryptionKey, encryptedPayload);
            return AlgoApiSerializer.DeserializeJson<JsonRpcResponse>(responseData);
        }
    }
}
