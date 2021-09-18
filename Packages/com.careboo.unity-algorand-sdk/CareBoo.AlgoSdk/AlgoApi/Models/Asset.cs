using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct Asset
        : IEquatable<Asset>
    {
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
