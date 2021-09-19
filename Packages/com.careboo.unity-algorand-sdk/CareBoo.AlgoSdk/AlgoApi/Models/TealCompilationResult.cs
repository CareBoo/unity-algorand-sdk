using System;
using AlgoSdk.Crypto;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct TealCompilationResult
        : IEquatable<TealCompilationResult>
    {
        [AlgoApiKey("hash")]
        public Sha512_256_Hash Hash;
        [AlgoApiKey("result")]
        public string BytesBase64;

        public bool Equals(TealCompilationResult other)
        {
            return Hash.Equals(other.Hash);
        }
    }
}
