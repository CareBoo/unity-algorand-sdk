using AlgoSdk.Json;
using AlgoSdk.MessagePack;

namespace AlgoSdk
{
    public class JsonRpcResponseFormatter<T, U> : IAlgoApiFormatter<T>
        where T : struct, JsonRpcResponse<U>
    {
        public T Deserialize(ref JsonReader reader)
        {
            if (reader.TryRead(JsonToken.ObjectBegin))
                JsonReadError.IncorrectType.ThrowIfError(reader.Char, reader.Position);

            var result = new T();
            while (true)
            {
                if (reader.Peek() == JsonToken.ObjectEnd)
                    break;

                reader.ReadString(out var key)
                    .ThrowIfError(reader.Char, reader.Position);

                switch (key)
                {
                    case "id":
                        result.Id = AlgoApiFormatterCache<ulong>.Formatter.Deserialize(ref reader);
                        break;
                    case "jsonrpc":
                        result.JsonRpc = AlgoApiFormatterCache<string>.Formatter.Deserialize(ref reader);
                        break;
                    case "result":
                        result.Result = AlgoApiFormatterCache<U>.Formatter.Deserialize(ref reader);
                        break;
                    default:
                        JsonReadError.IncorrectFormat.ThrowIfError(reader.Char, reader.Position);
                        break;
                }
            }
            if (!reader.TryRead(JsonToken.ObjectEnd))
                JsonReadError.IncorrectFormat.ThrowIfError(reader.Char, reader.Position);
            return result;
        }

        public T Deserialize(ref MessagePackReader reader)
        {
            throw new System.NotSupportedException();
        }

        public void Serialize(ref JsonWriter writer, T value)
        {
            throw new System.NotSupportedException();
        }

        public void Serialize(ref MessagePackWriter writer, T value)
        {
            throw new System.NotSupportedException();
        }
    }
}
