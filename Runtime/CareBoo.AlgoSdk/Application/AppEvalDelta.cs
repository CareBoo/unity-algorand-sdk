using System;
using UnityEngine;

namespace AlgoSdk
{
    public interface IAppEvalDelta<TTxn>
        where TTxn : IAppliedSignedTxn<TTxn>
    {
        /// <summary>
        /// Global state delta
        /// </summary>
        AppStateDelta GlobalDelta { get; set; }

        /// <summary>
        /// Local state deltas
        /// </summary>
        AccountStateDelta[] LocalDeltas { get; set; }

        /// <summary>
        /// Logs from application calls
        /// </summary>
        string[] Logs { get; set; }

        /// <summary>
        /// The inner transactions (if any) that were evaluated.
        /// </summary>
        TTxn[] InnerTxns { get; set; }
    }

    /// <summary>
    /// Stores <see cref="AppStateDelta"/> for an application's global key/value store,
    /// and a number of accounts holding local state for that application.
    /// </summary>
    [AlgoApiObject]
    [Serializable]
    public partial struct AppEvalDelta<TTxn>
        : IEquatable<AppEvalDelta<TTxn>>
        , IAppEvalDelta<TTxn>
        where TTxn : IAppliedSignedTxn<TTxn>
    {
        [SerializeField, Tooltip("Global state delta.")]
        AppStateDelta globalDelta;

        [SerializeField, Tooltip("Local state deltas.")]
        AccountStateDelta[] localDeltas;

        [SerializeField, Tooltip("Logs from application calls.")]
        string[] logs;

        [SerializeField, Tooltip("The inner transactions (if any) that were evaluated.")]
        TTxn[] innerTxns;


        /// <inheritdoc />
        [AlgoApiField("gd")]
        public AppStateDelta GlobalDelta
        {
            get => globalDelta;
            set => globalDelta = value;
        }

        /// <inheritdoc />
        [AlgoApiField("ld")]
        public AccountStateDelta[] LocalDeltas
        {
            get => localDeltas;
            set => localDeltas = value;
        }

        /// <inheritdoc />
        [AlgoApiField("lg")]
        public string[] Logs
        {
            get => logs;
            set => logs = value;
        }

        /// <inheritdoc />
        [AlgoApiField("itx")]
        public TTxn[] InnerTxns
        {
            get => innerTxns;
            set => innerTxns = value;
        }

        public bool Equals(AppEvalDelta<TTxn> other)
        {
            return GlobalDelta.Equals(other.GlobalDelta)
                && ArrayComparer.Equals(LocalDeltas, other.LocalDeltas)
                && ArrayComparer.Equals(Logs, other.Logs)
                && ArrayComparer.Equals(InnerTxns, other.InnerTxns)
                ;
        }
    }
}
