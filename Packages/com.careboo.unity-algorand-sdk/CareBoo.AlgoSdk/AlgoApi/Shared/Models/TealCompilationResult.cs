using System;
using AlgoSdk.Crypto;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct TealCompilationResult
        : IEquatable<TealCompilationResult>
    {
        [AlgoApiField("hash", null)]
        public Sha512_256_Hash Hash;

        [AlgoApiField("result", null)]
        public string BytesBase64;

        public bool Equals(TealCompilationResult other)
        {
            return Hash.Equals(other.Hash);
        }
    }
}
