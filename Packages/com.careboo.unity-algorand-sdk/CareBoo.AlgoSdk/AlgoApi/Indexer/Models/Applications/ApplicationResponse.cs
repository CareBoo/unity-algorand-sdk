using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct ApplicationResponse
        : IEquatable<ApplicationResponse>
        , IIndexerResponse
    {
        [AlgoApiField("application", null)]
        public Application Application { get; set; }

        [AlgoApiField("current-round", null)]
        public ulong CurrentRound { get; set; }

        public bool Equals(ApplicationResponse other)
        {
            return Application.Equals(other.Application)
                && CurrentRound.Equals(other.CurrentRound)
                ;
        }
    }
}
