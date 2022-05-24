using Unity.Collections;

namespace AlgoSdk.Abi
{
    public readonly struct Array<T>
        : IArgEnumerator<Array<T>>
        , IAbiValue
        where T : IAbiValue
    {
        readonly T[] value;
        readonly int current;

        public Array(T[] value)
        {
            this.value = value ?? throw new System.ArgumentNullException(nameof(value));
            this.current = 0;
        }

        Array(T[] value, int current)
        {
            this.value = value ?? throw new System.ArgumentNullException(nameof(value));
            this.current = current;
        }

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

            var nestedTypes = new AbiType[Count];
            for (var i = 0; i < Count; i++)
                nestedTypes[i] = elementType;
            return Tuple.Of(this)
                .Encode(AbiType.Tuple(nestedTypes), allocator);
        }

        public NativeArray<byte> EncodeCurrent(AbiType type, Allocator allocator)
        {
            return value[current].Encode(type, allocator);
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
                length += value[i].Length(elementType) + 2;
            }
            return length;
        }

        public int LengthOfCurrent(AbiType type)
        {
            return value[current].Length(type);
        }

        public bool TryNext(out Array<T> next)
        {
            if (current + 1 >= Count)
            {
                next = this;
                return false;
            }
            next = new Array<T>(value, current + 1);
            return true;
        }

        public bool TryPrev(out Array<T> prev)
        {
            if (current < 1)
            {
                prev = this;
                return false;
            }
            prev = new Array<T>(value, current - 1);
            return true;
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
