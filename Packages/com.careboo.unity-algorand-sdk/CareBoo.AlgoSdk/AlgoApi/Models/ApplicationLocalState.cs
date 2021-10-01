using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct ApplicationLocalState
        : IEquatable<ApplicationLocalState>
    {
        [AlgoApiKey("closed-out-at-round", null)]
        public Optional<ulong> ClosedOutAtRound;

        [AlgoApiKey("deleted", null)]
        public Optional<bool> Deleted;

        [AlgoApiKey("id", null)]
        public ulong Id;

        [AlgoApiKey("key-value", null)]
        public TealKeyValue[] KeyValues;

        [AlgoApiKey("opted-in-at-round", null)]
        public Optional<ulong> OptedInAtRound;

        [AlgoApiKey("schema", null)]
        public ApplicationStateSchema Schema;

        public bool Equals(ApplicationLocalState other)
        {
            return Id.Equals(other.Id)
                && ArrayComparer.Equals(KeyValues, other.KeyValues);
        }
    }
}
