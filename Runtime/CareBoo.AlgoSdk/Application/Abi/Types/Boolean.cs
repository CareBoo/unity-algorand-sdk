using Unity.Collections;

namespace AlgoSdk.Abi
{
    public readonly struct Boolean : IAbiType
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

        public bool IsStatic => true;

        public string AbiTypeName => "bool";

        public NativeArray<byte> Encode(Method.Arg definition, Allocator allocator)
        {
            CheckType(definition);
            var result = new NativeArray<byte>(1, allocator);
            result[0] = value ? EncodedTrue : EncodedFalse;
            return result;
        }

        public int Length(Method.Arg definition)
        {
            CheckType(definition);
            return 1;
        }

        void CheckType(Method.Arg definition)
        {
            if (definition.Type != "bool")
                throw new System.ArgumentException($"Cannot encode {definition.Type} to ABI bool");
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
