using System;
using UnityEngine;

namespace Algorand.Unity
{
    /// <summary>
    /// TEAL Value Type. Value 1 refers to bytes, value 2 refers to uint.
    /// </summary>
    public enum TealValueType : byte
    {
        None = 0,
        Bytes = 1,
        Uint = 2
    }

    /// <summary>
    /// Represents a TEAL value.
    /// </summary>
    [AlgoApiObject]
    [Serializable]
    public partial struct TealValue
        : IEquatable<TealValue>
    {
        /// <summary>
        /// [tb] bytes value.
        /// </summary>
        [AlgoApiField("tb")]
        [SerializeField]
        public TealBytes Bytes;

        /// <summary>
        /// [ui] uint value.
        /// </summary>
        [AlgoApiField("ui")]
        [SerializeField]
        public ulong UintValue;

        /// <summary>
        /// See <see cref="TealValueType"/>
        /// </summary>
        [AlgoApiField("tt")]
        [SerializeField]
        public TealValueType Type;

        public bool Equals(TealValue other)
        {
            if (this.Type != other.Type) return false;

            return Type switch
            {
                TealValueType.Bytes => Bytes.Equals(other.Bytes),
                TealValueType.Uint => UintValue == other.UintValue,
                _ => true
            };
        }
    }
}
