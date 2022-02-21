using AlgoSdk.Json;
using AlgoSdk.MessagePack;

namespace AlgoSdk.WalletConnect
{
    public class JsonRpcRequestFormatter<T, U> : IAlgoApiFormatter<T>
        where T : IJsonRpcRequest<U>
    {
        public T Deserialize(ref JsonReader reader)
        {
            throw new System.NotSupportedException();
        }

        public T Deserialize(ref MessagePackReader reader)
        {
            throw new System.NotSupportedException();
        }

        public void Serialize(ref JsonWriter writer, T value)
        {
            writer.BeginObject();

            writer.WriteObjectKey("id");
            writer.WriteNumber(value.Id);

            writer.BeginNextItem();

            writer.WriteObjectKey("jsonrpc");
            writer.WriteString(value.JsonRpc);

            writer.BeginNextItem();

            writer.WriteObjectKey("method");
            writer.WriteString(value.Method);

            writer.BeginNextItem();

            writer.WriteObjectKey("params");
            AlgoApiSerializer.SerializeJson(value.Params);

            writer.EndObject();
        }

        public void Serialize(ref MessagePackWriter writer, T value)
        {
            throw new System.NotSupportedException();
        }
    }
}
