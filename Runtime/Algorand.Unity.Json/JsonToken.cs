using Unity.Collections;

namespace Algorand.Unity.Json
{
    public enum JsonToken
    {
        None = -1,
        ObjectBegin,
        ObjectEnd,
        ArrayBegin,
        ArrayEnd,
        String,
        Bool,
        Null,
        Number,
        EscapeChar
    }

    public static class JsonTokenExtensions
    {
        public static JsonToken ToJsonToken(this Unicode.Rune rune)
        {
            return rune.ToChar().ToJsonToken();
        }

        public static JsonToken ToJsonToken(this char c)
        {
            return c switch
            {
                '{' => JsonToken.ObjectBegin,
                '}' => JsonToken.ObjectEnd,
                '[' => JsonToken.ArrayBegin,
                ']' => JsonToken.ArrayEnd,
                '"' => JsonToken.String,
                't' => JsonToken.Bool,
                'f' => JsonToken.Bool,
                'n' => JsonToken.Null,
                '0' => JsonToken.Number,
                '1' => JsonToken.Number,
                '2' => JsonToken.Number,
                '3' => JsonToken.Number,
                '4' => JsonToken.Number,
                '5' => JsonToken.Number,
                '6' => JsonToken.Number,
                '7' => JsonToken.Number,
                '8' => JsonToken.Number,
                '9' => JsonToken.Number,
                '-' => JsonToken.Number,
                '.' => JsonToken.Number,
                '+' => JsonToken.Number,
                'e' => JsonToken.Number,
                'E' => JsonToken.Number,
                '\\' => JsonToken.EscapeChar,
                _ => JsonToken.None
            };
        }
    }
}
