using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct ApplicationLocalState
        : IEquatable<ApplicationLocalState>
    {
        [AlgoApiKey("closed-out-at-round")]
        public Optional<ulong> ClosedOutAtRound;

        [AlgoApiKey("deleted")]
        public Optional<bool> Deleted;

        [AlgoApiKey("id")]
        public ulong Id;

        [AlgoApiKey("key-value")]
        public TealKeyValue[] KeyValues;

        [AlgoApiKey("opted-in-at-round")]
        public Optional<ulong> OptedInAtRound;

        [AlgoApiKey("schema")]
        public ApplicationStateSchema Schema;

        public bool Equals(ApplicationLocalState other)
        {
            return Id.Equals(other.Id)
                && ArrayComparer.Equals(KeyValues, other.KeyValues);
        }
    }
}
