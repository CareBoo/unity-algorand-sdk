using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace Algorand.Unity
{
    public unsafe static class Utf8StringExtensions
    {
        public static FormatError Append<T>(ref this T fs, ulong input)
            where T : struct, INativeList<byte>, IUTF8Bytes
        {
            const int maximumDigits = 20;
            var temp = stackalloc byte[maximumDigits];
            int offset = maximumDigits;
            do
            {
                var digit = (byte)(input % 10);
                temp[--offset] = (byte)('0' + digit);
                input /= 10;
            }
            while (input != 0);

            return fs.Append(temp + offset, maximumDigits - offset);
        }

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
            unsafe
            {
                fixed (byte* b = &bytes[0])
                {
                    UnsafeUtility.MemCpy(b, text.GetUnsafePtr(), text.Length);
                }
            }
            return bytes;
        }
    }
}
