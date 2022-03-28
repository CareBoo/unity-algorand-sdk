using System;
using UnityEngine;

namespace AlgoSdk
{
    /// <summary>
    /// Stores local state associated with an application.
    /// </summary>
    [AlgoApiObject]
    [Serializable]
    public partial struct ApplicationLocalState
        : IEquatable<ApplicationLocalState>
    {
        /// <summary>
        /// Round when account closed out of the application.
        /// </summary>
        [AlgoApiField("closed-out-at-round")]
        [Tooltip("Round when account closed out of the application.")]
        public ulong ClosedOutAtRound;

        /// <summary>
        /// Whether or not the application local state is currently deleted from its account.
        /// </summary>
        [AlgoApiField("deleted")]
        [Tooltip("Whether or not the application local state is currently deleted from its account.")]
        public Optional<bool> Deleted;

        /// <summary>
        /// The application which this local state is for.
        /// </summary>
        [AlgoApiField("id")]
        [Tooltip("The application which this local state is for.")]
        public ulong Id;

        /// <summary>
        /// [tkv] storage.
        /// </summary>
        [AlgoApiField("key-value")]
        [Tooltip("[tkv] storage.")]
        public TealKeyValue[] KeyValues;

        /// <summary>
        /// Round when the account opted into the application.
        /// </summary>
        [AlgoApiField("opted-in-at-round")]
        [Tooltip("Round when the account opted into the application.")]
        public ulong OptedInAtRound;

        /// <summary>
        /// [hsch] schema.
        /// </summary>
        [AlgoApiField("schema")]
        [Tooltip("[hsch] schema.")]
        public StateSchema Schema;

        public bool Equals(ApplicationLocalState other)
        {
            return Id.Equals(other.Id)
                && ArrayComparer.Equals(KeyValues, other.KeyValues);
        }
    }
}
