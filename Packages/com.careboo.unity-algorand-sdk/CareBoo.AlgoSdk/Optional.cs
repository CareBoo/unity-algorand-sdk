using System;
using AlgoSdk.Formatters;

namespace AlgoSdk
{
    [AlgoApiFormatter(typeof(OptionalFormatter<bool>))]
    [AlgoApiFormatter(typeof(OptionalFormatter<ulong>))]
    [AlgoApiFormatter(typeof(OptionalFormatter<uint>))]
    [Serializable]
    public struct Optional<T>
        : IEquatable<Optional<T>>
        where T : struct, IEquatable<T>
    {
        public static Optional<T> Empty => default;

        public T Value;
        public bool HasValue;

        public Optional(T value)
        {
            Value = value;
            HasValue = true;
        }

        public bool Equals(Optional<T> other)
        {
            return HasValue == other.HasValue
                && Value.Equals(other.Value);
        }

        public static implicit operator Optional<T>(T value)
        {
            return new Optional<T>(value);
        }

        public static implicit operator T(Optional<T> optional)
        {
            return optional.HasValue
                ? optional.Value
                : default
                ;
        }
    }
}
