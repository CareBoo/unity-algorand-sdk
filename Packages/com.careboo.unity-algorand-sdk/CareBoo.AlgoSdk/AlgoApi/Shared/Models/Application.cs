using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct Application
        : IEquatable<Application>
    {
        [AlgoApiField("created-at-round", null)]
        public ulong CreatedAtRound;

        [AlgoApiField("deleted", null)]
        public Optional<bool> Deleted;

        [AlgoApiField("deleted-at-round", null)]
        public ulong DeletedAtRound;

        [AlgoApiField("id", null)]
        public ulong Id;

        [AlgoApiField("params", null)]
        public ApplicationParams Params;

        public bool Equals(Application other)
        {
            return Id.Equals(other.Id);
        }
    }
}
