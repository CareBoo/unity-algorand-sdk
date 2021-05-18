
using System;
using System.Diagnostics;

namespace Algorand {
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
