using System;
using AlgoSdk.Formatters;

namespace AlgoSdk
{
    /// <summary>
    /// Represents the action on the value
    /// </summary>
    [AlgoApiFormatter(typeof(ByteEnumFormatter<EvalDeltaAction>))]
    public enum EvalDeltaAction : byte
    {
        None = 0,
        SetUInt = 1,
        SetBytes = 2,
        Delete = 3
    }

    /// <summary>
    /// Represents a TEAL value delta.
    /// </summary>
    [AlgoApiObject]
    public struct EvalDelta
        : IEquatable<EvalDelta>
    {
        [AlgoApiField("action", "at")]
        public EvalDeltaAction Action;

        [AlgoApiField("bytes", "bs")]
        public TealBytes Bytes;

        [AlgoApiField("uint", "ui")]
        public ulong UInt;

        public bool Equals(EvalDelta other)
        {
            return Action == other.Action
                && Action switch
                {
                    EvalDeltaAction.SetBytes => Bytes.Equals(other.Bytes),
                    EvalDeltaAction.SetUInt => UInt.Equals(other.UInt),
                    _ => true
                };
        }
    }
}
