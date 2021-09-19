using System;
using AlgoSdk.Crypto;
using AlgoSdk.Formatters;
using AlgoSdk.LowLevel;

namespace AlgoSdk
{
    [AlgoApiFormatter(typeof(GenesisHashFormatter))]
    public struct GenesisHash
        : IByteArray
        , IEquatable<GenesisHash>
    {
        Sha512_256_Hash hash;

        public byte this[int index] { get => hash[index]; set => hash[index] = value; }

        public IntPtr Buffer => hash.Buffer;

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
    }
}
