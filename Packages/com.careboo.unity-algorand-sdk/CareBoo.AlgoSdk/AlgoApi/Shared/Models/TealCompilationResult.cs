using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct TealCompilationResult
        : IEquatable<TealCompilationResult>
    {
        [AlgoApiField("hash", null)]
        public Address Hash;

        [AlgoApiField("result", null)]
        public string CompiledBytesBase64;

        public bool Equals(TealCompilationResult other)
        {
            return Hash.Equals(other.Hash);
        }
    }
}
