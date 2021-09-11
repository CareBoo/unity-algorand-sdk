using System;
using System.Collections.Generic;
using AlgoSdk.MsgPack;

namespace AlgoSdk
{
    public class EvalDeltaActionComparer : IEqualityComparer<EvalDeltaAction>
    {
        public bool Equals(EvalDeltaAction x, EvalDeltaAction y)
        {
            return x == y;
        }

        public int GetHashCode(EvalDeltaAction obj)
        {
            return obj.GetHashCode();
        }

        public static EvalDeltaActionComparer Instance = new EvalDeltaActionComparer();
    }

    public enum EvalDeltaAction
    {
        None = 0,
        SetUint = 1,
        SetBytes = 2,
        Delete = 3
    }

    public struct EvalDelta
        : IMessagePackObject
        , IEquatable<EvalDelta>
    {
        [AlgoApiKey("action")]
        public EvalDeltaAction Action;
        [AlgoApiKey("bytes")]
        public TealBytes Bytes;
        [AlgoApiKey("uint")]
        public ulong Uint;

        public bool Equals(EvalDelta other)
        {
            return this.Equals(ref other);
        }
    }
}

namespace AlgoSdk.MsgPack
{
    internal static partial class FieldMaps
    {
        internal static readonly Field<EvalDelta>.Map evalDeltaFields =
            new Field<EvalDelta>.Map()
                .Assign("action", (ref EvalDelta x) => ref x.Action, EvalDeltaActionComparer.Instance)
                .Assign("bytes", (ref EvalDelta x) => ref x.Bytes)
                .Assign("uint", (ref EvalDelta x) => ref x.Uint)
                ;
    }
}
