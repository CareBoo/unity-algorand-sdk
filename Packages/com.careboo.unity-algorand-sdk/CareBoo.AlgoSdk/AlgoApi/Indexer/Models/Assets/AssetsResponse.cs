using System;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public partial struct AssetsResponse
        : IEquatable<AssetsResponse>
        , IPaginatedResponse
    {
        [AlgoApiField("assets", null)]
        public Asset[] Assets { get; set; }

        [AlgoApiField("current-round", null)]
        public ulong CurrentRound { get; set; }

        [AlgoApiField("next-token", null)]
        public FixedString128Bytes NextToken { get; set; }

        public bool Equals(AssetsResponse other)
        {
            return ArrayComparer.Equals(Assets, other.Assets)
                && CurrentRound.Equals(other.CurrentRound)
                && NextToken.Equals(other.NextToken)
                ;
        }
    }
}
