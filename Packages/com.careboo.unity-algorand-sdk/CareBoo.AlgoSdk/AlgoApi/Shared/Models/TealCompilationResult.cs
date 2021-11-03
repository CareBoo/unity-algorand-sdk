using System;
using UnityEngine;

namespace AlgoSdk
{
    /// <summary>
    /// Result from <see cref="IAlgodClient.TealCompile(string)"/>
    /// </summary>
    [AlgoApiObject]
    [Serializable]
    public struct TealCompilationResult
        : IEquatable<TealCompilationResult>
    {
        /// <summary>
        /// base32 SHA512_256 of program bytes (Address style)
        /// </summary>
        [AlgoApiField("hash", null)]
        [Tooltip("base32 SHA512_256 of program bytes (Address style)")]
        public string Hash;

        /// <summary>
        /// base64 encoded program bytes
        /// </summary>
        [AlgoApiField("result", null)]
        [Tooltip("base64 encoded program bytes")]
        public string CompiledBytesBase64;

        public bool Equals(TealCompilationResult other)
        {
            return Hash.Equals(other.Hash);
        }
    }
}
