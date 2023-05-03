using System;
using System.Runtime.InteropServices;
using Algorand.Unity.Crypto;
using Algorand.Unity.Formatters;
using Algorand.Unity.LowLevel;
using Unity.Collections;
using UnityEngine;

namespace Algorand.Unity
{
    /// <summary>
    ///     An error describing issues with address formatting when converting from a string.
    /// </summary>
    public enum AddressFormatError
    {
        /// <summary>
        ///     The formatting is correct, and there were no errors.
        /// </summary>
        None,

        /// <summary>
        ///     The calculated checksum does not match the given checksum.
        /// </summary>
        InvalidChecksum,

        /// <summary>
        ///     The length of the address string is not <see cref="Address.StringLength" />.
        /// </summary>
        IncorrectLength,

        /// <summary>
        ///     The string is not valid Base32 format.
        /// </summary>
        NotBase32
    }

    /// <summary>
    ///     A public key for an Algorand account.
    /// </summary>
    [Serializable]
    [AlgoApiFormatter(typeof(AddressFormatter))]
    [StructLayout(LayoutKind.Explicit, Size = SizeBytes)]
    public partial struct Address
        : IByteArray
            , IEquatable<Address>
    {
        /// <summary>
        ///     Size of an Algorand Address in Bytes.
        /// </summary>
        public const int SizeBytes = Ed25519.PublicKey.SizeBytes;

        /// <summary>
        ///     Length of a formatted Address string.
        /// </summary>
        public const int StringLength = 58;

        /// <summary>
        ///     An empty address.
        /// </summary>
        public static readonly Address Empty;

        [FieldOffset(0)]
        [SerializeField]
        internal Ed25519.PublicKey publicKey;

        [FieldOffset(0)]
        internal byte buffer;

        /// <summary>
        ///     Get the pointer to this struct.
        /// </summary>
        /// <returns>A pointer to this struct.</returns>
        public unsafe void* GetUnsafePtr()
        {
            fixed (byte* b = &buffer)
            {
                return b;
            }
        }

        /// <summary>
        ///     The length of an Address in Bytes = 32.
        /// </summary>
        public int Length => SizeBytes;

        /// <summary>
        ///     The byte of this address at a given index.
        /// </summary>
        /// <param name="index">An index in the range [0, <see cref="Length" />).</param>
        public byte this[int index]
        {
            get => this.GetByteAt(index);
            set => this.SetByteAt(index, value);
        }

        public bool Equals(Address other)
        {
            for (var i = 0; i < Ed25519.PublicKey.SizeBytes; i++)
                if (!this[i].Equals(other[i]))
                    return false;
            return true;
        }

        /// <summary>
        ///     Converts this address to a fixed string base32 representation with padding trimmed.
        /// </summary>
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

            Base32Encoding.TrimPadding(ref result);
            return result;
        }

        /// <summary>
        ///     Returns the ed25519 public key this address represents.
        /// </summary>
        public Ed25519.PublicKey ToPublicKey()
        {
            return publicKey;
        }

