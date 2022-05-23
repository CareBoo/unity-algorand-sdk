using System.Numerics;
using Unity.Collections;
using Unity.Mathematics;

namespace AlgoSdk.Abi
{
    public readonly struct Uint8 : IUintN
    {
        readonly byte value;

        public Uint8(byte value)
        {
            this.value = value;
        }

        public bool IsStatic => true;

        public string AbiTypeName => "uint8";

        public ushort N => 8;

        public NativeArray<byte> Encode(Method.Arg definition, Allocator allocator)
        {
            var type = definition.Type;
            if (Alias.UnderlyingType(type) != AbiTypeName)
                throw new System.ArgumentException($"Cannot convert {type} to uint8.", nameof(type));
            var result = new NativeArray<byte>(1, allocator);
            result[0] = value;
            return result;
        }

        public int Length(Method.Arg definition)
        {
            var type = definition.Type;
            if (Alias.UnderlyingType(type) != AbiTypeName)
                throw new System.ArgumentException($"Cannot convert {type} to uint8.", nameof(type));
            return 1;
        }
    }

    public readonly struct Uint16 : IUintN
    {
        readonly ushort value;

        public Uint16(ushort value)
        {
            this.value = value;
        }

        public bool IsStatic => true;

        public string AbiTypeName => $"uint{N}";

        public ushort N
        {
            get
            {
                if (value < 1 << 8)
                    return 8;
                return 16;
            }
        }

        public NativeArray<byte> Encode(Method.Arg definition, Allocator allocator)
        {
            var type = definition.Type;
            this.CheckFitsIn(type, out int encodedLength);
            var result = new NativeArray<byte>(encodedLength, allocator);
            value.CopyToNativeBytesBigEndian(ref result, encodedLength - 2);
            return result;
        }

        public int Length(Method.Arg definition)
        {
            var type = definition.Type;
            this.CheckFitsIn(type, out int encodedLength);
            return encodedLength;
        }
    }

    public readonly struct Uint32 : IUintN
    {
        readonly uint value;

        public Uint32(uint value)
        {
            this.value = value;
        }

        public bool IsStatic => true;

        public string AbiTypeName => $"uint{N}";

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

        public NativeArray<byte> Encode(Method.Arg definition, Allocator allocator)
        {
            var type = definition.Type;
            this.CheckFitsIn(type, out int encodedLength);
            var result = new NativeArray<byte>(encodedLength, allocator);
            value.CopyToNativeBytesBigEndian(ref result, encodedLength - 4);
            return result;
        }

        public int Length(Method.Arg definition)
        {
            var type = definition.Type;
            this.CheckFitsIn(type, out int encodedLength);
            return encodedLength;
        }
    }

    public readonly struct Uint64 : IUintN
    {
        readonly ulong value;

        public Uint64(ulong value)
        {
            this.value = value;
        }

        public string AbiTypeName => $"uint{N}";

        public bool IsStatic => true;

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

        public NativeArray<byte> Encode(Method.Arg definition, Allocator allocator)
        {
            var type = definition.Type;
            this.CheckFitsIn(type, out int encodedLength);
            var result = new NativeArray<byte>(encodedLength, allocator);
            value.CopyToNativeBytesBigEndian(ref result, encodedLength - 8);
            return result;
        }

        public int Length(Method.Arg definition)
        {
            var type = definition.Type;
            this.CheckFitsIn(type, out int encodedLength);
            return encodedLength;
        }
    }

