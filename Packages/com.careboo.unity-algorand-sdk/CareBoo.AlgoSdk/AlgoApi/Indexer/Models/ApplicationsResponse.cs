using System;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct ApplicationsResponse
        : IEquatable<ApplicationsResponse>
    {
        [AlgoApiField("applications", null)]
        public Application[] Applications;

        [AlgoApiField("current-round", null)]
        public ulong CurrentRound;

        [AlgoApiField("next-token", null)]
        public FixedString128Bytes NextToken;

        public bool Equals(ApplicationsResponse other)
        {
            return ArrayComparer.Equals(Applications, other.Applications)
                && CurrentRound.Equals(other.CurrentRound)
                && NextToken.Equals(other.NextToken)
                ;
        }
    }
}
