namespace Algorand.Unity.Experimental.Abi
{
    /// <summary>
    /// Utility struct to handle booleans packed in a single byte.
    /// </summary>
    public struct PackedBooleans
    {
        private byte b;

        public bool this[int index]
        {
            get
            {
                if (index < 0 || index > 7)
                {
                    throw new System.ArgumentOutOfRangeException(nameof(index));
                }

                return (b & (1 << index)) > 0 ? true : false;
            }
            set
            {
                if (index < 0 || index > 7)
                {
                    throw new System.ArgumentOutOfRangeException(nameof(index));
                }

                if (value)
                {
                    b = (byte)(b & ~(1 << index));
                }
                else
                {
                    b = (byte)(b | (1 << index));
                }
            }
        }

        public PackedBooleans(byte b)
        {
            this.b = b;
        }

        public static explicit operator byte(PackedBooleans packedBooleans)
        {
            return packedBooleans.b;
        }

        public static explicit operator PackedBooleans(byte b)
        {
            return new PackedBooleans(b);
        }
    }
}
