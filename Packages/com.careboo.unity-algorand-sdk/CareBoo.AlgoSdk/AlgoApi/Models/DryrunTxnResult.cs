using System;
using AlgoSdk.MsgPack;
using Unity.Collections;

namespace AlgoSdk
{
    [AlgoApiObject]
    public struct DryrunTxnResult
        : IMessagePackObject
        , IEquatable<DryrunTxnResult>
    {
        [AlgoApiKey("app-call-messages")]
        public FixedString128Bytes[] AppCallMessages;
        [AlgoApiKey("app-call-trace")]
        public DryrunState[] AppCallTrace;
        [AlgoApiKey("disassembly")]
        public FixedString128Bytes[] Disassembly;
        [AlgoApiKey("global-delta")]
        public EvalDeltaKeyValue[] GlobalDelta;
        [AlgoApiKey("local-deltas")]
        public AccountStateDelta[] LocalDeltas;
        [AlgoApiKey("logic-sig-messages")]
        public FixedString128Bytes[] LogicSigMessages;
        [AlgoApiKey("logic-sig-trace")]
        public DryrunState[] LogicSigTrace;

        public bool Equals(DryrunTxnResult other)
        {
            return this.Equals(ref other);
        }
    }
}

namespace AlgoSdk.MsgPack
{
    internal static partial class FieldMaps
    {
        internal static readonly Field<DryrunTxnResult>.Map dryrunTxnResultFields =
            new Field<DryrunTxnResult>.Map()
                .Assign("app-call-messages", (ref DryrunTxnResult x) => ref x.AppCallMessages, ArrayComparer<FixedString128Bytes>.Instance)
                .Assign("app-call-trace", (ref DryrunTxnResult x) => ref x.AppCallTrace, ArrayComparer<DryrunState>.Instance)
                .Assign("disassembly", (ref DryrunTxnResult x) => ref x.Disassembly, ArrayComparer<FixedString128Bytes>.Instance)
                .Assign("global-delta", (ref DryrunTxnResult x) => ref x.GlobalDelta, ArrayComparer<EvalDeltaKeyValue>.Instance)
                .Assign("local-deltas", (ref DryrunTxnResult x) => ref x.LocalDeltas, ArrayComparer<AccountStateDelta>.Instance)
                .Assign("logic-sig-messages", (ref DryrunTxnResult x) => ref x.LogicSigMessages, ArrayComparer<FixedString128Bytes>.Instance)
                .Assign("logic-sig-trace", (ref DryrunTxnResult x) => ref x.LogicSigTrace, ArrayComparer<DryrunState>.Instance)
                ;
    }
}
