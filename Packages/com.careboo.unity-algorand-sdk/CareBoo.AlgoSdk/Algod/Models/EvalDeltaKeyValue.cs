using System;
using AlgoSdk.MsgPack;
using Unity.Collections;

namespace AlgoSdk
{
    public struct EvalDeltaKeyValue
        : IMessagePackObject
        , IEquatable<EvalDeltaKeyValue>
    {
        public FixedString64Bytes Key;
        public EvalDelta Value;

        public bool Equals(EvalDeltaKeyValue other)
        {
            return this.Equals(ref other);
        }
    }
}

namespace AlgoSdk.MsgPack
{
    internal static partial class FieldMaps
    {
        internal static readonly Field<EvalDeltaKeyValue>.Map evalDeltaKeyValueFields =
            new Field<EvalDeltaKeyValue>.Map()
                .Assign("key", (ref EvalDeltaKeyValue x) => ref x.Key)
                .Assign("value", (ref EvalDeltaKeyValue x) => ref x.Value)
                ;
    }
}
