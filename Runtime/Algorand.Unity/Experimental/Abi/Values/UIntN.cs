using System.Numerics;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;

namespace Algorand.Unity.Experimental.Abi
{
    /// <summary>
    /// Stores data that can be converted to a "uint" in an ABI Method call.
    /// </summary>
    public readonly struct UInt8 : IUIntN
    {
        private readonly byte value;

        public UInt8(byte value)
        {
            this.value = value;
        }

        public byte Value => value;

        /// <inheritdoc />
        public ushort N => 8;

        /// <inheritdoc />
        public EncodedAbiArg Encode(IAbiType type, AbiReferences references, Allocator allocator)
        {
            this.CheckFitsIn(type);
            if (type.IsReference())
            {
                var reference = references.Encode((ulong)value, type.ReferenceType);
                var refResult = new EncodedAbiArg(1, allocator);
                refResult.Bytes.AddNoResize(reference);
                return refResult;
            }
            var encodedLength = type.N / 8;
            var result = new EncodedAbiArg(encodedLength, allocator);
            result.Length = encodedLength;
            result[encodedLength - 1] = value;
            return result;
        }

        /// <inheritdoc />
        public int Length(IAbiType type)
        {
            this.CheckFitsIn(type);
            return type.IsReference()
                ? 1
                : type.N / 8
                ;
        }

        public override string ToString()
        {
            return value.ToString();
        }
    }

    /// <summary>
    /// Stores data that can be converted to a "uint" in an ABI Method call.
    /// </summary>
    public readonly struct UInt16 : IUIntN
    {
        private readonly ushort value;

        public UInt16(ushort value)
        {
            this.value = value;
        }

        /// <inheritdoc />
        public ushort N
        {
            get
            {
                if (value < 1 << 8)
                    return 8;
                return 16;
            }
        }

        /// <inheritdoc />
        public EncodedAbiArg Encode(IAbiType type, AbiReferences references, Allocator allocator)
        {
            this.CheckFitsIn(type);
            if (type.IsReference())
            {
                var reference = references.Encode((ulong)value, type.ReferenceType);
                var refResult = new EncodedAbiArg(1, allocator);
                refResult.Bytes.AddNoResize(reference);
                return refResult;
            }
            var encodedLength = type.N / 8;
            var result = new EncodedAbiArg(encodedLength, allocator);
            result.Length = encodedLength;
            value.CopyToNativeBytesBigEndian(ref result, encodedLength - 2);
            return result;
        }

        /// <inheritdoc />
        public int Length(IAbiType type)
        {
            this.CheckFitsIn(type);
            return type.IsReference()
                ? 1
                : type.N / 8
                ;
        }

        public override string ToString()
        {
            return value.ToString();
        }
    }

    /// <summary>
    /// Stores data that can be converted to a "uint" in an ABI Method call.
    /// </summary>
    public readonly struct UInt32 : IUIntN
    {
        private readonly uint value;

        public UInt32(uint value)
        {
            this.value = value;
        }

        /// <inheritdoc />
        public ushort N
        {
            get
            {
                var x = math.lzcnt(value);
                x += 7;
                x -= x % 8;
                return (ushort)math.max(32 - x, 8);
            }
        }

        /// <inheritdoc />
        public EncodedAbiArg Encode(IAbiType type, AbiReferences references, Allocator allocator)
        {
            this.CheckFitsIn(type);
            if (type.IsReference())
            {
                var reference = references.Encode((ulong)value, type.ReferenceType);
                var refResult = new EncodedAbiArg(1, allocator);
                refResult.Bytes.AddNoResize(reference);
                return refResult;
            }
            var encodedLength = type.N / 8;
            var result = new EncodedAbiArg(encodedLength, allocator);
            result.Length = encodedLength;
            value.CopyToNativeBytesBigEndian(ref result, encodedLength - 4);
            return result;
        }

        /// <inheritdoc />
        public int Length(IAbiType type)
        {
            this.CheckFitsIn(type);
            return type.IsReference()
                ? 1
                : type.N / 8
                ;
        }

        public override string ToString()
        {
            return value.ToString();
        }
    }

