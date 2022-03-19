using System;

namespace AlgoSdk
{
    /// <summary>
    /// Represents the action on the value
    /// </summary>
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
    [Serializable]
    public partial struct EvalDelta
        : IEquatable<EvalDelta>
    {
        [AlgoApiField("action", "action")]
        public EvalDeltaAction Action;

        [AlgoApiField("bytes", "bytes")]
        public TealBytes Bytes;

        [AlgoApiField("uint", "uint")]
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
