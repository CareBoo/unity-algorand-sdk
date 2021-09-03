using Unity.Collections;

namespace AlgoSdk
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
    }
}
