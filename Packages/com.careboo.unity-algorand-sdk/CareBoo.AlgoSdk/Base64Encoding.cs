using AlgoSdk.LowLevel;
using Unity.Collections;
using Unity.Mathematics;

namespace AlgoSdk
{
    public static class Base64Encoding
    {
        public static void CopyToBase64<TByteArray, T>(ref this TByteArray bytes, ref T s)
            where TByteArray : struct, IByteArray
            where T : struct, IUTF8Bytes, INativeList<byte>
        {
            s.CopyFrom(System.Convert.ToBase64String(bytes.ToArray()));
        }

        public static void CopyFromBase64<TByteArray, T>(ref this TByteArray bytes, in T s, int maxLength = int.MaxValue)
            where TByteArray : struct, IByteArray
            where T : struct, IUTF8Bytes, INativeList<byte>
        {
            var managedString = s.ToString();
            var byteArr = System.Convert.FromBase64String(managedString);
            var length = math.min(maxLength, byteArr.Length);
            for (var i = 0; i < length; i++)
                bytes[i] = byteArr[i];
        }
    }
}
