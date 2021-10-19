using Unity.Collections;

namespace AlgoSdk
{
    public static class Endianness
    {
        public static NativeArray<byte> ToBytesBigEndian(this ulong value, Allocator allocator)
        {
            var result = new NativeArray<byte>(8, allocator);
            unchecked
            {
                result[0] = (byte)(value >> 56);
                result[1] = (byte)(value >> 48);
                result[2] = (byte)(value >> 40);
                result[3] = (byte)(value >> 32);
                result[4] = (byte)(value >> 24);
                result[5] = (byte)(value >> 16);
                result[6] = (byte)(value >> 8);
                result[7] = (byte)(value);
            }
            return result;
        }
    }
}
