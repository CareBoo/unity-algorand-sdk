using System;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct BalancesResponse
        : IEquatable<BalancesResponse>
    {
        [AlgoApiField("balances", null)]
        public MiniAssetHolding[] Balances;

        [AlgoApiField("current-round", null)]
        public ulong CurrentRound;

        [AlgoApiField("next-token", null)]
        public FixedString128Bytes NextToken;

        public bool Equals(BalancesResponse other)
        {
            return ArrayComparer.Equals(Balances, other.Balances)
                && CurrentRound.Equals(other.CurrentRound)
                && NextToken.Equals(other.NextToken)
                ;
        }
    }
}
