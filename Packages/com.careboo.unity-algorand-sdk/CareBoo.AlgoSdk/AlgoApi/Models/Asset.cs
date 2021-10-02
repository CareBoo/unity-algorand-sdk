using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct Asset
        : IEquatable<Asset>
    {
        [AlgoApiField("created-at-round", null)]
        public Optional<ulong> CreatedAtRound;

        [AlgoApiField("deleted", null)]
        public Optional<bool> Deleted;

        [AlgoApiField("destroyed-at-round", null)]
        public Optional<ulong> DestroyedAtRound;

        [AlgoApiField("index", null)]
        public ulong Index;

        [AlgoApiField("params", null)]
        public AssetParams Params;

        public bool Equals(Asset other)
        {
            return Index.Equals(other.Index)
                && Params.Equals(other.Params)
                ;
        }
    }
}
