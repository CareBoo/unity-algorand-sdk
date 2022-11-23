using Algorand.Unity.Json;
using Algorand.Unity.MessagePack;

namespace Algorand.Unity
{
    public sealed class WrappedValueFormatter<T, U> : IAlgoApiFormatter<T>
        where T : struct, IWrappedValue<U>
    {
        public T Deserialize(ref JsonReader reader)
        {
            T result = default;
            result.WrappedValue = AlgoApiFormatterCache<U>.Formatter.Deserialize(ref reader);
            return result;
        }

        public T Deserialize(ref MessagePackReader reader)
        {
            T result = default;
            result.WrappedValue = AlgoApiFormatterCache<U>.Formatter.Deserialize(ref reader);
            return result;
        }

        public void Serialize(ref JsonWriter writer, T value)
        {
            AlgoApiFormatterCache<U>.Formatter.Serialize(ref writer, value.WrappedValue);
        }

        public void Serialize(ref MessagePackWriter writer, T value)
        {
            AlgoApiFormatterCache<U>.Formatter.Serialize(ref writer, value.WrappedValue);
        }
    }
}
