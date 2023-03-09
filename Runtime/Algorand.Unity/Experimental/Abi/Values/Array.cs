using Algorand.Unity.LowLevel;
using Unity.Collections;

namespace Algorand.Unity.Experimental.Abi
{
    /// <summary>
    /// Stores data that can be converted to an array in an ABI Method Call.
    /// </summary>
    public readonly struct Array<T>
        : IArgEnumerator<Array<T>>
        , IAbiValue
        where T : IAbiValue
    {
        private readonly T[] value;
        private readonly int current;

        public Array(T[] value)
        {
            this.value = value ?? throw new System.ArgumentNullException(nameof(value));
            this.current = 0;
        }

        private Array(T[] value, int current)
        {
            this.value = value ?? throw new System.ArgumentNullException(nameof(value));
            this.current = current;
        }

        public T[] Value => value;

        public int Count => value?.Length ?? 0;

        /// <inheritdoc />
        public EncodedAbiArg Encode(IAbiType type, AbiReferences references, Allocator allocator)
        {
            CheckType(type);

            var elementType = type.NestedTypes[0];
            var nestedTypes = new Repeated<IAbiType>(value.Length, elementType);
            var tupleEncoding = Tuple.Of(this).Encode(nestedTypes, references, allocator);
            if (type.IsFixedArray)
                return tupleEncoding;

            try
            {
                var result = new EncodedAbiArg(allocator);
                using var k = new UInt16((ushort)value.Length)
                    .Encode(AbiType.UIntN(16), references, Allocator.Temp);
                result.Bytes.AddRange(k.Bytes.AsArray());
                result.Bytes.AddRange(tupleEncoding.Bytes.AsArray());
                return result;
            }
            finally
            {
                tupleEncoding.Dispose();
            }
        }

        /// <inheritdoc />
        public EncodedAbiArg EncodeCurrent(IAbiType type, AbiReferences references, Allocator allocator)
        {
            return value[current].Encode(type, references, allocator);
        }

        /// <inheritdoc />
        public int Length(IAbiType type)
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

        /// <inheritdoc />
        public int LengthOfCurrent(IAbiType type)
        {
            return value[current].Length(type);
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
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

        public override string ToString()
        {
            return $"{typeof(T).FullName}[{Count}]";
        }

        private void CheckType(IAbiType type)
        {
            switch (type.ValueType)
            {
                case AbiValueType.Array when type.IsReference():
                    throw new System.NotSupportedException($"{nameof(Array<T>)}<{nameof(T)}> cannot encode as account reference. Use {nameof(AbiAddress)} instead.");
                case AbiValueType.Array when type.IsStatic && type.N != Count:
                    throw new System.ArgumentException($"Cannot encode array of length {value.Length} as an array of length {type.N}", nameof(type));
                case AbiValueType.Array when type.NestedTypes == null || type.NestedTypes.Length != 1:
                    throw new System.ArgumentException($"Given array type does not have an element type.", nameof(type));
                case AbiValueType.Array:
                    break;
                default:
                    throw new System.ArgumentException($"Cannot encode array as type {type.ValueType}", nameof(type));
            }
        }
    }

    public static class ArrayExtensions
    {
        public static string GetUtf8String(this Array<UInt8> arr)
        {
            var bytes = new byte[arr.Count];
            for (var i = 0; i < bytes.Length; i++)
            {
                bytes[i] = arr.Value[i].Value;
            }
            var text = System.Text.Encoding.UTF8.GetString(bytes);
            return text;
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
