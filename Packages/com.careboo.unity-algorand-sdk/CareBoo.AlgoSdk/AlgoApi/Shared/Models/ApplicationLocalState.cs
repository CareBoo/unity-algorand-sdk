using System;

namespace AlgoSdk
{
    /// <summary>
    /// Stores local state associated with an application.
    /// </summary>
    [AlgoApiObject]
    public struct ApplicationLocalState
        : IEquatable<ApplicationLocalState>
    {
        /// <summary>
        /// Round when account closed out of the application.
        /// </summary>
        [AlgoApiField("closed-out-at-round", null)]
        public ulong ClosedOutAtRound;

        /// <summary>
        /// Whether or not the application local state is currently deleted from its account.
        /// </summary>
        [AlgoApiField("deleted", null)]
        public Optional<bool> Deleted;

        /// <summary>
        /// The application which this local state is for.
        /// </summary>
        [AlgoApiField("id", null)]
        public ulong Id;

        /// <summary>
        /// [tkv] storage.
        /// </summary>
        [AlgoApiField("key-value", null)]
        public TealKeyValue[] KeyValues;

        /// <summary>
        /// Round when the account opted into the application.
        /// </summary>
        [AlgoApiField("opted-in-at-round", null)]
        public ulong OptedInAtRound;

        /// <summary>
        /// [hsch] schema.
        /// </summary>
        [AlgoApiField("schema", null)]
        public StateSchema Schema;

        public bool Equals(ApplicationLocalState other)
        {
            return Id.Equals(other.Id)
                && ArrayComparer.Equals(KeyValues, other.KeyValues);
        }
    }
}
