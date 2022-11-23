using Algorand.Unity.LowLevel;
using Unity.Collections;
using Unity.Mathematics;

namespace Algorand.Unity
{
    internal static class ByteArrayExtensions
    {
        public static NativeArray<ushort> ToBitArray<TByteArray>(ref this TByteArray bytes, Allocator allocator, int bitsPerElement, int maxArraySize = int.MaxValue)
            where TByteArray : unmanaged, IByteArray
        {
            var numElementsNeededToFitBytes = (bytes.Length * 8 + bitsPerElement - 1) / bitsPerElement;
            maxArraySize = math.min(maxArraySize, numElementsNeededToFitBytes);
            var result = new NativeArray<ushort>(maxArraySize, allocator);
            var baseNum = 2 << (bitsPerElement - 1);
            var baseMaxValue = baseNum - 1;
            var numBits = 0;
            var buffer = 0;
            var arrIndex = 0;
            for (var i = 0; i < bytes.Length && arrIndex < maxArraySize; i++)
            {
                buffer |= bytes.GetByteAt(i) << numBits;
                numBits += 8;
                if (numBits >= bitsPerElement)
                {
                    result[arrIndex] = (ushort)(buffer & baseMaxValue);
                    arrIndex++;
                    buffer >>= bitsPerElement;
                    numBits -= bitsPerElement;
                }
            }
            if (numBits != 0 && arrIndex < maxArraySize)
                result[arrIndex] = (ushort)(buffer & baseMaxValue);
            return result;
        }
    }
}
