using System.Numerics;
using Unity.Collections;
using Unity.Mathematics;

namespace AlgoSdk.Abi
{
    public readonly struct UInt8 : IUIntN
    {
        readonly byte value;

        public UInt8(byte value)
        {
            this.value = value;
        }

        public ushort N => 8;

        public NativeArray<byte> Encode(AbiType type, Allocator allocator)
        {
            this.CheckFitsIn(type);
            var result = new NativeArray<byte>(1, allocator);
            result[0] = value;
            return result;
        }

        public int Length(AbiType type)
        {
            this.CheckFitsIn(type);
            return 1;
        }
    }

    public readonly struct UInt16 : IUIntN
    {
        readonly ushort value;

        public UInt16(ushort value)
        {
            this.value = value;
        }

        public ushort N
        {
            get
            {
                if (value < 1 << 8)
                    return 8;
                return 16;
            }
        }

        public NativeArray<byte> Encode(AbiType type, Allocator allocator)
        {
            this.CheckFitsIn(type);
            var encodedLength = type.N / 8;
            var result = new NativeArray<byte>(encodedLength, allocator);
            value.CopyToNativeBytesBigEndian(ref result, encodedLength - 2);
            return result;
        }

        public int Length(AbiType type)
        {
            this.CheckFitsIn(type);
            var encodedLength = type.N / 8;
            return encodedLength;
        }
    }

    public readonly struct UInt32 : IUIntN
    {
        readonly uint value;

        public UInt32(uint value)
        {
            this.value = value;
        }

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

        public NativeArray<byte> Encode(AbiType type, Allocator allocator)
        {
            this.CheckFitsIn(type);
            var encodedLength = type.N / 8;
            var result = new NativeArray<byte>(encodedLength, allocator);
            value.CopyToNativeBytesBigEndian(ref result, encodedLength - 4);
            return result;
        }

        public int Length(AbiType type)
        {
            this.CheckFitsIn(type);
            var encodedLength = type.N / 8;
            return encodedLength;
        }
    }

    public readonly struct UInt64 : IUIntN
    {
        readonly ulong value;

        public UInt64(ulong value)
        {
            this.value = value;
        }

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

        public NativeArray<byte> Encode(AbiType type, Allocator allocator)
        {
            this.CheckFitsIn(type);
            var encodedLength = type.N / 8;
            var result = new NativeArray<byte>(encodedLength, allocator);
            value.CopyToNativeBytesBigEndian(ref result, encodedLength - 8);
            return result;
        }

        public int Length(AbiType type)
        {
            this.CheckFitsIn(type);
            return type.N / 8;
        }
    }

    public readonly struct UIntN : IUIntN
    {
        readonly BigInteger value;

        public UIntN(BigInteger value)
        {
            this.value = value;
        }

        public BigInteger Value => value;

        public ushort N => (ushort)(value.GetByteCount() * 8);

        public NativeArray<byte> Encode(AbiType type, Allocator allocator)
        {
            this.CheckFitsIn(type);
            var encodedLength = type.N / 8;
            var bytes = value.ToByteArray(isUnsigned: true, isBigEndian: true);
            if (encodedLength == bytes.Length)
            {
                return new NativeArray<byte>(bytes, allocator);
            }

            var encoded = new NativeArray<byte>(encodedLength, allocator);
            var offset = encoded.Length - bytes.Length;
            for (var i = 0; i < bytes.Length; i++)
                encoded[i + offset] = bytes[i];
            return encoded;
        }

        public int Length(AbiType type)
        {
            this.CheckFitsIn(type);
            return type.N / 8;
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

    public interface IUIntN : IAbiValue
    {
        ushort N { get; }
    }

    public static class UintNExtensions
    {
        public static void CheckFitsIn<T>(this T x, AbiType type)
            where T : IUIntN
        {
            if (type.ValueType != AbiValueType.UIntN)
                throw new System.ArgumentException($"Cannot encode this value to type {type.Name}.", nameof(type));
            if (type.N < x.N)
                throw new System.ArgumentException($"Not enough bits in {type.Name} to store this value.", nameof(type));
        }
    }
}
