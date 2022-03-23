using System;

namespace AlgoSdk
{
    /// <summary>
    /// A health check response from <see cref="IIndexerClient.GetHealth"/>
    /// </summary>
    [AlgoApiObject]
    [Serializable]
    public partial struct HealthCheck
        : IEquatable<HealthCheck>
    {
        [AlgoApiField("data", "data")]
        public AlgoApiObject Data;

        [AlgoApiField("db-available", "db-available")]
        public bool DatabaseAvailable;

        [AlgoApiField("errors", "errors")]
        public string[] Errors;

        [AlgoApiField("is-migrating", "is-migrating")]
        public bool IsMigrating;

        [AlgoApiField("message", "message")]
        public string Message;

        [AlgoApiField("round", "round")]
        public ulong Round;

        [AlgoApiField("version", "version")]
        public string Version;

        public bool Equals(HealthCheck other)
        {
            return Data.Equals(other.Data)
                && DatabaseAvailable.Equals(other.DatabaseAvailable)
                && ArrayComparer.Equals(Errors, other.Errors)
                && IsMigrating.Equals(other.IsMigrating)
                && Message.Equals(other.Message)
                && Round.Equals(other.Round)
                ;
        }
    }
}
