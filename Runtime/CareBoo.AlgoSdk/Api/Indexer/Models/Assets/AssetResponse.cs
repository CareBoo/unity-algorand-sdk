using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public partial struct AssetResponse
        : IEquatable<AssetResponse>
        , IIndexerResponse<Asset>
    {
        [AlgoApiField("asset")]
        public Asset Asset { get; set; }

        [AlgoApiField("current-round")]
        public ulong CurrentRound { get; set; }

        Asset IIndexerResponse<Asset>.Result
        {
            get => Asset;
            set => Asset = value;
        }

        public bool Equals(AssetResponse other)
        {
            return Asset.Equals(other.Asset)
                && CurrentRound.Equals(other.CurrentRound)
                ;
        }
    }
}
