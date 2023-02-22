using Unity.Collections;

namespace Algorand.Unity.Collections
{
    public static class FixedStringParseExtensions
    {
        internal static bool ParseUlongInternal<T>(ref T fs, ref int offset, out ulong value)
            where T : unmanaged, INativeList<byte>, IUTF8Bytes
        {
            var digitOffset = offset;
            value = 0;
            var rune = fs.Peek(offset);
            while (offset < fs.Length && Unicode.Rune.IsDigit(rune))
            {
                value *= 10;
                value += (ulong)(fs.Read(ref offset).value - '0');
                rune = fs.Peek(offset);
            }

            return digitOffset != offset;
        }

        public static ParseError Parse<T>(ref this T fs, ref int offset, ref long output)
            where T : unmanaged, INativeList<byte>, IUTF8Bytes
        {
            if (!FixedStringMethods.ParseLongInternal(ref fs, ref offset, out long value))
                return ParseError.Syntax;
            output = value;
            return ParseError.None;
        }

        public static ParseError Parse<T>(ref this T fs, ref int offset, ref ulong output)
            where T : unmanaged, INativeList<byte>, IUTF8Bytes
        {
            if (!ParseUlongInternal(ref fs, ref offset, out ulong value))
                return ParseError.Syntax;
            output = value;
            return ParseError.None;
        }

        public static bool Found<T>(ref this T fs, ref int offset, char a, char b, char c, char d)
            where T : unmanaged, INativeList<byte>, IUTF8Bytes
        {
            var old = offset;
            if ((fs.Read(ref offset).value | 32) == a
                && (fs.Read(ref offset).value | 32) == b
                && (fs.Read(ref offset).value | 32) == c
                && (fs.Read(ref offset).value | 32) == d)
                return true;
            offset = old;
            return false;
        }

        public static bool Found<T>(ref this T fs, ref int offset, char a, char b, char c, char d, char e)
            where T : unmanaged, INativeList<byte>, IUTF8Bytes
        {
            var old = offset;
            if ((fs.Read(ref offset).value | 32) == a
                && (fs.Read(ref offset).value | 32) == b
                && (fs.Read(ref offset).value | 32) == c
                && (fs.Read(ref offset).value | 32) == d
                && (fs.Read(ref offset).value | 32) == e)
                return true;
            offset = old;
            return false;
        }
    }
}