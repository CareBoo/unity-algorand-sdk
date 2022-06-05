using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;

namespace AlgoSdk.Abi
{
    /// <summary>
    /// Stores values that can be encoded as a tuple in an ABI method call.
    /// </summary>
    public readonly struct Tuple<T>
        : IAbiValue
        where T : IArgEnumerator<T>
    {
        readonly T args;

        public Tuple(T args)
        {
            this.args = args;
        }

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
            using var encoder = new Encoder<U>(args, types, references, Allocator.Persistent);
            return encoder.Encode(allocator);
        }

        void CheckType(IAbiType type)
        {
            if (type.ValueType != AbiValueType.Tuple)
                throw new System.ArgumentException($"Cannot cast tuple to type {type.ValueType}", nameof(type));

            if (type.NestedTypes == null || type.NestedTypes.Length == 0)
                throw new System.ArgumentException($"Cannot encode a tuple type without any nested types.", nameof(type));
        }

        struct Encoder<U>
            : INativeDisposable
            where U : IReadOnlyList<IAbiType>
        {
            T args;

            U types;

            AbiReferences references;

            NativeList<byte> headBytes;

            NativeList<byte> tailBytes;

            EncodedAbiArg result;

            byte boolShift;

            public Encoder(T args, U types, AbiReferences references, Allocator allocator)
            {
                this.args = args;
                this.types = types;
                this.references = references;
                this.headBytes = new NativeList<byte>(allocator);
                this.tailBytes = new NativeList<byte>(allocator);
                this.result = new EncodedAbiArg(allocator);
                this.boolShift = 0;
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

            int Encode()
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

            void EncodeHead(int t)
            {
                if (types[t].IsStatic)
                    EncodeStaticHead(t);
                else
                    EncodeDynamicHead(t);
            }

            void EncodeStaticHead(int t)
            {
                if (types[t].Name == "bool")
                {
                    EncodeBoolHead(t);
                }
                else
                {
                    boolShift = 0;
                    using var bytes = args.EncodeCurrent(types[t], references, Allocator.Persistent);
                    headBytes.AddRange(bytes.Bytes);
                }
            }

            void EncodeDynamicHead(int t)
            {
                boolShift = 0;
                var offset = headBytes.Length + tailBytes.Length;
                var args = this.args;
                do
                {
                    var type = types[t];
                    offset += type.IsStatic
                        ? args.LengthOfCurrent(types[t])
                        : 2
                        ;
                    t++;
                } while (args.TryNext(out args));
            }

            void EncodeBoolHead(int t)
            {
                boolShift %= 8;
                if (boolShift == 0)
                    headBytes.Add(0);
                using var encoded = args.EncodeCurrent(types[t], references, Allocator.Persistent);
                headBytes[headBytes.Length - 1] |= (byte)(encoded[0] >> boolShift);
                boolShift++;
            }

            void EncodeTail(int t)
            {
                if (types[t].IsStatic)
                    EncodeStaticTail(t);
                else
                    EncodeDynamicTail(t);
            }

            void EncodeStaticTail(int t)
            {
            }

            void EncodeDynamicTail(int t)
            {
                using var bytes = args.EncodeCurrent(types[t], references, Allocator.Persistent);
                tailBytes.AddRange(bytes.Bytes);
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
    }
}
