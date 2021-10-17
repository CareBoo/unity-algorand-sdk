using System;
using System.Runtime.InteropServices;
using AlgoSdk.Crypto;
using AlgoSdk.Formatters;
using AlgoSdk.LowLevel;
using Unity.Collections;
using UnityEngine;

namespace AlgoSdk
{
    [Serializable]
    [StructLayout(LayoutKind.Explicit, Size = SizeBytes)]
    [AlgoApiFormatter(typeof(AddressFormatter))]
    public struct Address
        : IByteArray
        , IEquatable<Address>
    {
        public const int SizeBytes = Ed25519.PublicKey.SizeBytes;

        public static readonly Address Empty = default(Address);

        [FieldOffset(0)] internal byte buffer;
        [SerializeField] [FieldOffset(0)] internal Ed25519.PublicKey publicKey;

        public unsafe void* GetUnsafePtr()
        {
            fixed (byte* b = &buffer)
                return b;
        }

        public int Length => SizeBytes;

        public byte this[int index]
        {
            get => this.GetByteAt(index);
            set => this.SetByteAt(index, value);
        }

        public FixedString128Bytes ToFixedString()
        {
            var result = new FixedString128Bytes();
            var checksum = ComputeCheckSum(publicKey);
            var bytes = new NativeByteArray(SizeBytes + CheckSum.SizeBytes, Allocator.Temp);
            try
            {
                bytes.CopyFrom(this, 0);
                bytes.CopyFrom(checksum, SizeBytes);
                Base32Encoding.ToString(bytes, ref result);
            }
            finally
            {
                bytes.Dispose();
            }
            var trimPadding = result.Length;
            while (result[trimPadding - 1] == Base32Encoding.PaddingCharValue)
                trimPadding--;
            result.Length = trimPadding;
            return result;
        }

        public Ed25519.PublicKey ToPublicKey()
        {
            return publicKey;
        }

        public override string ToString()
        {
            return ToFixedString().ToString();
        }

        public static Address FromString<TString>(TString s)
            where TString : struct, IUTF8Bytes, INativeList<byte>
        {
            if (s.Length == 0)
                return Address.Empty;
            Address address = default;
            CheckSum checksum = default;
            var bytes = new NativeByteArray(SizeBytes + CheckSum.SizeBytes, Allocator.Temp);
            try
            {
                Base32Encoding.ToBytes(s, ref bytes);
                bytes.CopyTo(ref address, 0);
                bytes.CopyTo(ref checksum, SizeBytes);
            }
            finally
            {
                bytes.Dispose();
            }
            var computedChecksum = ComputeCheckSum(address.publicKey);
            if (checksum != computedChecksum)
                throw new ArgumentException($"Checksum for {s} was invalid. Got {checksum} but was expecting {computedChecksum}");
            return address;
        }

        public static Address FromString(string addressString)
        {
            var s = new FixedString128Bytes(addressString);
            return FromString(s);
        }

        public static Address FromPublicKey(Ed25519.PublicKey publicKey)
        {
            return new Address() { publicKey = publicKey };
        }

        public static implicit operator Address(string s)
        {
            return FromString(s);
        }

        public static implicit operator Address(Ed25519.PublicKey publicKey)
        {
            return Address.FromPublicKey(publicKey);
        }

        public static implicit operator Ed25519.PublicKey(Address address)
        {
            return address.ToPublicKey();
        }

        public static bool operator ==(in Address a1, in Address a2)
        {
            return a1.Equals(a2);
        }

        public static bool operator !=(in Address a1, in Address a2)
        {
            return !a1.Equals(a2);
        }

        private static CheckSum ComputeCheckSum(Ed25519.PublicKey publicKey)
        {
            return Sha512.Hash256Truncated(publicKey);
        }

        public override bool Equals(object obj)
        {
            return ByteArray.Equals(in this, obj);
        }

        public override int GetHashCode()
        {
            return ByteArray.GetHashCode(in this);
        }

        public bool Equals(Address other)
        {
            for (var i = 0; i < Ed25519.PublicKey.SizeBytes; i++)
                if (!this[i].Equals(other[i]))
                    return false;
            return true;
        }

        [Serializable]
        [StructLayout(LayoutKind.Explicit, Size = SizeBytes)]
        public struct CheckSum
        : IByteArray
        , IEquatable<CheckSum>
        {
            [SerializeField] [FieldOffset(0)] internal byte byte0000;
            [SerializeField] [FieldOffset(1)] internal byte byte0001;
            [SerializeField] [FieldOffset(2)] internal byte byte0002;
            [SerializeField] [FieldOffset(3)] internal byte byte0003;

            public const int SizeBytes = 4;

            public unsafe void* GetUnsafePtr()
            {
                fixed (byte* b = &byte0000)
                    return b;
            }

            public int Length => SizeBytes;

            public byte this[int index]
            {
                get => this.GetByteAt(index);
                set => this.SetByteAt(index, value);
            }

            public bool Equals(CheckSum other)
            {
                return ByteArray.Equals(in this, in other);
            }

            public static implicit operator CheckSum(Sha512_256_Hash hash)
            {
                var checkSum = new CheckSum();
                var hashStart = hash.Length - checkSum.Length;
                for (var i = 0; i < checkSum.Length; i++)
                    checkSum[i] = hash[i + hashStart];
                return checkSum;
            }

            public static bool operator ==(in CheckSum x, in CheckSum y)
            {
                return ByteArray.Equals(in x, in y);
            }

            public static bool operator !=(in CheckSum x, in CheckSum y)
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

            public override string ToString()
            {
                return System.Convert.ToBase64String(this.ToArray());
            }
        }
    }
}
