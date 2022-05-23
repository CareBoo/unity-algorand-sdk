using System.Numerics;
using System.Text.RegularExpressions;
using Unity.Collections;
using Unity.Mathematics;

namespace AlgoSdk.Abi
{
    public interface IUfixedNxM : IAbiType
    {
        ushort N { get; }
        byte M { get; }
    }

    public readonly struct UfixedNxM : IUfixedNxM
    {
        public static readonly Regex Pattern = new Regex(
            @"^ufixed(?<N>\d{1,3})x(?<M>\d{1,3})$"
        );

        readonly UintN value;

        readonly byte precision;

        public UfixedNxM(UintN value, byte precision)
        {
            this.value = value;
            this.precision = precision;
        }

        public bool IsStatic => true;

        public string AbiTypeName => $"ufixed{N}x{M}";

        public ushort N => value.N;

        public byte M => precision;

        public BigInteger Value => value.Value;

        public NativeArray<byte> Encode(Method.Arg definition, Allocator allocator)
        {
            var type = definition.Type;
            return As(type, out var n, out var m)
                .value
                .EncodeAs(n, allocator);
        }

        public int Length(Method.Arg definition)
        {
            var type = definition.Type;
            return As(type, out var n, out var m)
                .value
                .EncodedLengthAs(n);
        }

        public UfixedNxM As(string type, out ushort n, out byte m)
        {
            if (!TryParseType(type, out n, out m))
                throw new System.ArgumentException($"{type} is not ufixedNxM", nameof(type));

            return As(n, m);
        }

        public UfixedNxM As(ushort n, byte m)
        {
            var diff = m - precision;
            var newVal = diff >= 0
                ? value.Value * (int)(math.pow(10, diff))
                : value.Value / (int)(math.pow(10, -diff))
                ;
            return new UfixedNxM(new UintN(newVal), m);
        }

        bool TryParseType(string type, out ushort n, out byte m)
        {
            n = default;
            m = default;
            var match = Pattern.Match(type);
            return match.Success
                && ushort.TryParse(match.Groups["N"].Value, out n)
                && byte.TryParse(match.Groups["M"].Value, out m)
                ;
        }
    }
}
