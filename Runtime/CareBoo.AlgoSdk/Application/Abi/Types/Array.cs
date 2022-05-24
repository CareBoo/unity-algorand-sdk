using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace AlgoSdk.Abi
{
    public readonly struct Array<T>
        : IAbiValue
        where T : IAbiValue
    {
        readonly T[] value;

        public int Count => value?.Length ?? 0;

        public NativeArray<byte> Encode(AbiType type, Allocator allocator)
        {
            CheckType(type);
            var elementType = type.NestedTypes[0];
            if (type.IsStatic)
            {
                var result = new NativeArray<byte>(type.StaticLength, allocator);
                var writeResult = result;
                for (var i = 0; i < Count; i++)
                {
                    using var childEncoded = value[i].Encode(elementType, Allocator.Temp);
                    var offset = childEncoded.Length * i;
                    for (var j = 0; j < childEncoded.Length; j++)
                    {
                        result[offset + j] = childEncoded[j];
                    }
                }
                return result;
            }

            var resultList = new NativeList<byte>(allocator);
            for (var i = 0; i < Count; i++)
            {
                using var childEncoded = value[i].Encode(elementType, Allocator.Temp);
                resultList.AddRange(childEncoded);
            }
            return resultList.AsArray();
        }

        public int Length(AbiType type)
        {
            CheckType(type);
            if (type.IsStatic)
                return type.StaticLength;

            var elementType = type.ElementType;
            var length = 0;
            for (var i = 0; i < Count; i++)
            {
                length += value[i].Length(elementType);
            }
            return length;
        }

        void CheckType(AbiType type)
        {
            if (type.ValueType != AbiValueType.Array)
            {
                throw new System.ArgumentException($"Cannot encode array as type {type.ValueType}", nameof(type));
            }

            if (type.IsStatic && type.N != Count)
            {
                throw new System.ArgumentException($"Cannot encode array of length {value.Length} as an array of length {type.N}", nameof(type));
            }

            if (type.NestedTypes == null || type.NestedTypes.Length != 1)
            {
                throw new System.ArgumentException($"Given array type does not have an element type.", nameof(type));
            }
        }
    }
}
