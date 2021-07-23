using System;
using System.Runtime.InteropServices;
using AlgoSdk.LowLevel;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace AlgoSdk.Crypto
{
    [StructLayout(LayoutKind.Explicit, Size = 64)]
    internal struct Sha512StateVector
    {
        [FieldOffset(0)] internal FixedBytes64 buffer;

        public const int Length = 8;

        unsafe internal byte* Buffer
        {
            get
            {
                fixed (byte* b = &buffer.offset0000.offset0000.byte0000)
                    return b;
            }
        }

        public UInt64 this[int index]
        {
            get
            {
                ByteArray.CheckElementAccess(index, Length);
                unsafe
                {
                    return UnsafeUtility.ReadArrayElement<UInt64>(Buffer, index);
                }
            }
            set
            {
                ByteArray.CheckElementAccess(index, Length);
                unsafe
                {
                    UnsafeUtility.WriteArrayElement<UInt64>(Buffer, index, value);
                }
            }
        }

        public static implicit operator Sha512StateVector(UInt64[] arr)
        {
            var result = new Sha512StateVector();
            for (var i = 0; i < arr.Length; i++)
                result[i] = arr[i];
            return result;
        }
    }

    [StructLayout(LayoutKind.Explicit, Size = 16)]
    internal struct Sha512StateCount
    {
        [FieldOffset(0)] internal FixedBytes16 buffer;

        public const int Length = 2;

        unsafe internal byte* Buffer
        {
            get
            {
                fixed (byte* b = &buffer.byte0000)
                    return b;
            }
        }

        public UInt64 this[int index]
        {
            get
            {
                ByteArray.CheckElementAccess(index, Length);
                unsafe
                {
                    return UnsafeUtility.ReadArrayElement<UInt64>(Buffer, index);
                }
            }
            set
            {
                ByteArray.CheckElementAccess(index, Length);
                unsafe
                {
                    UnsafeUtility.WriteArrayElement<UInt64>(Buffer, index, value);
                }
            }
        }
    }

    internal static unsafe partial class sodium
    {
        [StructLayout(LayoutKind.Explicit, Size = SizeBytes)]
        internal struct crypto_hash_sha512_state
        {
            internal const int SizeBytes = (8 * 8) + (2 * 8) + 128;

            [FieldOffset(0)] internal Sha512StateVector vector;
            [FieldOffset(64)] internal Sha512StateCount count;
            [FieldOffset(80)] internal FixedBytes128 buffer;
        }

        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int crypto_hash_sha512(
            void* @out,
            void* @in,
            ulong inlen);

        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int crypto_hash_sha512_init(
            crypto_hash_sha512_state* state);

        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int crypto_hash_sha512_update(
            crypto_hash_sha512_state* state,
            void* @in,
            ulong inlen);

        [DllImport(Library, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int crypto_hash_sha512_final(
            crypto_hash_sha512_state* state,
            void* @out);
    }
}
