using System;
using System.Runtime.InteropServices;
using AlgoSdk.LowLevel;

namespace AlgoSdk
{
    public enum TealValueType : int
    {
        None = 0,
        Bytes = 1,
        Uint = 2
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct TealValue
        : IEquatable<TealValue>
    {
        [FieldOffset(0)] TealBytes bytes;
        [FieldOffset(0)] ulong uintValue;
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

    public struct TealBytes
        : IEquatable<TealBytes>
        , IByteArray
    {
        public const int SizeBytes = 64;
        public FixedBytes64 Bytes;
        public byte this[int index] { get => this.GetByteAt(index); set => this.SetByteAt(index, value); }

        public unsafe IntPtr Buffer
        {
            get
            {
                fixed (byte* b = &Bytes.offset0000.offset0000.byte0000)
                    return (IntPtr)b;
            }
        }

        public int Length => SizeBytes;

        public bool Equals(TealBytes other)
        {
            return ByteArray.Equals(in this, in other);
        }
    }
}
