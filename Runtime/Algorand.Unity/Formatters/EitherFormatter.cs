using System;
using Algorand.Unity.Json;
using Algorand.Unity.MessagePack;

namespace Algorand.Unity
{
    public sealed class EitherFormatter<T, U> : IAlgoApiFormatter<Either<T, U>>
    {
        public Either<T, U> Deserialize(ref JsonReader reader)
        {
            if (TryDeserialize(ref reader, out T value1, out Exception ex1))
                return new Either<T, U>(value1);

            if (TryDeserialize(ref reader, out U value2, out Exception ex2))
                return new Either<T, U>(value2);

            throw new AggregateException($"Could not deserialize either {typeof(T)} or {typeof(U)}", ex1, ex2);
        }

        public Either<T, U> Deserialize(ref MessagePackReader reader)
        {
            if (TryDeserialize(ref reader, out T value1, out Exception ex1))
                return new Either<T, U>(value1);

            if (TryDeserialize(ref reader, out U value2, out Exception ex2))
                return new Either<T, U>(value2);

            throw new AggregateException($"Could not deserialize either {typeof(T)} or {typeof(U)}", ex1, ex2);
        }

        public void Serialize(ref JsonWriter writer, Either<T, U> value)
        {
            if (value.IsValue1)
                AlgoApiFormatterCache<T>.Formatter.Serialize(ref writer, value.Value1);
            else if (value.IsValue2)
                AlgoApiFormatterCache<U>.Formatter.Serialize(ref writer, value.Value2);
            else
                writer.WriteNull();
        }

        public void Serialize(ref MessagePackWriter writer, Either<T, U> value)
        {
            if (value.IsValue1)
                AlgoApiFormatterCache<T>.Formatter.Serialize(ref writer, value.Value1);
            else if (value.IsValue2)
                AlgoApiFormatterCache<U>.Formatter.Serialize(ref writer, value.Value2);
            else
                writer.WriteNil();
        }

        private bool TryDeserialize<TValue>(ref JsonReader reader, out TValue value, out Exception exception)
        {
            var startingPosition = reader.Position;
            try
            {
                value = AlgoApiFormatterCache<TValue>.Formatter.Deserialize(ref reader);
                exception = default;
                return true;
            }
            catch (Exception ex)
            {
                reader.Position = startingPosition;
                value = default;
                exception = ex;
                return false;
            }
        }

        private bool TryDeserialize<TValue>(ref MessagePackReader reader, out TValue value, out Exception exception)
        {
            var startingPosition = reader.Position;
            try
            {
                value = AlgoApiFormatterCache<TValue>.Formatter.Deserialize(ref reader);
                exception = default;
                return true;
            }
            catch (Exception ex)
            {
                reader.Position = startingPosition;
                value = default;
                exception = ex;
                return false;
            }
        }
    }
}
