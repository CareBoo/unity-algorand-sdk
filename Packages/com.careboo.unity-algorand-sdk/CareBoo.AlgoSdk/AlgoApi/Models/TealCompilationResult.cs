using System;
using AlgoSdk.Crypto;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct TealCompilationResult
        : IEquatable<TealCompilationResult>
    {
        [AlgoApiKey("hash", null)]
        public Sha512_256_Hash Hash;

        [AlgoApiKey("result", null)]
        public string BytesBase64;

        public bool Equals(TealCompilationResult other)
        {
            return Hash.Equals(other.Hash);
        }
    }
}
