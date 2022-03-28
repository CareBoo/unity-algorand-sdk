using System;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public partial struct AssetsResponse
        : IEquatable<AssetsResponse>
        , IPaginatedIndexerResponse<Asset>
    {
        [AlgoApiField("assets")]
        public Asset[] Assets { get; set; }

        [AlgoApiField("current-round")]
        public ulong CurrentRound { get; set; }

        [AlgoApiField("next-token")]
        public FixedString128Bytes NextToken { get; set; }

        Asset[] IPaginatedIndexerResponse<Asset>.Results
        {
            get => Assets;
            set => Assets = value;
        }

        public bool Equals(AssetsResponse other)
        {
            return ArrayComparer.Equals(Assets, other.Assets)
                && CurrentRound.Equals(other.CurrentRound)
                && NextToken.Equals(other.NextToken)
                ;
        }
    }
}
