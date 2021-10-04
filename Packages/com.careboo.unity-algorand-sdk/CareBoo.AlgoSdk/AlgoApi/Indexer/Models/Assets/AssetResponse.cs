using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct AssetResponse
        : IEquatable<AssetResponse>
    {
        [AlgoApiField("asset", null)]
        public Asset Asset;

        [AlgoApiField("current-round", null)]
        public ulong CurrentRound;

        public bool Equals(AssetResponse other)
        {
            return Asset.Equals(other.Asset)
                && CurrentRound.Equals(other.CurrentRound)
                ;
        }
    }
}
