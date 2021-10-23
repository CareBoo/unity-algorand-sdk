using System;
using AlgoSdk.Crypto;
using AlgoSdk.Formatters;
using AlgoSdk.LowLevel;
using Unity.Collections;

namespace AlgoSdk
{
    /// <summary>
    /// Genesis hash found of the genesis block.
    /// </summary>
    [AlgoApiFormatter(typeof(GenesisHashFormatter))]
    public struct GenesisHash
        : IByteArray
        , IEquatable<GenesisHash>
    {
        Sha512_256_Hash hash;

        public byte this[int index] { get => hash[index]; set => hash[index] = value; }

        public unsafe void* GetUnsafePtr() => hash.GetUnsafePtr();

        public int Length => hash.Length;

        public bool Equals(GenesisHash other)
        {
            return this.hash.Equals(other.hash);
        }

        public static implicit operator Sha512_256_Hash(GenesisHash genesisHash)
        {
            return genesisHash.hash;
        }

        public static implicit operator GenesisHash(Sha512_256_Hash hash)
        {
            return new GenesisHash { hash = hash };
        }

        public static implicit operator GenesisHash(string s)
        {
            GenesisHash result = default;
            FixedString64Bytes fs = s;
            result.CopyFromBase64(fs);
            return result;
        }
    }
}
