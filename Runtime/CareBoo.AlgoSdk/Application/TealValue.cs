using System;
using AlgoSdk.Formatters;
using AlgoSdk.LowLevel;
using UnityEngine;

namespace AlgoSdk
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

    [Serializable]
    [AlgoApiFormatter(typeof(ByteArrayFormatter<TealBytes>))]
    public partial struct TealBytes
        : IEquatable<TealBytes>
        , IByteArray
    {
        public const int SizeBytes = 128;
        public FixedBytes128 Bytes;
        public byte this[int index] { get => this.GetByteAt(index); set => this.SetByteAt(index, value); }

        public unsafe void* GetUnsafePtr()
        {
            fixed (byte* b = &Bytes.offset0000.offset0000.byte0000)
                return b;
        }

        public int Length => SizeBytes;

        public bool Equals(TealBytes other)
        {
            return ByteArray.Equals(this, other);
        }
    }
}
