using System;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct DryrunTxnResult
        : IEquatable<DryrunTxnResult>
    {
        [AlgoApiKey("app-call-messages", null)]
        public FixedString128Bytes[] AppCallMessages;
        [AlgoApiKey("app-call-trace", null)]
        public DryrunState[] AppCallTrace;
        [AlgoApiKey("disassembly", null)]
        public FixedString128Bytes[] Disassembly;
        [AlgoApiKey("global-delta", null)]
        public EvalDeltaKeyValue[] GlobalDelta;
        [AlgoApiKey("local-deltas", null)]
        public AccountStateDelta[] LocalDeltas;
        [AlgoApiKey("logic-sig-messages", null)]
        public FixedString128Bytes[] LogicSigMessages;
        [AlgoApiKey("logic-sig-trace", null)]
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
