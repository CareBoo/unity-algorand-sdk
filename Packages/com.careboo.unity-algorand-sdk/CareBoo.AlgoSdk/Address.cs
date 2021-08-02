using System;
using System.Runtime.InteropServices;
using AlgoSdk.Crypto;
using AlgoSdk.LowLevel;
using UnityEngine;

namespace AlgoSdk
{
    [Serializable]
    [StructLayout(LayoutKind.Explicit, Size = 32)]
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
        }

        public const int SizeBytes = Ed25519.PublicKey.SizeBytes;

        [SerializeField] [FieldOffset(0)] internal Ed25519.PublicKey publicKey;

        public IntPtr Buffer => publicKey.Buffer;

        public int Length => publicKey.Length;

        public byte this[int index]
        {
            get => publicKey[index];
            set => publicKey[index] = value;
        }

        public CheckSum ComputeCheckSum()
        {
            return Sha512.Hash256Truncated(in this);
        }

        public override string ToString()
        {
            var bytes = new byte[Length + CheckSum.SizeBytes];
            for (var i = 0; i < Length; i++)
                bytes[i] = this[i];
            var checkSum = ComputeCheckSum();
            for (var i = 0; i < checkSum.Length; i++)
                bytes[i + Length] = checkSum[i];
            return Base32Encoding.ToString(bytes).Trim('=');
        }

        public static Address FromString(string addressString)
        {
            var bytes = Base32Encoding.ToBytes(addressString);
            var address = new Address();
            for (var i = 0; i < bytes.Length - CheckSum.SizeBytes; i++)
                address[i] = bytes[i];
            var checkSum = new CheckSum();
            for (var i = 0; i < checkSum.Length; i++)
                checkSum[i] = bytes[i + bytes.Length - checkSum.Length];
            if (!address.ComputeCheckSum().Equals(checkSum))
                throw new ArgumentException($"Checksum for {addressString} was invalid!");
            return address;
        }

        public static implicit operator Address(Ed25519.PublicKey publicKey)
        {
            return new Address() { publicKey = publicKey };
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
