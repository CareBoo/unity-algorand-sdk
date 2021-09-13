using System;
using AlgoSdk.MsgPack;

namespace AlgoSdk
{
    public enum EvalDeltaAction : byte
    {
        None = 0,
        SetUint = 1,
        SetBytes = 2,
        Delete = 3
    }

    [AlgoApiObject]
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
