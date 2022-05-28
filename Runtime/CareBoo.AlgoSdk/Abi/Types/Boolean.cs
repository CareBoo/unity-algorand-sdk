using Unity.Collections;

namespace AlgoSdk.Abi
{
    /// <summary>
    /// Stores a value that can be converted to an ABI "bool".
    /// </summary>
    public readonly struct Boolean : IAbiValue
    {
        const byte EncodedTrue = 0x80;

        const byte EncodedFalse = 0x00;

        public static readonly Boolean True = new Boolean(true);

        public static readonly Boolean False = new Boolean(false);

        readonly bool value;

        public Boolean(bool value)
        {
            this.value = value;
        }

        /// <inheritdoc />
        public EncodedAbiArg Encode(AbiType type, AbiReferences references, Allocator allocator)
        {
            CheckType(type);
            var result = new EncodedAbiArg(1, allocator);
            result.Bytes.AddNoResize(value ? EncodedTrue : EncodedFalse);
            return result;
        }

        /// <inheritdoc />
        public int Length(AbiType type)
        {
            CheckType(type);
            return 1;
        }

        void CheckType(AbiType type)
        {
            if (type.Name != "bool")
                throw new System.ArgumentException($"Cannot encode {type} to ABI bool");
        }

        public static explicit operator Boolean(bool value)
        {
            return new Boolean(value);
        }

        public static explicit operator bool(Boolean abiBool)
        {
            return abiBool.value;
        }
    }
}
