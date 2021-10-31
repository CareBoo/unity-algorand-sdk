using System;
using System.Runtime.InteropServices;
using AlgoSdk.Formatters;
using AlgoSdk.LowLevel;

namespace AlgoSdk
{
    /// <summary>
    /// TEAL Value Type. Value 1 refers to bytes, value 2 refers to uint.
    /// </summary>
    [AlgoApiFormatter(typeof(ByteEnumFormatter<TealValueType>))]
    public enum TealValueType : byte
    {
        None = 0,
        Bytes = 1,
        Uint = 2
    }

    /// <summary>
    /// Represents a TEAL value.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    [AlgoApiFormatter(typeof(TealValueFormatter))]
    public struct TealValue
        : IEquatable<TealValue>
    {
        /// <summary>
        /// [tb] bytes value.
        /// </summary>
        [FieldOffset(0)] TealBytes bytes;

        /// <summary>
        /// [ui] uint value.
        /// </summary>
        [FieldOffset(0)] ulong uintValue;

        /// <summary>
        /// See <see cref="TealValueType"/>
        /// </summary>
        [FieldOffset(64)] public TealValueType Type;

        public TealBytes Bytes
        {
            get
            {
                if (Type != TealValueType.Bytes)
                    throw new NotSupportedException(
                        $"You cannot access {nameof(TealValue)}.{nameof(Bytes)} when {nameof(TealValue)}.{nameof(Type)} == {Type}"
                    );
                return bytes;
            }
            set
            {
                Type = TealValueType.Bytes;
                bytes = value;
            }
        }

        public ulong Uint
        {
            get
            {
                if (Type != TealValueType.Uint)
                    throw new NotSupportedException(
                        $"You cannot access {nameof(TealValue)}.{nameof(Uint)} when {nameof(TealValue)}.{nameof(Type)} == {Type}"
                    );
                return uintValue;
            }
            set
            {
                Type = TealValueType.Uint;
                uintValue = value;
            }
        }

        public bool Equals(TealValue other)
        {
            if (this.Type != other.Type) return false;

            return Type switch
            {
                TealValueType.Bytes => bytes.Equals(other.bytes),
                TealValueType.Uint => uintValue == other.uintValue,
                _ => true
            };
        }
    }

    [AlgoApiFormatter(typeof(ByteArrayFormatter<TealBytes>))]
    public struct TealBytes
        : IEquatable<TealBytes>
        , IByteArray
    {
        public const int SizeBytes = 64;
        public FixedBytes64 Bytes;
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
