using System;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct DryrunTxnResult
        : IEquatable<DryrunTxnResult>
    {
        [AlgoApiField("app-call-messages", null)]
        public FixedString128Bytes[] AppCallMessages;
        [AlgoApiField("app-call-trace", null)]
        public DryrunState[] AppCallTrace;
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
