using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct ApplicationResponse
        : IEquatable<ApplicationResponse>
    {
        [AlgoApiField("application", null)]
        public Application Application;

        [AlgoApiField("current-round", null)]
        public ulong CurrentRound;

        public bool Equals(ApplicationResponse other)
        {
            return Application.Equals(other.Application)
                && CurrentRound.Equals(other.CurrentRound)
                ;
        }
    }
}
