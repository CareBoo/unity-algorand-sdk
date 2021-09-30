using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct Asset
        : IEquatable<Asset>
    {
        [AlgoApiKey("created-at-round")]
        public Optional<ulong> CreatedAtRound;

        [AlgoApiKey("deleted")]
        public Optional<bool> Deleted;

        [AlgoApiKey("destroyed-at-round")]
        public Optional<ulong> DestroyedAtRound;

        [AlgoApiKey("index")]
        public ulong Index;

        [AlgoApiKey("params")]
        public AssetParams Params;

        public bool Equals(Asset other)
        {
            return Index.Equals(other.Index)
                && Params.Equals(other.Params)
                ;
        }
    }
}
