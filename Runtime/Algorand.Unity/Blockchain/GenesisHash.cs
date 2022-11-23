using System;
using Algorand.Unity.Crypto;
using Algorand.Unity.Formatters;
using Algorand.Unity.LowLevel;
using Unity.Collections;
using UnityEngine;

namespace Algorand.Unity
{
    /// <summary>
    /// Genesis hash found of the genesis block.
    /// </summary>
    [Serializable]
    [AlgoApiFormatter(typeof(GenesisHashFormatter))]
    public partial struct GenesisHash
        : IByteArray
        , IEquatable<GenesisHash>
    {
        [SerializeField] private Sha512_256_Hash hash;

        public byte this[int index] { get => hash[index]; set => hash[index] = value; }

        public unsafe void* GetUnsafePtr() => hash.GetUnsafePtr();

        public int Length => hash.Length;

        public bool Equals(GenesisHash other)
        {
            return this.hash.Equals(other.hash);
        }

        public static implicit operator GenesisHash(byte[] bytes)
        {
            if (bytes == null)
                throw new ArgumentNullException(nameof(bytes));

            GenesisHash result = default;
            if (bytes.Length != result.Length)
                throw new ArgumentException(
                    $"Was expecting {result.Length} bytes but got {bytes.Length} bytes.",
                    nameof(bytes)
                );

            result.CopyFrom(bytes, 0, result.Length);
            return result;
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
