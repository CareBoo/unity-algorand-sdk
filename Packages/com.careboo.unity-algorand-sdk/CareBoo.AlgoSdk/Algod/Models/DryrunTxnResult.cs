using System;
using AlgoSdk.MsgPack;
using Unity.Collections;

namespace AlgoSdk
{
    public struct DryrunTxnResult
        : IMessagePackObject
        , IEquatable<DryrunTxnResult>
    {
        public FixedString128Bytes[] AppCallMessages;
        public DryrunState[] AppCallTrace;
        public FixedString128Bytes[] Disassembly;
        public EvalDeltaKeyValue[] GlobalDelta;
        public AccountStateDelta[] LocalDeltas;
        public FixedString128Bytes[] LogicSigMessages;
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
