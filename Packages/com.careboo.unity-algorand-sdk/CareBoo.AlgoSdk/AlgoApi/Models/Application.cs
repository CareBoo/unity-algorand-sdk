using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct Application
        : IEquatable<Application>
    {
        [AlgoApiKey("created-at-round", null)]
        public Optional<ulong> CreatedAtRound;

        [AlgoApiKey("deleted", null)]
        public Optional<bool> Deleted;

        [AlgoApiKey("deleted-at-round", null)]
        public Optional<ulong> DeletedAtRound;

        [AlgoApiKey("id", null)]
        public ulong Id;

        [AlgoApiKey("params", null)]
        public ApplicationParams Params;

        public bool Equals(Application other)
        {
            return Id.Equals(other.Id);
        }
    }
}
