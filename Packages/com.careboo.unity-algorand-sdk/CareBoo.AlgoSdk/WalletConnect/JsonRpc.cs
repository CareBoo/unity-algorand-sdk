using System.Text;

namespace AlgoSdk.WalletConnect
{
    public static class JsonRpc
    {
        public static NetworkMessage ToWalletConnectMessage<T>(this T request, Hex key, string topic)
            where T : JsonRpcRequest
        {
            var payloadData = Encoding.UTF8.GetBytes(AlgoApiSerializer.SerializeJson(request));
            var encryptedPayload = AesCipher.EncryptWithKey(key, payloadData);
            return new NetworkMessage
            {
                Payload = AlgoApiSerializer.SerializeJson(encryptedPayload),
                Type = "pub",
                Topic = topic
            };
        }

        public static T ToJsonRpcResponse<T>(this EncryptedPayload encryptedPayload, Hex key)
            where T : JsonRpcResponse
        {
            var responseData = AesCipher.DecryptWithKey(key, encryptedPayload);
            return AlgoApiSerializer.DeserializeJson<T>(responseData);
        }
    }
}
