namespace Algorand.Unity
{
    [AlgoApiFormatter(typeof(EitherFormatter<,>))]
    public partial struct Either<T, U>
    {
        public T Value1 { get; private set; }

        public U Value2 { get; private set; }

        public bool IsValue1 { get; private set; }

        public bool IsValue2 { get; private set; }

        public Either(T value)
        {
            Value1 = value;
            Value2 = default;
            IsValue1 = true;
            IsValue2 = false;
        }

        public Either(U value)
        {
            Value1 = default;
            Value2 = value;
            IsValue1 = false;
            IsValue2 = true;
        }

        public bool TryGetValue1(out T value)
        {
            value = Value1;
            return IsValue1;
        }

        public bool TryGetValue2(out U value)
        {
            value = Value2;
            return IsValue2;
        }

        public void Deconstruct(out T value1, out U value2)
        {
            value1 = Value1;
            value2 = Value2;
        }

        public static implicit operator Either<U, T>(Either<T, U> either)
        {
            if (either.IsValue1)
                return new Either<U, T>(either.Value1);
            if (either.IsValue2)
                return new Either<U, T>(either.Value2);
            return default;
        }

        public static implicit operator Either<T, U>(T value1)
        {
            return new Either<T, U>(value1);
        }

        public static implicit operator Either<T, U>(U value2)
        {
            return new Either<T, U>(value2);
        }
    }
}
