using System;
using Unity.Collections;
using Unity.Jobs;

namespace AlgoSdk.Abi
{
    public readonly struct Tuple<T>
        : IAbiType
        where T : IArgEnumerator<T>
    {
        readonly T args;

        public Tuple(T args)
        {
            this.args = args;
        }

        public bool IsStatic
        {
            get
            {
                var arg = args;
                do
                {
                    if (!arg.IsStatic)
                        return false;
                } while (arg.TryNext(out arg));
                return true;
            }
        }

        public string AbiTypeName
        {
            get
            {
                var text = new NativeText(Allocator.Temp);
                try
                {
                    text.Append("(");
                    var arg = args;
                    text.Append(arg.AbiTypeName);
                    while (arg.TryNext(out arg))
                    {
                        text.Append(",");
                        text.Append(arg.AbiTypeName);
                    }
                    text.Append(")");
                    return text.ToString();
                }
                finally
                {
                    text.Dispose();
                }
            }
        }

        public NativeArray<byte> Encode(Method.Arg definition, Allocator allocator)
        {
            var types = definition.Type.Trim('(', ')').Split(',');
            var definitions = new Method.Arg[types.Length];
            for (var i = 0; i < definitions.Length; i++)
                definitions[i] = new Method.Arg { Type = types[i] };
            return Encode(definitions, allocator);
        }

        public int Length(Method.Arg definition)
        {
            using var encoded = Encode(definition, Allocator.Temp);
            return encoded.Length;
        }

        public NativeArray<byte> Encode(ArraySegment<Method.Arg> definitions, Allocator allocator)
        {
            using var encoder = new Encoder(args, definitions, Allocator.Temp);
            return encoder.ToArray(allocator);
        }

        public struct Encoder
            : INativeDisposable
        {
            T args;

            ArraySegment<Method.Arg> definitions;

            NativeList<byte> headBytes;

            NativeList<byte> tailBytes;

            byte boolShift;

            public Encoder(T args, ArraySegment<Method.Arg> definitions, Allocator allocator)
            {
                this.args = args;
                this.definitions = definitions;
                this.headBytes = new NativeList<byte>(allocator);
                this.tailBytes = new NativeList<byte>(allocator);
                this.boolShift = 0;
            }

            public NativeArray<byte> ToArray(Allocator allocator)
            {
                var length = headBytes.Length + tailBytes.Length;
                if (length == 0)
                {
                    length = Encode();
                }
                var result = new NativeArray<byte>(length, allocator);
                for (var i = 0; i < headBytes.Length; i++)
                    result[i] = headBytes[i];
                for (var i = 0; i < tailBytes.Length; i++)
                    result[i + headBytes.Length] = tailBytes[i];
                return result;
            }

            int Encode()
            {
                for (var i = 0; i < definitions.Count; i++)
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

            void EncodeHead(int d)
            {
                if (args.IsStatic)
                    EncodeStaticHead(d);
                else
                    EncodeDynamicHead(d);
            }

            void EncodeStaticHead(int d)
            {
                if (definitions[d].Type == "bool")
                {
                    EncodeBoolHead(d);
                }
                else
                {
                    boolShift = 0;
                    using var bytes = args.Encode(definitions[d], Allocator.Temp);
                    headBytes.AddRange(bytes);
                }
            }

            void EncodeDynamicHead(int d)
            {
                boolShift = 0;
                var offset = headBytes.Length + tailBytes.Length;
                var arg = args;
                do
                {
                    offset += arg.IsStatic
                        ? arg.Length(definitions[d])
                        : 2
                        ;
                    d++;
                } while (arg.TryNext(out arg));
            }

            void EncodeBoolHead(int d)
            {
                boolShift %= 8;
                if (boolShift == 0)
                    headBytes.Add(0);
                using var encoded = args.Encode(definitions[d], Allocator.Temp);
                headBytes[headBytes.Length - 1] |= (byte)(encoded[0] >> boolShift);
                boolShift++;
            }

            void EncodeTail(int d)
            {
                if (args.IsStatic)
                    EncodeStaticTail(d);
                else
                    EncodeDynamicTail(d);
            }

            void EncodeStaticTail(int d)
            {
            }

            void EncodeDynamicTail(int d)
            {
                using var bytes = args.Encode(definitions[d], Allocator.Temp);
                tailBytes.AddRange(bytes);
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
