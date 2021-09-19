using Unity.Collections;

namespace AlgoSdk.Json
{
    public static class UnicodeExtensions
    {
        public static bool IsWhiteSpaceOrSeparator(this Unicode.Rune rune)
        {
            var c = rune.ToChar();
            return c == ' ' || c == '\t' || c == '\n' || c == '\r'
                || c == ',' || c == ':'
                ;
        }

        public static char ToChar(this Unicode.Rune rune)
        {
            return (char)(rune.value);
        }

        public static Unicode.Rune ToRune(this char c)
        {
            return new Unicode.Rune(c);
        }
    }
}