        /// <summary>
        ///     Converts this address to its base32 string representation with trimmed padding.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ToFixedString().ToString();
        }

        /// <summary>
        ///     Try to parse a string for an address.
        /// </summary>
        public static AddressFormatError TryParse<TString>(TString s, out Address address)
            where TString : struct, IUTF8Bytes, INativeList<byte>
        {
            address = default;
            if (s.Length == 0) return AddressFormatError.None;

            if (s.Length != StringLength) return AddressFormatError.IncorrectLength;

            CheckSum checksum = default;
            var bytes = new NativeByteArray(SizeBytes + CheckSum.SizeBytes, Allocator.Temp);
            try
            {
                var error = Base32Encoding.ToBytes(s, ref bytes);
                if (error != ConversionError.None) return AddressFormatError.NotBase32;

                bytes.CopyTo(ref address, 0);
                bytes.CopyTo(ref checksum, SizeBytes);
            }
            finally
            {
                bytes.Dispose();
            }

            var computedChecksum = ComputeCheckSum(address.publicKey);
            return checksum != computedChecksum ? AddressFormatError.InvalidChecksum : AddressFormatError.None;
        }

        /// <summary>
        ///     Try to parse a string for an address.
        /// </summary>
        public static AddressFormatError TryParse(string s, out Address address)
        {
            if (s == null)
            {
                address = default;
                return AddressFormatError.IncorrectLength;
            }

            using var nativeS = new NativeText(s, Allocator.Temp);
            return TryParse(nativeS, out address);
        }

        /// <summary>
        ///     Determines if the given string is a correctly formatted address.
        /// </summary>
        public static bool IsAddressString<TString>(TString s)
            where TString : struct, IUTF8Bytes, INativeList<byte>
        {
            return TryParse(s, out var address) == AddressFormatError.None;
        }

        /// <summary>
        ///     Determines if the given string is a correctly formatted address.
        /// </summary>
        public static bool IsAddressString(string s)
        {
            return TryParse(s, out var address) == AddressFormatError.None;
        }

        /// <summary>
        ///     Get an address from a string. This will throw an error if the string isn't formatter properly.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown if the address has an invalid checksum or is not 58 chars long.</exception>
        public static Address FromString<TString>(TString s)
            where TString : struct, IUTF8Bytes, INativeList<byte>
        {
            var error = TryParse(s, out var address);
            switch (error)
            {
                case AddressFormatError.None:
                    return address;
                case AddressFormatError.InvalidChecksum:
                    throw new ArgumentException(
                        $"'{s}' is not a valid address string because it has an invalid checksum.");
                case AddressFormatError.IncorrectLength:
                    throw new ArgumentException(
                        $"'{s}' is not a valid address string because its length, {s.Length}, is not {StringLength}.");
                case AddressFormatError.NotBase32:
                    throw new ArgumentException(
                        $"'{s}' is not a properly formatted base32 string.");
                default:
                    throw new ArgumentException(
                        $"'{s}' is not a properly formatted address string.");
            }
        }

        /// <summary>
        ///     Get an address from a string. This will throw an error if the string isn't formatter properly.
        /// </summary>
        /// <exception cref="ArgumentException">Thrown if the address has an invalid checksum or is not 58 chars long.</exception>
        public static Address FromString(string addressString)
        {
            using var s = new NativeText(addressString, Allocator.Temp);
            return FromString(s);
        }

        /// <summary>
        ///     Gets the address representation of an ed25519 public key.
        /// </summary>
        public static Address FromPublicKey(Ed25519.PublicKey publicKey)
        {
            return new Address { publicKey = publicKey };
        }

        public static implicit operator Address(string s)
        {
            return FromString(s);
        }

        public static implicit operator string(Address addr)
        {
            return addr.ToString();
        }

        public static implicit operator Address(Ed25519.PublicKey publicKey)
        {
            return FromPublicKey(publicKey);
        }

        public static implicit operator Ed25519.PublicKey(Address address)
        {
            return address.ToPublicKey();
        }

        public static implicit operator Address(Sha512_256_Hash checksum)
        {
            var result = Empty;
            checksum.CopyTo(ref result);
            return result;
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
            return ByteArray.Equals(this, obj);
        }

        public override int GetHashCode()
        {
            return ByteArray.GetHashCode(this);
        }

        [Serializable]
        [StructLayout(LayoutKind.Explicit, Size = SizeBytes)]
        public struct CheckSum
            : IByteArray
                , IEquatable<CheckSum>
        {
            public const int SizeBytes = 4;

            [SerializeField]
            [FieldOffset(0)]
            internal byte byte0000;

            [SerializeField]
            [FieldOffset(1)]
            internal byte byte0001;

            [SerializeField]
            [FieldOffset(2)]
            internal byte byte0002;

            [SerializeField]
            [FieldOffset(3)]
            internal byte byte0003;

            public unsafe void* GetUnsafePtr()
            {
                fixed (byte* b = &byte0000)
                {
                    return b;
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
                return ByteArray.Equals(this, other);
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
                return ByteArray.Equals(x, y);
            }

            public static bool operator !=(in CheckSum x, in CheckSum y)
            {
                return !ByteArray.Equals(x, y);
            }

            public override bool Equals(object obj)
            {
                return ByteArray.Equals(this, obj);
            }

            public override int GetHashCode()
            {
                return ByteArray.GetHashCode(this);
            }

            public override string ToString()
            {
                return Convert.ToBase64String(this.ToArray());
            }
        }
    }
}