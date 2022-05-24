using Unity.Collections;

namespace AlgoSdk.Abi
{
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

        public NativeArray<byte> Encode(AbiType type, Allocator allocator)
        {
            CheckType(type);
            var result = new NativeArray<byte>(1, allocator);
            result[0] = value ? EncodedTrue : EncodedFalse;
            return result;
        }

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
