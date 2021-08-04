namespace AlgoSdk.MsgPack
{
    public struct Prop<T>
    {
        public T Value { get; private set; }
        public bool IsCreated { get; private set; }

        public Prop(T value)
        {
            this.Value = value;
            IsCreated = true;
        }

        public T Get()
        {
            return Value;
        }

        public void Set(T value)
        {
            IsCreated = true;
            Value = value;
        }

        public static implicit operator T(Prop<T> prop)
        {
            return prop.Value;
        }

        public static implicit operator Prop<T>(T value)
        {
            return new Prop<T>(value);
        }
    }
}
