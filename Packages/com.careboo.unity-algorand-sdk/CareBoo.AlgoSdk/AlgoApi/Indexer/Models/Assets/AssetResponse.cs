using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct AssetResponse
        : IEquatable<AssetResponse>
        , IIndexerResponse
    {
        [AlgoApiField("asset", null)]
        public Asset Asset { get; set; }

        [AlgoApiField("current-round", null)]
        public ulong CurrentRound { get; set; }

        public bool Equals(AssetResponse other)
        {
            return Asset.Equals(other.Asset)
                && CurrentRound.Equals(other.CurrentRound)
                ;
        }
    }
}
