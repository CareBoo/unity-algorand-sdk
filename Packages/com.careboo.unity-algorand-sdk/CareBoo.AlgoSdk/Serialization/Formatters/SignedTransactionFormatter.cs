using AlgoSdk.Json;
using AlgoSdk.MessagePack;

namespace AlgoSdk.Formatters
{
    public sealed class SignedTransactionFormatter<T>
        : IAlgoApiFormatter<T>
        where T : struct, ISignedTransaction
    {
        public T Deserialize(ref JsonReader reader)
        {
            var raw = AlgoApiFormatterCache<RawSignedTransaction>.Formatter.Deserialize(ref reader);
            T result = default;
            result.CopyFrom(in raw);
            return result;
        }

        public T Deserialize(ref MessagePackReader reader)
        {
            var raw = AlgoApiFormatterCache<RawSignedTransaction>.Formatter.Deserialize(ref reader);
            T result = default;
            result.CopyFrom(in raw);
            return result;
        }

        public void Serialize(ref JsonWriter writer, T value)
        {
            RawSignedTransaction raw = default;
            value.CopyTo(ref raw);
            AlgoApiFormatterCache<RawSignedTransaction>.Formatter.Serialize(ref writer, raw);
        }

        public void Serialize(ref MessagePackWriter writer, T value)
        {
            RawSignedTransaction raw = default;
            value.CopyTo(ref raw);
            AlgoApiFormatterCache<RawSignedTransaction>.Formatter.Serialize(ref writer, raw);
        }
    }
}
