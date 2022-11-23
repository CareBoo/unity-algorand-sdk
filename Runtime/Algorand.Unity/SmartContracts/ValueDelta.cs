using System;

namespace Algorand.Unity
{
    /// <summary>
    /// Represents a TEAL value delta.
    /// </summary>
    [AlgoApiObject]
    [Serializable]
    public partial struct ValueDelta
        : IEquatable<ValueDelta>
    {
        [AlgoApiField("at")]
        public DeltaAction Action;

        [AlgoApiField("bs")]
        public TealBytes Bytes;

        [AlgoApiField("ui")]
        public ulong UInt;

        public bool Equals(ValueDelta other)
        {
            return Action == other.Action
                && Action switch
                {
                    DeltaAction.SetBytes => Bytes.Equals(other.Bytes),
                    DeltaAction.SetUInt => UInt.Equals(other.UInt),
                    _ => true
                };
        }
    }
}
