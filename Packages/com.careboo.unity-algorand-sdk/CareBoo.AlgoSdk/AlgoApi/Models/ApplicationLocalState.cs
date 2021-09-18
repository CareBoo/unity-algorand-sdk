using System;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct ApplicationLocalState
        : IEquatable<ApplicationLocalState>
    {
        [AlgoApiKey("id")]
        public ulong Id;

        [AlgoApiKey("key-value")]
        public TealKeyValue[] KeyValues;

        [AlgoApiKey("schema")]
        public ApplicationStateSchema Schema;

        public bool Equals(ApplicationLocalState other)
        {
            return Id.Equals(other.Id)
                && ArrayComparer.Equals(KeyValues, other.KeyValues);
        }
    }
}