    public readonly struct UintN : IUintN
    {
        readonly BigInteger value;

        public UintN(BigInteger value)
        {
            this.value = value;
        }

        public bool IsStatic => true;

        public string AbiTypeName => $"uint{N}";

        public BigInteger Value => value;

        public ushort N => (ushort)(value.GetByteCount() * 8);

        public NativeArray<byte> Encode(Method.Arg definition, Allocator allocator)
        {
            var type = definition.Type;
            this.CheckFitsIn(type, out int encodedLength);
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

        public int Length(Method.Arg definition)
        {
            var type = definition.Type;
            this.CheckFitsIn(type, out int byteLength);
            return byteLength;
        }

        public NativeArray<byte> EncodeAs(ushort n, Allocator allocator)
        {
            this.CheckFitsIn(n);
            var encodedLength = n / 8;
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

        public int EncodedLengthAs(ushort n)
        {
            this.CheckFitsIn(n);
            return n / 8;
        }
    }

    public static partial class Args
    {
        public static SingleArg<Uint8> Add(byte x) => Args.Add(new Uint8(x));

        public static ArgsList<Uint8, T> Add<T>(this T tail, byte head)
            where T : struct, IArgEnumerator<T>
        {
            return new ArgsList<Uint8, T>(new Uint8(head), tail);
        }

        public static SingleArg<Uint16> Add(ushort x) => Args.Add(new Uint16(x));

        public static ArgsList<Uint16, T> Add<T>(this T tail, ushort head)
            where T : struct, IArgEnumerator<T>
        {
            return new ArgsList<Uint16, T>(new Uint16(head), tail);
        }

        public static SingleArg<Uint32> Add(uint x) => Args.Add(new Uint32(x));

        public static ArgsList<Uint32, T> Add<T>(this T tail, uint head)
            where T : struct, IArgEnumerator<T>
        {
            return new ArgsList<Uint32, T>(new Uint32(head), tail);
        }

        public static SingleArg<Uint64> Add(ulong x) => Args.Add(new Uint64(x));

        public static ArgsList<Uint64, T> Add<T>(this T tail, ulong head)
            where T : struct, IArgEnumerator<T>
        {
            return new ArgsList<Uint64, T>(new Uint64(head), tail);
        }

        public static SingleArg<UintN> Add(BigInteger x) => Args.Add(new UintN(x));

        public static ArgsList<UintN, T> Add<T>(this T tail, BigInteger head)
            where T : struct, IArgEnumerator<T>
        {
            return new ArgsList<UintN, T>(new UintN(head), tail);
        }
    }

    public interface IUintN : IAbiType
    {
        ushort N { get; }
    }

    public static class UintNExtensions
    {
        public static void CheckFitsIn<T>(this T x, string type, out int byteLength)
            where T : IUintN
        {
            var bits = GetTypeBits(type);
            if (bits < x.N)
                throw new System.ArgumentException($"Not enough bits in {type} to store this value.", nameof(type));
            byteLength = GetTypeBits(type) / 8;
        }

        public static void CheckFitsIn<T>(this T x, int bits)
            where T : IUintN
        {
            if (bits < x.N)
                throw new System.ArgumentException("Not enough bits available to store this value.", nameof(bits));
        }

        public static ushort GetTypeBits(string type)
        {
            return Alias.UnderlyingType(type) switch
            {
                "uint8" => 8,
                "uint16" => 16,
                "uint24" => 24,
                "uint32" => 32,
                "uint40" => 40,
                "uint48" => 48,
                "uint56" => 56,
                "uint64" => 64,
                "uint72" => 72,
                "uint80" => 80,
                "uint88" => 88,
                "uint96" => 96,
                "uint104" => 104,
                "uint112" => 112,
                "uint120" => 120,
                "uint128" => 128,
                "uint136" => 136,
                "uint144" => 144,
                "uint152" => 152,
                "uint160" => 160,
                "uint168" => 168,
                "uint176" => 176,
                "uint184" => 184,
                "uint192" => 192,
                "uint200" => 200,
                "uint208" => 208,
                "uint216" => 216,
                "uint224" => 224,
                "uint232" => 232,
                "uint240" => 240,
                "uint248" => 248,
                "uint256" => 256,
                "uint264" => 264,
                "uint272" => 272,
                "uint280" => 280,
                "uint288" => 288,
                "uint296" => 296,
                "uint304" => 304,
                "uint312" => 312,
                "uint320" => 320,
                "uint328" => 328,
                "uint336" => 336,
                "uint344" => 344,
                "uint352" => 352,
                "uint360" => 360,
                "uint368" => 368,
                "uint376" => 376,
                "uint384" => 384,
                "uint392" => 392,
                "uint400" => 400,
                "uint408" => 408,
                "uint416" => 416,
                "uint424" => 424,
                "uint432" => 432,
                "uint440" => 440,
                "uint448" => 448,
                "uint456" => 456,
                "uint464" => 464,
                "uint472" => 472,
                "uint480" => 480,
                "uint488" => 488,
                "uint496" => 496,
                "uint504" => 504,
                "uint512" => 512,
                _ => throw new System.ArgumentException($"Cannot encode {type} as uintN", nameof(type))
            };
        }
    }
}
