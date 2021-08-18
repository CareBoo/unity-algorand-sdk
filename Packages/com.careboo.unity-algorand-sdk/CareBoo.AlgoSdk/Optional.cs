using System;

namespace AlgoSdk
{
    public struct Optional<T>
        : IEquatable<Optional<T>>
        where T : struct, IEquatable<T>
    {
        public readonly T Value;
        public readonly bool HasValue;

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
    }
}
