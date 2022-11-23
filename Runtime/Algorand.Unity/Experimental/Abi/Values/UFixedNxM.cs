using System.Numerics;
using Unity.Collections;
using Unity.Mathematics;

namespace Algorand.Unity.Experimental.Abi
{

    /// <inheritdoc />
    public interface IUFixedNxM : IAbiValue
    {
        /// <summary>
        /// The number of bits available to this value.
        /// </summary>
        ushort N { get; }

        /// <summary>
        /// The precision of this value (the number of digits after the decimal place).
        /// </summary>
        byte M { get; }
    }

    /// <summary>
    /// Stores data that can be converted to a "ufixed" in an ABI method call.
    /// </summary>
    public readonly struct UFixedNxM : IUFixedNxM
    {
        private readonly UIntN value;

        private readonly byte precision;

        public UFixedNxM(UIntN value, byte precision)
        {
            this.value = value;
            this.precision = precision;
        }

        /// <inheritdoc />
        public ushort N => value.N;

        /// <inheritdoc />
        public byte M => precision;

        public BigInteger Value => value.Value;

        /// <inheritdoc />
        public EncodedAbiArg Encode(IAbiType type, AbiReferences references, Allocator allocator)
        {
            return As(type)
                .value
                .Encode(AbiType.UIntN(type.N), references, allocator);
        }

        /// <inheritdoc />
        public int Length(IAbiType type)
        {
            return type.StaticLength;
        }

        public override string ToString()
        {
            var valueStr = value.ToString();
            return precision > 0
                ? valueStr.Insert(valueStr.Length - precision, ".")
                : valueStr
                ;
        }

        public UFixedNxM As(IAbiType type)
        {
            CheckType(type);

            var diff = type.M - precision;
            var newVal = diff >= 0
                ? value.Value * (int)(math.pow(10, diff))
                : value.Value / (int)(math.pow(10, -diff))
                ;
            return new UFixedNxM(new UIntN(newVal), (byte)type.M);
        }

        private void CheckType(IAbiType type)
        {
            if (type.ValueType != AbiValueType.UFixedNxM)
                throw new System.ArgumentException($"Cannot encode UFixed{N}x{M} to ${type.Name}", nameof(type));

            if (type.N < N)
                throw new System.ArgumentException($"Not enough bits in {type.Name} to fit this UFixed{N}x{M}", nameof(type));
        }
    }
}
