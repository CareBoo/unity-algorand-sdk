using System;

namespace AlgoSdk
{
    /// <summary>
    /// Result from <see cref="IAlgodClient.TealCompile(string)"/>
    /// </summary>
    [AlgoApiObject]
    public struct TealCompilationResult
        : IEquatable<TealCompilationResult>
    {
        /// <summary>
        /// base32 SHA512_256 of program bytes (Address style)
        /// </summary>
        [AlgoApiField("hash", null)]
        public string Hash;

        /// <summary>
        /// base64 encoded program bytes
        /// </summary>
        [AlgoApiField("result", null)]
        public string CompiledBytesBase64;

        public bool Equals(TealCompilationResult other)
        {
            return Hash.Equals(other.Hash);
        }
    }
}
