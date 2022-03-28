using System;
using Unity.Collections;
using UnityEngine;

namespace AlgoSdk
{
    /// <summary>
    /// DryrunTxnResult contains any <see cref="LogicSig"/> or ApplicationCall program debug information and state updates from a dryrun.
    /// </summary>
    [AlgoApiObject]
    [Serializable]
    public partial struct DryrunTxnResult
        : IEquatable<DryrunTxnResult>
    {
        [AlgoApiField("app-call-messages")]
        public FixedString128Bytes[] AppCallMessages;

        [AlgoApiField("app-call-trace")]
        public DryrunState[] AppCallTrace;

        /// <summary>
        /// Execution cost of app call transaction
        /// </summary>
        [AlgoApiField("cost")]
        [Tooltip("Execution cost of app call transaction")]
        public ulong Cost;

        /// <summary>
        /// Disassembled program line by line.
        /// </summary>
        [AlgoApiField("disassembly")]
        [Tooltip("Disassembled program line by line.")]
        public FixedString128Bytes[] Disassembly;

        [AlgoApiField("global-delta")]
        public EvalDeltaKeyValue[] GlobalDelta;

        [AlgoApiField("local-deltas")]
        public AccountStateDelta[] LocalDeltas;

        [AlgoApiField("logic-sig-messages")]
        public FixedString128Bytes[] LogicSigMessages;

        [AlgoApiField("logic-sig-trace")]
        public DryrunState[] LogicSigTrace;

        public bool Equals(DryrunTxnResult other)
        {
            return ArrayComparer.Equals(AppCallMessages, other.AppCallMessages)
                && ArrayComparer.Equals(AppCallTrace, other.AppCallTrace)
                && ArrayComparer.Equals(Disassembly, other.Disassembly)
                && ArrayComparer.Equals(GlobalDelta, other.GlobalDelta)
                && ArrayComparer.Equals(LocalDeltas, other.LocalDeltas)
                && ArrayComparer.Equals(LogicSigMessages, other.LogicSigMessages)
                && ArrayComparer.Equals(LogicSigTrace, other.LogicSigTrace)
                ;
        }
    }
}
