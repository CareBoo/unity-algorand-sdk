using System;

namespace AlgoSdk
{
    /// <summary>
    /// Stores <see cref="AppStateDelta"/> for an application's global key/value store,
    /// and a number of accounts holding local state for that application.
    /// </summary>
    [AlgoApiObject]
    [Serializable]
    public partial struct AppEvalDelta
        : IEquatable<AppEvalDelta>
    {
        /// <summary>
        /// Global state delta
        /// </summary>
        [AlgoApiField("gd", "gd")]
        public AppStateDelta GlobalDelta;

        /// <summary>
        /// Local state deltas
        /// </summary>
        [AlgoApiField("ld", "ld")]
        public AppStateDelta[] LocalDeltas;

        /// <summary>
        /// Logs from application calls
        /// </summary>
        [AlgoApiField("lg", "lg")]
        public string[] Logs;

        /// <summary>
        /// The inner transactions (if any) that were evaluated.
        /// </summary>
        [AlgoApiField("itx", "itx")]
        public AppliedSignedTxn[] InnerTxns;

        public bool Equals(AppEvalDelta other)
        {
            return GlobalDelta.Equals(other.GlobalDelta)
                && ArrayComparer.Equals(LocalDeltas, other.LocalDeltas)
                && ArrayComparer.Equals(Logs, other.Logs)
                && ArrayComparer.Equals(InnerTxns, other.InnerTxns)
                ;
        }
    }
}
