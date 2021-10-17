using System;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct AssetsResponse
        : IEquatable<AssetsResponse>
    {
        [AlgoApiField("assets", null)]
        public Asset[] Assets;

        [AlgoApiField("current-round", null)]
        public ulong CurrentRound;

        [AlgoApiField("next-token", null)]
        public FixedString128Bytes NextToken;

        public bool Equals(AssetsResponse other)
        {
            return ArrayComparer.Equals(Assets, other.Assets)
                && CurrentRound.Equals(other.CurrentRound)
                && NextToken.Equals(other.NextToken)
                ;
        }
    }
}
