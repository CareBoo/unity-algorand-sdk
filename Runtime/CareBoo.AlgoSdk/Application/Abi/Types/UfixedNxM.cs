using System.Numerics;
using Unity.Collections;
using Unity.Mathematics;

namespace AlgoSdk.Abi
{
    public interface IUfixedNxM : IAbiValue
    {
        ushort N { get; }
        byte M { get; }
    }

    public readonly struct UfixedNxM : IUfixedNxM
    {
        readonly UIntN value;

        readonly byte precision;

        public UfixedNxM(UIntN value, byte precision)
        {
            this.value = value;
            this.precision = precision;
        }

        public ushort N => value.N;

        public byte M => precision;

        public BigInteger Value => value.Value;

        public NativeArray<byte> Encode(AbiType type, Allocator allocator)
        {
            return As(type)
                .value
                .Encode(AbiType.UIntN(type.N), allocator);
        }

        public int Length(AbiType type)
        {
            return type.StaticLength;
        }

        public UfixedNxM As(AbiType type)
        {
            CheckType(type);

            var diff = type.M - precision;
            var newVal = diff >= 0
                ? value.Value * (int)(math.pow(10, diff))
                : value.Value / (int)(math.pow(10, -diff))
                ;
            return new UfixedNxM(new UIntN(newVal), (byte)type.M);
        }

        void CheckType(AbiType type)
        {
            if (type.ValueType != AbiValueType.UFixedNxM)
                throw new System.ArgumentException($"Cannot encode UFixed{N}x{M} to ${type.Name}", nameof(type));

            if (type.N < N)
                throw new System.ArgumentException($"Not enough bits in {type.Name} to fit this UFixed{N}x{M}", nameof(type));
        }
    }
}
