

using System;
using AlgoSdk.Crypto;
using AlgoSdk.Formatters;
using AlgoSdk.LowLevel;
using Unity.Collections;
using UnityEngine;

namespace AlgoSdk
{
    [Serializable]
    [AlgoApiFormatter(typeof(BlockHashFormatter))]
    public partial struct BlockHash
        : IByteArray
        , IEquatable<BlockHash>
    {
        [SerializeField] private Sha512_256_Hash hash;

        public byte this[int index] { get => hash[index]; set => hash[index] = value; }

        public unsafe void* GetUnsafePtr() => hash.GetUnsafePtr();

        public bool Equals(BlockHash other)
        {
            return this.hash.Equals(other.hash);
        }

        public int Length => hash.Length;

        public static implicit operator Sha512_256_Hash(BlockHash blockHash)
        {
            return blockHash.hash;
        }

        public static implicit operator BlockHash(Sha512_256_Hash hash)
        {
            return new BlockHash { hash = hash };
        }

        public static implicit operator BlockHash(string s)
        {
            return (FixedString64Bytes)s;
        }

        public static implicit operator BlockHash(FixedString64Bytes fs)
        {
            BlockHash result = default;
            result.CopyFromBase64(fs);
            return result;
        }
    }
}
