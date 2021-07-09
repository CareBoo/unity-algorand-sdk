
using System;
using System.Diagnostics;
using Unity.Collections.LowLevel.Unsafe;

namespace AlgoSdk
{
    public interface IByteArray
    {
        int Length { get; }
        byte this[int index] { get; set; }
    }

    internal static class ByteArray
    {
        [Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
        internal static void CheckElementAccess(int index, int length)
        {
            if (index < 0 || index >= length)
                throw new IndexOutOfRangeException($"Index {index} is out of range [0, {length}).");
        }
    }
}
