using Unity.Collections;

namespace AlgoSdk.Experimental.Abi
{
    public struct String : IAbiValue
    {
        private readonly Array<UInt8> backingValue;

        public String(Array<UInt8> backingValue)
        {
            this.backingValue = backingValue;
        }

        public EncodedAbiArg Encode(IAbiType type, AbiReferences references, Allocator allocator)
        {
            return backingValue.Encode(type, references, allocator);
        }

        public int Length(IAbiType type)
        {
            return backingValue.Length(type);
        }

        public override string ToString()
        {
            return backingValue.GetUtf8String();
        }

        public static explicit operator String(Tuple<ArgsArray> tuple)
        {
            var argValues = tuple.Args.Values;
            var values = new UInt8[argValues.Length];
            for (var i = 0; i < values.Length; i++)
            {
                if (argValues[i] is not UInt8 uint8)
                    throw new System.ArgumentException("Tuple is not tuple of uint8", nameof(tuple));

                values[i] = uint8;
            }
            return new String(new Array<UInt8>(values));
        }

        public static explicit operator String(Array<UInt8> arr)
        {
            return new String(arr);
        }
    }
}
