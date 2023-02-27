using Unity.Collections;

namespace Algorand.Unity.Experimental.Abi
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
                if (argValues[i] is UInt8 uint8)
                    values[i] = uint8;
                else
                    throw new System.ArgumentException("Tuple is not tuple of uint8", nameof(tuple));
            }
            return new String(new Array<UInt8>(values));
        }

        public static explicit operator String(Array<UInt8> arr)
        {
            return new String(arr);
        }
    }
}
