using System;
using System.Runtime.InteropServices;
using AlgoSdk.Crypto;
using AlgoSdk.LowLevel;
using Unity.Collections;
using UnityEngine;

namespace AlgoSdk
{
    [Serializable]
    [StructLayout(LayoutKind.Explicit, Size = SizeBytes)]
    public struct Address
        : IByteArray
        , IEquatable<Address>
    {
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

            public unsafe IntPtr Buffer
            {
                get
                {
                    fixed (byte* b = &byte0000)
                        return (IntPtr)b;
                }
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
                return System.Convert.ToBase64String(this.AsReadOnlySpan().ToArray());
            }
        }

        public const int SizeBytes = Ed25519.PublicKey.SizeBytes + CheckSum.SizeBytes;

        [FieldOffset(0)] internal byte buffer;
        [SerializeField] [FieldOffset(0)] internal Ed25519.PublicKey publicKey;
        [SerializeField] [FieldOffset(Ed25519.PublicKey.SizeBytes)] internal CheckSum checkSum;

        public unsafe IntPtr Buffer
        {
            get
            {
                fixed (byte* b = &buffer)
                    return (IntPtr)b;
            }
        }

        public int Length => SizeBytes;

        public byte this[int index]
        {
            get => this.GetByteAt(index);
            set => this.SetByteAt(index, value);
        }

        private static CheckSum ComputeCheckSum(in Ed25519.PublicKey publicKey)
        {
            return Sha512.Hash256Truncated(in publicKey);
        }

        public Address GenerateCheckSum()
        {
            checkSum = ComputeCheckSum(in publicKey);
            return this;
        }

        public FixedString128 ToFixedString()
        {
            var result = new FixedString128();
            Base32Encoding.ToString(in this, ref result);
            var trimPadding = result.Length;
            while (result[trimPadding - 1] == Base32Encoding.PaddingCharValue)
                trimPadding--;
            result.Length = trimPadding;
            return result;
        }

        public override string ToString()
        {
            return ToFixedString().ToString();
        }

        public static Address FromString<TString>(in TString s)
            where TString : struct, IUTF8Bytes, INativeList<byte>
        {
            var address = new Address();
            Base32Encoding.ToBytes(in s, ref address);
            CheckSum checkSum = ComputeCheckSum(in address.publicKey);
            if (address.checkSum != checkSum)
                throw new ArgumentException($"Checksum for {s} was invalid. Got {checkSum} but was expecting {address.checkSum}");
            return address;
        }

        public static Address FromString(string addressString)
        {
            var s = new FixedString128(addressString);
            return FromString(in s);
        }

        public static implicit operator Address(string s)
        {
            return FromString(s);
        }

        public static implicit operator Address(Ed25519.PublicKey publicKey)
        {
            var address = new Address() { publicKey = publicKey };
            address.GenerateCheckSum();
            return address;
        }

        public static implicit operator Ed25519.PublicKey(Address address)
        {
            return address.publicKey;
        }

        public static bool operator ==(in Address a1, in Address a2)
        {
            return a1.Equals(a2);
        }

        public static bool operator !=(in Address a1, in Address a2)
        {
            return !a1.Equals(a2);
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
            return ByteArray.Equals(in this, in other);
        }
    }
}
