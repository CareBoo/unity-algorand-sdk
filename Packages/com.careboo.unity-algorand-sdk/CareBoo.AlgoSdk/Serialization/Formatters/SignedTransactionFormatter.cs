using System;
using AlgoSdk.Json;
using AlgoSdk.MessagePack;

namespace AlgoSdk.Formatters
{
    public sealed class SignedTransactionFormatter<T>
        : IAlgoApiFormatter<SignedTransaction<T>>
        where T : struct, ITransaction, IEquatable<T>
    {
        public SignedTransaction<T> Deserialize(ref JsonReader reader)
        {
            var raw = AlgoApiFormatterCache<SignedTransaction>.Formatter.Deserialize(ref reader);
            SignedTransaction<T> result = default;
            result.CopyFrom(in raw);
            return result;
        }

        public SignedTransaction<T> Deserialize(ref MessagePackReader reader)
        {
            var raw = AlgoApiFormatterCache<SignedTransaction>.Formatter.Deserialize(ref reader);
            SignedTransaction<T> result = default;
            result.CopyFrom(in raw);
            return result;
        }

        public void Serialize(ref JsonWriter writer, SignedTransaction<T> value)
        {
            SignedTransaction raw = default;
            value.CopyTo(ref raw);
            AlgoApiFormatterCache<SignedTransaction>.Formatter.Serialize(ref writer, raw);
        }

        public void Serialize(ref MessagePackWriter writer, SignedTransaction<T> value)
        {
            SignedTransaction raw = default;
            value.CopyTo(ref raw);
            AlgoApiFormatterCache<SignedTransaction>.Formatter.Serialize(ref writer, raw);
        }
    }
}
