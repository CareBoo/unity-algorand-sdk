using Unity.Collections;

namespace Algorand.Unity.LowLevel
{
    public static class NativeArrayUtil
    {
        public static NativeArray<byte> ConcatAll(byte[][] byteArrays, Allocator allocator)
        {
            if (byteArrays == null)
                return new NativeArray<byte>(0, allocator);
            var size = 0;
            for (var i = 0; i < byteArrays.Length; i++)
                size += byteArrays[i]?.Length ?? 0;
            var bytes = new NativeArray<byte>(size, allocator);
            var offset = 0;
            for (var i = 0; i < byteArrays.Length; i++)
            {
                if (byteArrays[i] == null)
                    continue;
                NativeArray<byte>.Copy(byteArrays[i], 0, bytes, offset, byteArrays[i].Length);
                offset += byteArrays[i].Length;
            }
            return bytes;
        }
    }
}
