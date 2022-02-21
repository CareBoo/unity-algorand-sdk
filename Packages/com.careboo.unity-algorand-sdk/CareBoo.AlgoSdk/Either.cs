namespace AlgoSdk
{
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
    }
}