    /// <summary>
    /// Stores data that can be converted to a "uint" in an ABI Method call.
    /// </summary>
    public readonly struct UInt64 : IUIntN
    {
        private readonly ulong value;

        public UInt64(ulong value)
        {
            this.value = value;
        }

        /// <inheritdoc />
        public ushort N
        {
            get
            {
                var x = math.lzcnt(value);
                x += 7;
                x -= x % 8;
                return (ushort)math.max(64 - x, 8);
            }
        }

        /// <inheritdoc />
        public EncodedAbiArg Encode(IAbiType type, AbiReferences references, Allocator allocator)
        {
            this.CheckFitsIn(type);
            if (type.IsReference())
            {
                var reference = references.Encode((ulong)value, type.ReferenceType);
                var refResult = new EncodedAbiArg(1, allocator);
                refResult.Bytes.AddNoResize(reference);
                return refResult;
            }
            var encodedLength = type.N / 8;
            var result = new EncodedAbiArg(encodedLength, allocator);
            result.Length = encodedLength;
            value.CopyToNativeBytesBigEndian(ref result, encodedLength - 8);
            return result;
        }

        /// <inheritdoc />
        public int Length(IAbiType type)
        {
            return type.IsReference()
                ? 1
                : type.N / 8
                ;
        }

        public override string ToString()
        {
            return value.ToString();
        }
    }

    /// <summary>
    /// Stores data that can be converted to a "uint" in an ABI Method call.
    /// </summary>
    public readonly struct UIntN : IUIntN
    {
        private readonly BigInteger value;

        public UIntN(BigInteger value)
        {
            this.value = value;
        }

        public BigInteger Value => value;

        /// <inheritdoc />
        public ushort N => (ushort)(value.ToByteArray().Length * 8);

        /// <inheritdoc />
        public EncodedAbiArg Encode(IAbiType type, AbiReferences references, Allocator allocator)
        {
            this.CheckFitsIn(type);
            if (type.IsReference())
            {
                var reference = references.Encode((ulong)value, type.ReferenceType);
                var refResult = new EncodedAbiArg(1, allocator);
                refResult.Bytes.AddNoResize(reference);
                return refResult;
            }
            var encodedLength = type.N / 8;
            var result = new EncodedAbiArg(encodedLength, allocator);
            result.Length = encodedLength;
            var bytes = value.ToByteArray();
            for (var i = 0; i < bytes.Length; i++)
                result[encodedLength - 1 - i] = bytes[i];

            return result;
        }

        /// <inheritdoc />
        public int Length(IAbiType type)
        {
            return type.IsReference()
                ? 1
                : type.N / 8
                ;
        }

        public override string ToString()
        {
            return value.ToString();
        }
    }

    /// <summary>
    /// Stores data that can be converted to a "uint" in an ABI Method call.
    /// </summary>
    public interface IUIntN : IAbiValue
    {
        /// <summary>
        /// The number of bits available to this value.
        /// </summary>
        ushort N { get; }
    }

    public static class UIntNExtensions
    {
        public static void CheckFitsIn<T>(this T x, IAbiType type)
            where T : IUIntN
        {
            switch (type.ValueType)
            {
                case AbiValueType.UIntN when type.IsReference() && 64 < x.N:
                case AbiValueType.UIntN when !type.IsReference() && type.N < x.N:
                    throw new System.ArgumentException($"Not enough bits in {type.Name} to store this value.", nameof(type));
                case AbiValueType.UIntN:
                    break;
                default:
                    throw new System.ArgumentException($"Cannot encode this value to type {type.Name}.", nameof(type));
            }
        }
    }

    public static partial class Args
    {
        public static SingleArg<UInt8> Add(byte x) => Args.Add(new UInt8(x));

        public static ArgsList<UInt8, T> Add<T>(this T tail, byte head)
            where T : struct, IArgEnumerator<T>
        {
            return new ArgsList<UInt8, T>(new UInt8(head), tail);
        }

        public static SingleArg<UInt16> Add(ushort x) => Args.Add(new UInt16(x));

        public static ArgsList<UInt16, T> Add<T>(this T tail, ushort head)
            where T : struct, IArgEnumerator<T>
        {
            return new ArgsList<UInt16, T>(new UInt16(head), tail);
        }

        public static SingleArg<UInt32> Add(uint x) => Args.Add(new UInt32(x));

        public static ArgsList<UInt32, T> Add<T>(this T tail, uint head)
            where T : struct, IArgEnumerator<T>
        {
            return new ArgsList<UInt32, T>(new UInt32(head), tail);
        }

        public static SingleArg<UInt64> Add(ulong x) => Args.Add(new UInt64(x));

        public static ArgsList<UInt64, T> Add<T>(this T tail, ulong head)
            where T : struct, IArgEnumerator<T>
        {
            return new ArgsList<UInt64, T>(new UInt64(head), tail);
        }

        public static SingleArg<UIntN> Add(BigInteger x) => Args.Add(new UIntN(x));

        public static ArgsList<UIntN, T> Add<T>(this T tail, BigInteger head)
            where T : struct, IArgEnumerator<T>
        {
            return new ArgsList<UIntN, T>(new UIntN(head), tail);
        }
    }
}
