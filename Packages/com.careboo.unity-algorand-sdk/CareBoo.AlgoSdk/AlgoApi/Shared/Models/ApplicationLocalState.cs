using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct ApplicationLocalState
        : IEquatable<ApplicationLocalState>
    {
        [AlgoApiField("closed-out-at-round", null)]
        public ulong ClosedOutAtRound;

        [AlgoApiField("deleted", null)]
        public Optional<bool> Deleted;

        [AlgoApiField("id", null)]
        public ulong Id;

        [AlgoApiField("key-value", null)]
        public TealKeyValue[] KeyValues;

        [AlgoApiField("opted-in-at-round", null)]
        public ulong OptedInAtRound;

        [AlgoApiField("schema", null)]
        public StateSchema Schema;

        public bool Equals(ApplicationLocalState other)
        {
            return Id.Equals(other.Id)
                && ArrayComparer.Equals(KeyValues, other.KeyValues);
        }
    }
}
