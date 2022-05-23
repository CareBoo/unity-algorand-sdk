namespace AlgoSdk.Abi
{
    public ref struct EncodedAbiArg
    {
        readonly unsafe byte* buffer;

        readonly int length;

        public EncodedAbiArg(int length)
        {
            this.length = length;
            unsafe
            {
                var b = stackalloc byte[length];
                buffer = b;
            }
        }

        public byte this[int i]
        {
            get
            {
                if (i < 0 || i >= length)
                    throw new System.ArgumentOutOfRangeException(nameof(i));

                unsafe
                {
                    return buffer[i];
                }
            }
            set
            {
                if (i < 0 || i >= length)
                    throw new System.ArgumentOutOfRangeException(nameof(i));

                unsafe
                {
                    buffer[i] = value;
                }
            }
        }

        public int Length => length;
    }
}
