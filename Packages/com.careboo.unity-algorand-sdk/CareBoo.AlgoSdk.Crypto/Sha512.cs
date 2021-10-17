using System;
using System.Runtime.InteropServices;
using AlgoSdk.LowLevel;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using static AlgoSdk.Crypto.sodium;

namespace AlgoSdk.Crypto
{
    public static class Sha512
    {
        internal static readonly Sha512StateVector FIPS_Sha512_256_IV = new UInt64[8]
        {
            Convert.ToUInt64("22312194FC2BF72C", 16),
            Convert.ToUInt64("9F555FA3C84C64C2", 16),
            Convert.ToUInt64("2393B86B6F53B151", 16),
            Convert.ToUInt64("963877195940EABD", 16),
            Convert.ToUInt64("96283EE2A88EFFE3", 16),
            Convert.ToUInt64("BE5E1E2553863992", 16),
            Convert.ToUInt64("2B0199FC2C85B8AA", 16),
            Convert.ToUInt64("0EB72DDC81C52CA2", 16),
        };

        public unsafe static Sha512_Hash Hash<TByteArray>(in TByteArray bytes)
            where TByteArray : unmanaged, IByteArray
        {
            fixed (void* b = &bytes)
                return Hash(b, bytes.Length);
        }

        public unsafe static Sha512_Hash Hash(NativeArray<byte> bytes)
        {
            return Hash(bytes.GetUnsafePtr(), bytes.Length);
        }

        public unsafe static Sha512_Hash Hash(void* ptr, int length)
        {
            var hash = new Sha512_Hash();
            crypto_hash_sha512(&hash, ptr, (ulong)length);
            return hash;
        }

        public unsafe static Sha512_256_Hash Hash256Truncated<TByteArray>(in TByteArray bytes)
            where TByteArray : unmanaged, IByteArray
        {
            fixed (void* bytesPtr = &bytes)
                return Hash256Truncated(bytesPtr, bytes.Length);
        }

        public unsafe static Sha512_256_Hash Hash256Truncated(NativeArray<byte> bytes)
        {
            return Hash256Truncated(bytes.GetUnsafePtr(), bytes.Length);
        }

        public unsafe static Sha512_256_Hash Hash256Truncated(void* ptr, int length)
        {
            var hashState = default(crypto_hash_sha512_state);
            crypto_hash_sha512_init(&hashState);
            hashState.vector = FIPS_Sha512_256_IV;
            crypto_hash_sha512_update(&hashState, ptr, (ulong)length);
            var hash512 = new Sha512_Hash();
            crypto_hash_sha512_final(&hashState, &hash512);
            var result = new Sha512_256_Hash();
            ByteArray.CopyTo(hash512, ref result);
            return result;
        }
    }

    [StructLayout(LayoutKind.Explicit, Size = SizeBytes)]
    public struct Sha512_Hash
    : IByteArray
    , IEquatable<Sha512_Hash>
    {
        [FieldOffset(0)] internal FixedBytes16 offset0000;
        [FieldOffset(16)] internal FixedBytes16 offset0016;
        [FieldOffset(32)] internal FixedBytes16 offset0032;
        [FieldOffset(48)] internal FixedBytes16 offset0048;
        public const int SizeBytes = 512 / 8;

        public unsafe void* GetUnsafePtr()
        {
            fixed (byte* b = &offset0000.byte0000)
                return b;
        }

        public int Length => SizeBytes;

        public byte this[int index]
        {
            get => this.GetByteAt(index);
            set => this.SetByteAt(index, value);
        }

        public bool Equals(Sha512_Hash other)
        {
            for (var i = 0; i < Length; i++)
                if (this[i] != other[i])
                    return false;
            return true;
        }
    }

    [StructLayout(LayoutKind.Explicit, Size = SizeBytes)]
    public struct Sha512_256_Hash
        : IByteArray
        , IEquatable<Sha512_256_Hash>
    {
        [FieldOffset(0)] internal FixedBytes16 offset0000;
        [FieldOffset(16)] internal FixedBytes16 offset0016;
        public const int SizeBytes = 256 / 8;

        public unsafe void* GetUnsafePtr()
        {
            fixed (byte* b = &offset0000.byte0000)
                return b;
        }

        public int Length => SizeBytes;

        public byte this[int index]
        {
            get => this.GetByteAt(index);
            set => this.SetByteAt(index, value);
        }

        public bool Equals(Sha512_256_Hash other)
        {
            return ByteArray.Equals(this, other);
        }

        public static bool operator ==(in Sha512_256_Hash x, in Sha512_256_Hash y)
        {
            return ByteArray.Equals(in x, in y);
        }

        public static bool operator !=(in Sha512_256_Hash x, in Sha512_256_Hash y)
        {
            return !ByteArray.Equals(in x, in y);
        }

        public override bool Equals(object obj)
        {
            return ByteArray.Equals(in this, obj);
        }

        public override int GetHashCode()
        {
            return ByteArray.GetHashCode(in this);
        }
    }
}
