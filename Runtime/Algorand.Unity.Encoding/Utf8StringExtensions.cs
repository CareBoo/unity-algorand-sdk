using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace Algorand.Unity
{
    public static unsafe class Utf8StringExtensions
    {
        public static FormatError Append<T>(ref this T fs, ulong input)
            where T : unmanaged, INativeList<byte>, IUTF8Bytes
        {
            const int maximumDigits = 20;
            var temp = stackalloc byte[maximumDigits];
            int offset = maximumDigits;
            do
            {
                var digit = (byte)(input % 10);
                temp[--offset] = (byte)('0' + digit);
                input /= 10;
            } while (input != 0);

            return fs.Append(temp + offset, maximumDigits - offset);
        }

        public static FormatError Append<T>(ref this T fs, bool input)
            where T : unmanaged, INativeList<byte>, IUTF8Bytes
        {
            return input
                    ? fs.Append("true")
                    : fs.Append("false")
                ;
        }

        public static byte[] ToByteArray<T>(this T text)
            where T : unmanaged, IUTF8Bytes, INativeList<byte>
        {
            var bytes = new byte[text.Length];
            fixed (byte* b = &bytes[0])
            {
                UnsafeUtility.MemCpy(b, text.GetUnsafePtr(), text.Length);
            }

            return bytes;
        }

        public static FormatError Append(ref this NativeText fs, ulong input)
        {
            const int maximumDigits = 20;
            var temp = stackalloc byte[maximumDigits];
            int offset = maximumDigits;
            do
            {
                var digit = (byte)(input % 10);
                temp[--offset] = (byte)('0' + digit);
                input /= 10;
            } while (input != 0);

            return fs.Append(temp + offset, maximumDigits - offset);
        }

        public static FormatError Append(ref this NativeText fs, bool input)
        {
            return input
                    ? fs.Append("true")
                    : fs.Append("false")
                ;
        }

        public static byte[] ToByteArray(this NativeText text)
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