namespace AlgoSdk
{
    public ref struct Optional<T>
        where T : struct
    {
        public readonly T Value;
        public readonly bool HasValue;

        public Optional(T value)
        {
            Value = value;
            HasValue = true;
        }
    }
}
