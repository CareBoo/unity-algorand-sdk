using System;
using System.Runtime.InteropServices;
using System.Text;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using AlgoSdk.LowLevel;
using AlgoSdk.Crypto;

namespace AlgoSdk
{
    [Serializable]
    [StructLayout(LayoutKind.Explicit, Size = 32)]
    [BurstCompatible]
    public struct PrivateKey
    : IEquatable<PrivateKey>
    , IByteArray
    {
        [SerializeField] [FieldOffset(0)] internal Ed25519.Seed seed;

        public IntPtr Buffer => seed.Buffer;

        public int Length => seed.Length;

        public byte this[int index]
        {
            get => seed[index];
            set => seed[index] = value;
        }

        public Mnemonic ToMnemonic()
        {
            using var bit11Array = this.ToBitArray(Allocator.Temp, bitsPerElement: 11);
            var result = new Mnemonic();
            for (var i = 0; i < Mnemonic.ChecksumIndex; i++)
                result[i] = (Mnemonic.Word)(bit11Array[i]);
            var checksum256 = Sha512.Hash256Truncated(in this);
            using var checksum11Bit = checksum256.ToBitArray(Allocator.Temp, bitsPerElement: 11, maxArraySize: 1);
            result[Mnemonic.ChecksumIndex] = (Mnemonic.Word)(checksum11Bit[0]);
            return result;
        }

        public Address ToAddress()
        {
            throw new NotImplementedException();
        }

        public bool Equals(PrivateKey other)
        {
            for (var i = 0; i < Length; i++)
                if (!this[i].Equals(other[i]))
                    return false;
            return true;
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
