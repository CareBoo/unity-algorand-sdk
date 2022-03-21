using System;
using System.Runtime.InteropServices;
using AlgoSdk.Crypto;
using AlgoSdk.Formatters;
using AlgoSdk.LowLevel;
using Unity.Collections;
using UnityEngine;

namespace AlgoSdk
{
    /// <summary>
    /// The private key for an Algorand account.
    /// </summary>
    [Serializable]
    [AlgoApiFormatter(typeof(PrivateKeyFormatter))]
    [StructLayout(LayoutKind.Explicit, Size = 32)]
    public partial struct PrivateKey
        : IEquatable<PrivateKey>
        , IByteArray
    {
        [SerializeField, FieldOffset(0)] internal Ed25519.Seed seed;

        public unsafe void* GetUnsafePtr() => seed.GetUnsafePtr();

        public int Length => seed.Length;

        public byte this[int index]
        {
            get => seed[index];
            set => seed[index] = value;
        }

        public byte[] SignTransaction<T>(T txn)
            where T : ITransaction, IEquatable<T>
        {
            using var kp = ToKeyPair();
            return AlgoApiSerializer.SerializeMessagePack(txn.Sign(kp.SecretKey));
        }

        public Mnemonic ToMnemonic()
        {
            using var bit11Array = this.ToBitArray(Allocator.Temp, bitsPerElement: 11);
            var result = new Mnemonic();
            for (var i = 0; i < Mnemonic.ChecksumIndex; i++)
                result[i] = (Mnemonic.Word)(bit11Array[i]);
            var checksum256 = Sha512.Hash256Truncated(this);
            using var checksum11Bit = checksum256.ToBitArray(Allocator.Temp, bitsPerElement: 11, maxArraySize: 1);
            result[Mnemonic.ChecksumIndex] = (Mnemonic.Word)(checksum11Bit[0]);
            return result;
        }

        public Ed25519.KeyPair ToKeyPair()
        {
            return seed.ToKeyPair();
        }

        public Ed25519.PublicKey ToPublicKey()
        {
            return seed.ToPublicKey();
        }

        public Address ToAddress()
        {
            return ToPublicKey();
        }

        public bool Equals(PrivateKey other)
        {
            for (var i = 0; i < Length; i++)
                if (!this[i].Equals(other[i]))
                    return false;
            return true;
        }

        public override string ToString()
        {
            var pk = this.ToPublicKey();
            var bytes = new byte[this.Length + pk.Length];
            for (var i = 0; i < this.Length; i++)
                bytes[i] = this[i];
            for (var i = 0; i < pk.Length; i++)
                bytes[i + this.Length] = pk[i];
            return System.Convert.ToBase64String(bytes);
        }

        public static PrivateKey FromString(string keyString)
        {
            var key = new PrivateKey();
            var bytes = System.Convert.FromBase64String(keyString);
            for (var i = 0; i < key.Length; i++)
                key[i] = bytes[i];
            return key;
        }
    }
}
