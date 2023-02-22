using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;

namespace Algorand.Unity.Collections
{
    public static class NativeTextExtensions
    {
        public unsafe static NativeArray<byte> AsArray(this NativeText text)
        {
#if ENABLE_UNITY_COLLECTIONS_CHECKS
            AtomicSafetyHandle.CheckGetSecondaryDataPointerAndThrow(text.m_Safety);
            var arraySafety = text.m_Safety;
            AtomicSafetyHandle.UseSecondaryVersion(ref arraySafety);
#endif
            var array = NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray<byte>(text.GetUnsafePtr(), text.Length, Allocator.None);

#if ENABLE_UNITY_COLLECTIONS_CHECKS
            NativeArrayUnsafeUtility.SetAtomicSafetyHandle(ref array, arraySafety);
#endif
            return array;
        }
        
        internal static bool ParseUlongInternal(ref NativeText fs, ref int offset, out ulong value)
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

        public static ParseError Parse(ref this NativeText fs, ref int offset, ref long output)
        {
            if (!FixedStringMethods.ParseLongInternal(ref fs, ref offset, out long value))
                return ParseError.Syntax;
            output = value;
            return ParseError.None;
        }

        public static ParseError Parse(ref this NativeText fs, ref int offset, ref ulong output)
        {
            if (!ParseUlongInternal(ref fs, ref offset, out ulong value))
                return ParseError.Syntax;
            output = value;
            return ParseError.None;
        }

        public static bool Found(ref this NativeText fs, ref int offset, char a, char b, char c, char d)
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

        public static bool Found(ref this NativeText fs, ref int offset, char a, char b, char c, char d, char e)
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
