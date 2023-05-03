using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;

namespace Algorand.Unity.Experimental.Abi
{
    /// <summary>
    /// Stores values that can be encoded as a tuple in an ABI method call.
    /// </summary>
    public readonly struct Tuple<T>
        : IAbiValue
        where T : IArgEnumerator<T>
    {
        private readonly T args;

        public Tuple(T args)
        {
            this.args = args;
        }

        public T Args => args;

        /// <inheritdoc />
        public EncodedAbiArg Encode(IAbiType type, AbiReferences references, Allocator allocator)
        {
            CheckType(type);
            return Encode(type.NestedTypes, references, allocator);
        }

        /// <inheritdoc />
        public int Length(IAbiType type)
        {
            CheckType(type);
            if (type.IsStatic)
                return type.StaticLength;

            var args = this.args;
            var length = 0;
            for (var i = 0; i < type.NestedTypes.Length; i++)
            {
                if (i > 0 && !args.TryNext(out args))
                    throw new System.ArgumentException($"Not enough values in tuple to encode {type.Name}", nameof(type));

                var argType = type.NestedTypes[i];
                if (argType.IsStatic)
                    length += argType.StaticLength;
                else
                    length += args.LengthOfCurrent(argType) + 2;
            }
            return length;
        }

        public EncodedAbiArg Encode<U>(U types, AbiReferences references, Allocator allocator)
            where U : IReadOnlyList<IAbiType>
        {
            using var encoder = new Encoder<U>(args, types, references, Allocator.Temp);
            return encoder.Encode(allocator);
        }

        public override string ToString()
        {
            var text = new NativeText(Allocator.Temp);
            try
            {
                text.Append((Unicode.Rune)'(');
                var next = this.args;
                text.Append(next.ToString());
                while (next.TryNext(out next))
                {
                    text.Append(", ");
                    text.Append(next.ToString());
                }
                text.Append((Unicode.Rune)')');
                return text.ToString();
            }
            finally
            {
                text.Dispose();
            }
        }

        private void CheckType(IAbiType type)
        {
            if (type.ValueType != AbiValueType.Tuple)
                throw new System.ArgumentException($"Cannot cast tuple to type {type.ValueType}", nameof(type));

            if (type.NestedTypes == null || type.NestedTypes.Length == 0)
                throw new System.ArgumentException($"Cannot encode a tuple type without any nested types.", nameof(type));
        }

        private struct Encoder<U>
            : INativeDisposable
            where U : IReadOnlyList<IAbiType>
        {
            private T args;

            private U types;

            private AbiReferences references;

            private NativeList<byte> headBytes;

            private NativeList<byte> tailBytes;

            private EncodedAbiArg result;

            private byte boolShift;

            private Optional<int> headBytesTotalLength;

            public Encoder(T args, U types, AbiReferences references, Allocator allocator)
            {
                this.args = args;
                this.types = types;
                this.references = references;
                this.headBytes = new NativeList<byte>(allocator);
                this.tailBytes = new NativeList<byte>(allocator);
                this.result = new EncodedAbiArg(allocator);
                this.boolShift = 0;
                this.headBytesTotalLength = default;
            }

            public EncodedAbiArg Encode(Allocator allocator)
            {
                var length = headBytes.Length + tailBytes.Length;
                if (length == 0)
                {
                    length = Encode();
                }
                result.Length = length;
                for (var i = 0; i < headBytes.Length; i++)
                    result.ElementAt(i) = headBytes[i];
                for (var i = 0; i < tailBytes.Length; i++)
                    result.ElementAt(i + headBytes.Length) = tailBytes[i];
                return result;
            }

            private int Encode()
            {
                for (var i = 0; i < types.Count; i++)
                {
                    if ((i > 0) && !args.TryNext(out args))
                        throw new System.InvalidOperationException($"Not enough arguments given to this tuple.");

                    EncodeHead(i);
                    EncodeTail(i);
                }
                return headBytes.Length + tailBytes.Length;
            }

            public JobHandle Dispose(JobHandle inputDeps)
            {
                return JobHandle.CombineDependencies(
                    headBytes.Dispose(inputDeps),
                    tailBytes.Dispose(inputDeps)
                );
            }

            public void Dispose()
            {
                headBytes.Dispose();
                tailBytes.Dispose();
            }

            private void EncodeHead(int t)
            {
                if (types[t].IsStatic)
                    EncodeStaticHead(t);
                else
                    EncodeDynamicHead(t);
            }

            private void EncodeStaticHead(int t)
            {
                if (types[t].Name == "bool")
                {
                    EncodeBoolHead(t);
                }
                else
                {
                    boolShift = 0;
                    using var bytes = args.EncodeCurrent(types[t], references, Allocator.Temp);
                    headBytes.AddRange(bytes.Bytes.AsArray());
                }
            }

            private void EncodeDynamicHead(int t)
            {
                if (!headBytesTotalLength.HasValue)
                {
                    headBytesTotalLength = CountHeadBytesLength(t);
                }
                var ptr = headBytesTotalLength.Value + tailBytes.Length;
                var offset = headBytes.Length;
                headBytes.Length += 2;
                Endianness.CopyToNativeBytesBigEndian((ushort)ptr, ref headBytes, offset);
            }

            private void EncodeBoolHead(int t)
            {
                boolShift %= 8;
                if (boolShift == 0)
                    headBytes.Add(0);
                using var encoded = args.EncodeCurrent(types[t], references, Allocator.Temp);
                headBytes[headBytes.Length - 1] |= (byte)(encoded[0] >> boolShift);
                boolShift++;
            }

            private void EncodeTail(int t)
            {
                if (types[t].IsStatic)
                    EncodeStaticTail(t);
                else
                    EncodeDynamicTail(t);
            }

            private void EncodeStaticTail(int t)
            {
            }

            private void EncodeDynamicTail(int t)
            {
                using var bytes = args.EncodeCurrent(types[t], references, Allocator.Temp);
                tailBytes.AddRange(bytes.Bytes.AsArray());
            }

            private int CountHeadBytesLength(int t)
            {
                var totalLength = headBytes.Length;
                var args = this.args;
                do
                {
                    var type = types[t];
                    if (type.Name == "bool")
                    {
                        boolShift %= 8;
                        if (boolShift == 0)
                            totalLength++;
                        boolShift++;
                    }
                    else
                    {
                        boolShift = 0;
                        totalLength += type.IsStatic
                            ? args.LengthOfCurrent(type)
                            : 2;
                    }
                    t++;
                } while (args.TryNext(out args));
                return totalLength;
            }
        }
    }

    public static class Tuple
    {
        public static Tuple<T> Of<T>(T args)
            where T : IArgEnumerator<T>
        {
            return new Tuple<T>(args);
        }

        public static Tuple<ArgsArray> Of(params IAbiValue[] args)
        {
            return new Tuple<ArgsArray>(new ArgsArray(args));
        }
    }
}
