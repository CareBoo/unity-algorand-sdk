using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public partial struct ApplicationResponse
        : IEquatable<ApplicationResponse>
        , IIndexerResponse<Application>
    {
        [AlgoApiField("application", null)]
        public Application Application { get; set; }

        [AlgoApiField("current-round", null)]
        public ulong CurrentRound { get; set; }

        Application IIndexerResponse<Application>.Result
        {
            get => Application;
            set => Application = value;
        }

        public bool Equals(ApplicationResponse other)
        {
            return Application.Equals(other.Application)
                && CurrentRound.Equals(other.CurrentRound)
                ;
        }
    }
}
