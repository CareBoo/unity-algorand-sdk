using System;
using Unity.Collections;

namespace AlgoSdk
{
    /// <summary>
    /// DryrunTxnResult contains any <see cref="LogicSig"/> or ApplicationCall program debug information and state updates from a dryrun.
    /// </summary>
    [AlgoApiObject]
    public struct DryrunTxnResult
        : IEquatable<DryrunTxnResult>
    {
        [AlgoApiField("app-call-messages", null)]
        public FixedString128Bytes[] AppCallMessages;

        [AlgoApiField("app-call-trace", null)]
        public DryrunState[] AppCallTrace;

        /// <summary>
        /// Execution cost of app call transaction
        /// </summary>
        [AlgoApiField("cost", null)]
        public ulong Cost;

        /// <summary>
        /// Disassembled program line by line.
        /// </summary>
        [AlgoApiField("disassembly", null)]
        public FixedString128Bytes[] Disassembly;

        [AlgoApiField("global-delta", null)]
        public EvalDeltaKeyValue[] GlobalDelta;

        [AlgoApiField("local-deltas", null)]
        public AccountStateDelta[] LocalDeltas;

        [AlgoApiField("logic-sig-messages", null)]
        public FixedString128Bytes[] LogicSigMessages;

        [AlgoApiField("logic-sig-trace", null)]
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
