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
    ///     The private key for an Algorand account.
    /// </summary>
    [Serializable]
    [AlgoApiFormatter(typeof(PrivateKeyFormatter))]
    [StructLayout(LayoutKind.Explicit, Size = SizeBytes)]
    public partial struct PrivateKey
        : IEquatable<PrivateKey>
            , IByteArray
    {
        public enum ParseError
        {
            None,
            InvalidBase64,
            InvalidLength
        }

        [SerializeField]
        [FieldOffset(0)]
        internal Ed25519.Seed seed;

        public unsafe void* GetUnsafePtr()
        {
            return seed.GetUnsafePtr();
        }

        public int Length => seed.Length;

        public const int SizeBytes = 32;

        public byte this[int index]
        {
            get => seed[index];
            set => seed[index] = value;
        }

        public bool Equals(PrivateKey other)
        {
            for (var i = 0; i < Length; i++)
                if (!this[i].Equals(other[i]))
                    return false;
            return true;
        }

        public Sig Sign<T>(T msg)
            where T : IByteArray
        {
            using var kp = ToKeyPair();
            return kp.SecretKey.Sign(msg);
        }

        public Mnemonic ToMnemonic()
        {
            using var bit11Array = this.ToBitArray(Allocator.Temp, 11);
            var result = new Mnemonic();
            for (var i = 0; i < Mnemonic.ChecksumIndex; i++)
                result[i] = (Mnemonic.Word)bit11Array[i];
            var checksum256 = Sha512.Hash256Truncated(this);
            using var checksum11Bit = checksum256.ToBitArray(Allocator.Temp, 11, 1);
            result[Mnemonic.ChecksumIndex] = (Mnemonic.Word)checksum11Bit[0];
            return result;
        }

        internal Ed25519.KeyPair ToKeyPair()
        {
            return seed.ToKeyPair();
        }

        internal Ed25519.PublicKey ToPublicKey()
        {
            return seed.ToPublicKey();
        }

        /// <summary>
        ///     Return the public key for this private key.
        /// </summary>
        /// <returns>An algorand address.</returns>
        public Address ToAddress()
        {
            return ToPublicKey();
        }

        public override string ToString()
        {
            var pk = ToPublicKey();
            var bytes = new byte[Length + pk.Length];
            for (var i = 0; i < Length; i++)
                bytes[i] = this[i];
            for (var i = 0; i < pk.Length; i++)
                bytes[i + Length] = pk[i];
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        ///     Convert the given base64 key string into a private key.
        /// </summary>
        /// <param name="keyString">A key string in base64 format.</param>
        /// <returns>A private key from the parsed key string.</returns>
        public static PrivateKey FromString(string keyString)
        {
            var key = new PrivateKey();
            var bytes = Convert.FromBase64String(keyString);
            for (var i = 0; i < key.Length; i++)
                key[i] = bytes[i];
            return key;
        }

        /// <summary>
        ///     Try to parse a base64 encoded private key.
        /// </summary>
        /// <param name="keyString">The base64 encoded private key.</param>
        /// <param name="pk">The private key from the base64 encoded string.</param>
        /// <returns>A ParseError if one is encountered.</returns>
        public static ParseError TryParse(string keyString, out PrivateKey pk)
        {
            pk = default;
            byte[] bytes;
            try
            {
                bytes = Convert.FromBase64String(keyString);
            }
            catch
            {
                return ParseError.InvalidBase64;
            }

            if (bytes.Length != pk.Length) return ParseError.InvalidLength;

            for (var i = 0; i < pk.Length; i++)
                pk[i] = bytes[i];
            return ParseError.None;
        }

        /// <summary>
        ///     Return the private key for a given mnemonic.
        /// </summary>
        /// <param name="mnemonic">A mnemonic with a valid checksum.</param>
        /// <returns>The private key for the 25 word mnemonic.</returns>
        public static PrivateKey FromMnemonic(Mnemonic mnemonic)
        {
            return mnemonic.ToPrivateKey();
        }
    }
}
