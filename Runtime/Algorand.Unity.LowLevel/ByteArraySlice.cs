using System;
using System.Diagnostics;

namespace Algorand.Unity.LowLevel
{
    public struct ByteArraySlice<TBytes> : IByteArray where TBytes : struct, IByteArray
    {
        private TBytes bytes;
        private readonly int start;

        public ByteArraySlice(TBytes bytes, int start, int length)
        {
            CheckLength(bytes, start, length);
            this.bytes = bytes;
            this.start = start;
            Length = length;
        }

        public byte this[int index]
        {
            get => this.GetByteAt(index);
            set => this.SetByteAt(index, value);
        }

        public int Length { get; }

        public unsafe void* GetUnsafePtr()
        {
            return (byte*)bytes.GetUnsafePtr() + start;
        }

        [Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
        private static void CheckLength(TBytes bytes, int start, int length)
        {
            if (start >= bytes.Length || start < 0)
                throw new ArgumentOutOfRangeException(nameof(start));

            if (start + length > bytes.Length || length < 0)
                throw new ArgumentOutOfRangeException(nameof(length));
        }
    }
}