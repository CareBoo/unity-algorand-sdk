using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace Algorand.Unity.Collections
{
    public static unsafe class Utf8StringExtensions
    {
        public static FormatError Append<T>(ref this T fs, bool input)
            where T : struct, INativeList<byte>, IUTF8Bytes
        {
            return input
                    ? fs.Append("true")
                    : fs.Append("false")
                ;
        }

        public static byte[] ToByteArray<T>(this T text)
            where T : struct, IUTF8Bytes, INativeList<byte>
        {
            var bytes = new byte[text.Length];
            fixed (byte* b = &bytes[0])
            {
                UnsafeUtility.MemCpy(b, text.GetUnsafePtr(), text.Length);
            }

            return bytes;
        }
    }
}
