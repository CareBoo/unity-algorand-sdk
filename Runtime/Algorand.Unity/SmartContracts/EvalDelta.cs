using System;
using UnityEngine;

namespace Algorand.Unity
{
    public interface IAppEvalDelta<TTxn>
        where TTxn : IAppliedSignedTxn<TTxn>
    {
        /// <summary>
        /// Global state delta
        /// </summary>
        StateDelta GlobalDelta { get; set; }

        /// <summary>
        /// When decoding EvalDeltas, the integer key represents an offset into
	    /// [txn.Sender, txn.Accounts[0], txn.Accounts[1], ...]
        /// </summary>
        StateDelta[] LocalDeltas { get; set; }

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
    /// Stores <see cref="StateDelta"/> for an application's global key/value store,
    /// and a number of accounts holding local state for that application.
    /// </summary>
    [AlgoApiObject]
    [Serializable]
    public partial struct EvalDelta<TTxn>
        : IEquatable<EvalDelta<TTxn>>
        , IAppEvalDelta<TTxn>
        where TTxn : IAppliedSignedTxn<TTxn>
    {
        [SerializeField, Tooltip("Global state delta.")]
        private StateDelta globalDelta;

        [SerializeField, Tooltip("Local state deltas.")]
        private StateDelta[] localDeltas;

        [SerializeField, Tooltip("Logs from application calls.")]
        private string[] logs;

        [SerializeField, Tooltip("The inner transactions (if any) that were evaluated.")]
        private TTxn[] innerTxns;


        /// <inheritdoc />
        [AlgoApiField("gd")]
        public StateDelta GlobalDelta
        {
            get => globalDelta;
            set => globalDelta = value;
        }

        /// <inheritdoc />
        [AlgoApiField("ld")]
        public StateDelta[] LocalDeltas
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

        public bool Equals(EvalDelta<TTxn> other)
        {
            return GlobalDelta.Equals(other.GlobalDelta)
                && ArrayComparer.Equals(LocalDeltas, other.LocalDeltas)
                && ArrayComparer.Equals(Logs, other.Logs)
                && ArrayComparer.Equals(InnerTxns, other.InnerTxns)
                ;
        }
    }
}
