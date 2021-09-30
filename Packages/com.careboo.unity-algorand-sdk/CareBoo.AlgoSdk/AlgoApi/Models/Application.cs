using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct Application
        : IEquatable<Application>
    {
        [AlgoApiKey("created-at-round")]
        public Optional<ulong> CreatedAtRound;

        [AlgoApiKey("deleted")]
        public Optional<bool> Deleted;

        [AlgoApiKey("deleted-at-round")]
        public Optional<ulong> DeletedAtRound;

        [AlgoApiKey("id")]
        public ulong Id;

        [AlgoApiKey("params")]
        public ApplicationParams Params;

        public bool Equals(Application other)
        {
            return Id.Equals(other.Id);
        }
    }
}
