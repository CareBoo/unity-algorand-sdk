using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct Asset
        : IEquatable<Asset>
    {
        [AlgoApiKey("created-at-round", null)]
        public Optional<ulong> CreatedAtRound;

        [AlgoApiKey("deleted", null)]
        public Optional<bool> Deleted;

        [AlgoApiKey("destroyed-at-round", null)]
        public Optional<ulong> DestroyedAtRound;

        [AlgoApiKey("index", null)]
        public ulong Index;

        [AlgoApiKey("params", null)]
        public AssetParams Params;

        public bool Equals(Asset other)
        {
            return Index.Equals(other.Index)
                && Params.Equals(other.Params)
                ;
        }
    }
}
