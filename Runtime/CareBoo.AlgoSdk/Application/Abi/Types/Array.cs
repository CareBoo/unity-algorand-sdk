using System.Collections;
using System.Collections.Generic;
using AlgoSdk.LowLevel;
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
            var nestedTypes = new Repeated<AbiType>(value.Length, elementType);
            var tupleEncoding = Tuple.Of(this).Encode(nestedTypes, allocator);
            if (type.IsFixedArray)
                return tupleEncoding;

            var result = new NativeList<byte>(allocator);
            using var k = new UInt16((ushort)value.Length).Encode(AbiType.UIntN(16), Allocator.Temp);
            result.AddRange(k);
            result.AddRange(tupleEncoding);
            tupleEncoding.Dispose();
            return result;
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

    public static partial class Args
    {
        public static SingleArg<Array<T>> Add<T>(T[] x) where T : IAbiValue => Args.Add(new Array<T>(x));

        public static ArgsList<Array<T>, U> Add<T, U>(this U tail, T[] x)
            where T : IAbiValue
            where U : struct, IArgEnumerator<U>
        {
            return new ArgsList<Array<T>, U>(new Array<T>(x), tail);
        }
    }
}
